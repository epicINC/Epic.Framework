using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Epic.Components;

namespace Epic.Converter
{
    public static class XmlNodeConverter
    {
        static XmlNodeConverter()
        {
            Selector = e => e.InnerText;
        }

        internal static Func<XmlNode, string> Selector
        {
            get;
            set;
        }

    
        public static string AsString(XmlNode value)
        {
            return AsString(value, String.Empty);
        }

        public static string AsString(XmlNode value, string defaultValue)
        {
            return CommonConverter.AsString(value, Selector, e => e.Trim(), defaultValue);
        }

        public static bool AsBool(XmlNode value, bool defaultValue = false)
        {
            return CommonConverter.AsBool(value, Selector, defaultValue);
        }

        public static byte AsByte(XmlNode value, byte defaultValue = 0)
        {
            return CommonConverter.AsByte(value, Selector, defaultValue);
        }

        public static ushort AsUInt16(XmlNode value, ushort defaultValue = 0)
        {
            return CommonConverter.AsUInt16(value, Selector, defaultValue);
        }

        public static short AsInt16(XmlNode value, short defaultValue = 0)
        {
            return CommonConverter.AsInt16(value, Selector, defaultValue);
        }

        public static uint AsUInt32(XmlNode value, uint defaultValue = 0)
        {
            return CommonConverter.AsUInt32(value, Selector, defaultValue);
        }

        public static int AsInt32(XmlNode value, int defaultValue = 0)
        {
            return CommonConverter.AsInt32(value, Selector, defaultValue);
        }

        public static ulong AsUInt64(XmlNode value, ulong defaultValue = 0)
        {
            return CommonConverter.AsUInt64(value, Selector, defaultValue);
        }

        public static long AsInt64(XmlNode value, long defaultValue = 0)
        {
            return CommonConverter.AsInt64(value, Selector, defaultValue);
        }

        public static T AsEnum<T>(XmlNode value, T defaultValue = default(T)) where T : struct, IEnumConstraint
        {
            return CommonConverter.AsEnum(value, Selector, defaultValue);
        }
 
    }
}
