using System;

namespace PdmCode.Infrastructure
{
    public class DisplayResourceAttribute : Attribute
    {
        public DisplayResourceAttribute(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            Name = name;
        }

        public string Name { get; set; }

        public Type ResourceType { get; set; }

        public string GetName()
        {
            if (ResourceType == null)
            {
                return Name;
            }
            return LocalizableString.GetLocalizableValue(ResourceType, Name);
        }
    }
}
