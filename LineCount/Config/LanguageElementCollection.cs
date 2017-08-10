using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace LineCount.Config
{
    public class LanguageElementCollection : ConfigurationElementCollection
    {
        public const string ELEMENT_NAME = "ProgrammingLanguages";

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override string ElementName
        {
            get
            {
                return LanguageElement.ELEMENT_NAME;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new LanguageElement();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((LanguageElement) element).Name;
        }

        public IEnumerable<LanguageElement> Elements
        {
            get
            {
                foreach (ConfigurationElement e in this)
                {
                    yield return (LanguageElement)e;
                }
            }
        }

        public ConfigurationElement[] GetAll()
        {
            ConfigurationElement[] retVal = new ConfigurationElement[this.Count];
            this.CopyTo(retVal, 0);
            return retVal;
        }
    }
}
