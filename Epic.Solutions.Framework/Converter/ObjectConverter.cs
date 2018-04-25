using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Converter
{
    public class ObjectConverter
    {


        public static Dictionary<string, object> AsDictionary(object value)
        {
            var result = new Dictionary<string, object>();
            if (value != null)
            {
                foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(value))
                {
                    result.Add(propertyDescriptor.Name, propertyDescriptor.GetValue(value));
                }
            }
            return result;
        }

        public static string AsQueryString(object value, bool removeEmptyEntry = true)
        {
            return DictionaryConverter.AsQueryString(AsDictionary(value), removeEmptyEntry);
        }
    }
}
