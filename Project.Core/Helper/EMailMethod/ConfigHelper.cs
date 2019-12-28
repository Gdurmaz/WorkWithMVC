using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Helper.EMailMethod
{
    public class ConfigHelper
    {
        //The key value read and return the value.
        public static T Get<T>(string key)
        {
            return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key],typeof(T));
        } 
    }
}
