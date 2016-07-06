using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;
using BlocksHandler.Domain;
using BlocksHandler.Services.Abstraction;

namespace BlocksHandler.Services
{
    public class ContentBlockSerializer : IContentBlockSerializer
    {
        private static readonly XmlSerializer XmlSerializer = new XmlSerializer(typeof(ContentBlock));

        public IEnumerable<ContentBlock> Deserialize(XDocument document)
        {
            var xElements = document.Descendants("Block");
            foreach (var xElement in xElements)
            {
                yield return (ContentBlock)XmlSerializer.Deserialize(xElement.CreateReader());
            }
        }
    }
}