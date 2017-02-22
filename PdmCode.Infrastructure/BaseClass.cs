using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace PdmCode.Infrastructure
{
	public class BaseClass : NotificationObject
	{
		public virtual Type EntityType => GetType();

		#region Properties
		public virtual bool IsEnumProperty(string propertyName)
		{
			var property = EntityType.GetTypeInfo().GetDeclaredProperty(propertyName);
			return typeof(Enum).GetTypeInfo().IsAssignableFrom(property.PropertyType.GetTypeInfo());
		}

		public virtual Type PropertyType(string propertyName)
		{
			var property = EntityType.GetTypeInfo().GetDeclaredProperty(propertyName);
			return property.PropertyType;
		}

		public virtual bool HasProperty(string propertyName)
		{
			return EntityType.GetTypeInfo().GetDeclaredProperty(propertyName) != null;
		}

		/// <summary>
		/// Gets value of property.
		/// </summary>
		/// <param name="propertyName">property name</param>
		/// <returns></returns>
		public virtual object GetPropertyValue(string propertyName)
		{
			var propertyInfo = EntityType.GetTypeInfo().GetDeclaredProperty(propertyName);
			return propertyInfo?.GetValue(this, null);
		}

		/// <summary>
		/// Gets value of property.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="expression"></param>
		/// <returns></returns>
		public virtual object GetPropertyValue<T>(Expression<Func<T>> expression)
		{
			var propertyName = PropertySupport.ExtractPropertyName(expression);
			return GetPropertyValue(propertyName);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="propertyName"></param>
		/// <param name="value"></param>
		public virtual void SetPropertyValue(string propertyName, object value)
		{
			var property = EntityType.GetTypeInfo().GetDeclaredProperty(propertyName);
			if (property != null && property.CanWrite)
			{
				if (!string.IsNullOrEmpty(value?.ToString()))
				{
					var isEnum = typeof(Enum).GetTypeInfo().IsAssignableFrom(property.PropertyType.GetTypeInfo());
					if (isEnum)
					{
						if (value.GetType() != typeof(string))
						{
							value = Enum.GetName(property.PropertyType, value);
						}
						value = Enum.Parse(property.PropertyType, value.ToString(), true);
					}
					property.SetValue(this, Convert.ChangeType(value, property.PropertyType, CultureInfo.CurrentCulture), null);
				}
				else
				{
					property.SetValue(this, null, null);
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="expression"></param>
		/// <param name="value"></param>
		public virtual void SetPropertyValue<T>(Expression<Func<T>> expression, object value)
		{
			var propertyName = PropertySupport.ExtractPropertyName(expression);
			SetPropertyValue(propertyName, value);
		}

		public virtual object this[string name]
		{
			get
			{
				return GetPropertyValue(name);
			}
			set
			{
				SetPropertyValue(name, value);
				RaisePropertyChanged(name);
			}
		}
		#endregion

		#region Events
		public event EventHandler<PropertyChangedEventArgs> OnPropertyChanged = delegate { };

		protected override void RaisePropertyChanged(string propertyName)
		{
			base.RaisePropertyChanged(propertyName);
			OnPropertyChanged?.Invoke(this, new PropertyChangedEventArgs
			{
				Property = GetType().GetTypeInfo().GetDeclaredProperty(propertyName),
				Value = GetType().GetTypeInfo().GetDeclaredProperty(propertyName).GetValue(this, null)
			});
		}
		#endregion
	}
}
