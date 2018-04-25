using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Epic.Configuration
{
    public class EpicSettings
    {
        static EpicSettings current;
        public static EpicSettings Current
        {
            get
            {
                if (current == null)
                    current = new EpicSettings();
                return current;
            }
        }

        EpicSettings()
        {

            var config = ConfigurationManager.OpenExeConfiguration(String.Empty);
            var section = (ProtectedConfigurationSection)config.GetSection("configProtectedData");
            this.Providers = section.Providers;
        }

        public ProviderSettingsCollection Providers
        {
            get;
            private set;
        }

    }
}
