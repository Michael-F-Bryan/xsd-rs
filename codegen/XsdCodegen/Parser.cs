using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace XsdCodegen
{
    public static class Parser
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(Schema));

        public static Schema Parse(Stream stream)
        {
            return (Schema)Serializer.Deserialize(stream);
        }

        public static Schema Parse(string src)
        {
            var bytes = Encoding.UTF8.GetBytes(src);

            using (var mem = new MemoryStream(bytes))
            {
                return Parse(mem);
            }
        }
    }
}