using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace LineCount.Config
{
    public class LanguageElement : ConfigurationElement
    {
        public const string ELEMENT_NAME = "Language";
        private const string FILE_MASK_PROPERTY = "FileMask";
        private const string NAME_PROPERTY = "Name";

        [ConfigurationProperty(FILE_MASK_PROPERTY, IsRequired=true)]
        public string FileMask
        {
            get
            {
                return (string)base[FILE_MASK_PROPERTY];
            }
            set
            {
                base[FILE_MASK_PROPERTY] = value;
            }
        }

        [ConfigurationProperty(NAME_PROPERTY, IsRequired = true, IsKey=true)]
        public string Name
        {
            get
            {
                return (string)base[NAME_PROPERTY];
            }
            set
            {
                base[NAME_PROPERTY] = value;
            }
        }

        public string[] MaskList
        {
            get
            {
                string mask = FileMask;
                if (null == mask)
                {
                    throw new InvalidOperationException();
                }

                return mask.Split(',', ';');
            }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
