using System;
using System.Reflection;

namespace PdmCode.Infrastructure
{
    public class PropertyChangedEventArgs : EventArgs
    {
        public PropertyInfo Property { get; set; }

        public object Value { get; set; }
    }
}
