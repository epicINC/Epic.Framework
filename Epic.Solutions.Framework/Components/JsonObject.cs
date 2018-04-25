using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Components
{
    public abstract class JsonObject
    {
        public string Save()
        {
            return Save(this);
        }



        public static T Read<T>(string value)
        {
            return Epic.Utility.JsonUtility.Deserialize<T>(value);
        }

        public static string Save(object value)
        {
            return Epic.Utility.JsonUtility.Serialize(value);
        }
    }
}
