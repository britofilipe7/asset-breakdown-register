using AssetBreakdownRegisterV3.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetBreakdownRegisterV2.Utility
{
    public class SaveAppConfigSettings
    {
        public string key;
        public string value;
        public SaveAppConfigSettings(string key, string value) {
            this.key = key;
            this.value = value;
        }
        
        public void Save()
        {
            Settings.Default[key] = value;
            Settings.Default.Save();
        }
    }
}
