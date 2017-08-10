using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace LineCount.Config
{
    public class AppPreferencesSection : ConfigurationSection
    {
        private const string SECTION_NAME = "Preferences";

        public static AppPreferencesSection Instance
        {
            get
            {
                if (null == _instance)
                {
                    _instance = (AppPreferencesSection) ConfigurationManager.GetSection(SECTION_NAME);
                }
                return _instance;
            }
        }

        [ConfigurationProperty(LanguageElementCollection.ELEMENT_NAME, IsDefaultCollection=true)]
        [ConfigurationCollection(typeof(LanguageElementCollection))]
        public LanguageElementCollection ProgrammingLanguages
        {
            get
            {
                return (LanguageElementCollection)base[LanguageElementCollection.ELEMENT_NAME];
            }
        }
	 


        private static AppPreferencesSection _instance;
    }
}
