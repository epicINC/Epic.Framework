using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epic.Extensions;
using System.ComponentModel;

namespace Epic.Solutions.Framework.ConsoleApplication.TempTest
{
    class TypeConverterTest
    {

        public static void Test()
        {
            Console.WriteLine(ToType<int>("123"));
            Console.WriteLine(ToType<byte>("123"));
        }



        public static T ToType<T>(object value)
        {
            return (T)ToType(typeof(T), value);
        }


        public static object ToType(Type destinationType, object value)
        {
            var convertible = value as IConvertible;
            if (value != null)
            {
                try
                {
                    return convertible.ToType(destinationType, null);
                }
                catch
                {
                }
            }

            var converter = TypeDescriptor.GetConverter(destinationType);
            var flag = !converter.CanConvertFrom(value.GetType());

            if (!flag)
                converter = TypeDescriptor.GetConverter(value.GetType());

            if (!flag && !converter.CanConvertTo(destinationType))
            {
                if (destinationType.IsEnum && value is int)
                    return Enum.ToObject(destinationType, (int)value);

                throw new InvalidOperationException("From: {0}, To: {1}".Formatting(value.GetType().FullName, destinationType.FullName));
            }
            else
            {
                try
                {
                    return flag ? converter.ConvertFrom(null, null, value) : converter.ConvertTo(null, null, value, destinationType);
                }
                catch (Exception ex)
                {

                    throw new InvalidOperationException("From: {0}, To: {1}".Formatting(value.GetType().FullName, destinationType.FullName), ex);
                }
            }

            return null;
        }
    }
}
