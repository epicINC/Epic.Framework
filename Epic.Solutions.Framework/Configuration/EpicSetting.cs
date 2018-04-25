using Epic.Components;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Configuration
{
    public class EpicSetting
    {
        private EpicSetting()
        {

        }

        public static EpicSetting Current
        {
            get { return LazyLoad.Load(() => Create()); }
        }

        static EpicSetting Create()
        {
            var config = ConfigurationManager.OpenExeConfiguration(String.Empty);
            var section = (ProtectedConfigurationSection)config.GetSection("configProtectedData");
            return new EpicSetting() { Providers = section.Providers };
        }

        public ProviderSettingsCollection Providers
        {
            get;
            private set;
        }

    }
}
