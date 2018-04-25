using System.Xml;
using System.Xml.XPath;
using System.IO;

namespace Epic.MVC
{
    public class XmlHelper
    {

        public static XmlTextReader ParseXmlTextReader(string value)
        {
            return ParseXmlTextReader(value, new XmlParserContext(null, null, null, XmlSpace.None));
        }

        public static XmlTextReader ParseXmlTextReader(string value, XmlParserContext context)
        {
            return new XmlTextReader(value, XmlNodeType.Element, context);
        }

        public static XPathNavigator ParseXPathNavigator(string value)
        {
            return new XPathDocument(new StringReader(value)).CreateNavigator();
        }

        public static XPathNavigator ParseXPathNavigator(Stream value)
        {
            return new XPathDocument(value).CreateNavigator();
        }

        public static XPathNodeIterator ParseXPathNodeIterator(string value)
        {
            return ParseXPathNavigator(value).Select("/*");
        }

        public static XPathNodeIterator ParseXPathNodeIterator(Stream value)
        {
            return ParseXPathNavigator(value).Select("/*");
        }

    }
}
