using System;

namespace PdmCode.Infrastructure
{
    public class LocalizedString
    {
        public string Input { get; set; }

        public Type ResourceType { get; set; }

        public string Output
        {
            get
            {
                if (string.IsNullOrEmpty(Input))
                {
                    throw new NullReferenceException();
                }
                if (ResourceType == null)
                {
                    return Input;
                }
                return LocalizableString.GetLocalizableValue(ResourceType, Input);
            }
        }
    }
}
