using System;
using System.Reflection;

namespace PdmCode.Infrastructure
{
	public static class LocalizableString
	{
		public static string GetLocalizableValue(Type resourceType, string propertyValue)
		{
			var property = resourceType.GetRuntimeProperty(propertyValue);

			bool badlyConfigured = false;

			if (property == null || property.PropertyType != typeof(string))
			{
				badlyConfigured = true;
			}
			else
			{
				MethodInfo getter = property.GetMethod;

				if (getter == null || !(getter.IsPublic && getter.IsStatic))
				{
					badlyConfigured = true;
				}
			}

			if (badlyConfigured)
			{
				throw new InvalidOperationException();
			}
			return (string)property.GetValue(null, null);
		}
	}
}
