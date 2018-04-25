using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Runtime;

namespace Epic.Configuration
{
    public sealed class KeyValueConfigurationElement : ConfigurationElement
    {
        // Fields
        static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
        static readonly ConfigurationProperty propKey = new ConfigurationProperty("key", typeof(string), string.Empty, ConfigurationPropertyOptions.IsKey);
        static readonly ConfigurationProperty propValue = new ConfigurationProperty("value", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

        // Methods
        static KeyValueConfigurationElement()
        {
            properties.Add(propKey);
            properties.Add(propValue);
        }

        internal KeyValueConfigurationElement()
        {
        }

        public KeyValueConfigurationElement(string name, string value)
        {
            base[propKey] = name;
            base[propValue] = value;
        }

        // Properties
        [ConfigurationProperty("key", IsKey = true, DefaultValue = "")]
        public string Name
        {
            get { return (string)base[propKey]; }
        }

        protected  override ConfigurationPropertyCollection Properties
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get { return properties; }
        }

        [ConfigurationProperty("value", DefaultValue = "")]
        public string Value
        {
            get { return (string)base[propValue]; }
            set { base[propValue] = value; }
        }
    }

 

}
