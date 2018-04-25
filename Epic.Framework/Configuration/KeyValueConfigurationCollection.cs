using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Runtime;
using Epic.Utility;

namespace Epic.Configuration
{
    [ConfigurationCollection(typeof(NameValueConfigurationElement))]
    public sealed class KeyValueConfigurationCollection : ConfigurationElementCollection
    {
        // Fields
        static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

        // Methods
        public void Add(KeyValueConfigurationElement nameValue)
        {
            this.BaseAdd(nameValue);
        }

        public void Clear()
        {
            base.BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new KeyValueConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((KeyValueConfigurationElement)element).Name;
        }

        public void Remove(KeyValueConfigurationElement nameValue)
        {
            if (base.BaseIndexOf(nameValue) >= 0)
                base.BaseRemove(nameValue.Name);
        }

        public void Remove(string name)
        {
            base.BaseRemove(name);
        }

        // Properties
        public string[] AllKeys
        {
            get { return StringUtil.ObjectArrayToStringArray(base.BaseGetAllKeys()); }
        }

        public new NameValueConfigurationElement this[string name]
        {
            get { return (NameValueConfigurationElement)base.BaseGet(name); }
            set
            {
                int index = -1;
                NameValueConfigurationElement element = (NameValueConfigurationElement)base.BaseGet(name);
                if (element != null)
                {
                    index = base.BaseIndexOf(element);
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get { return _properties; }
        }
    }
}
