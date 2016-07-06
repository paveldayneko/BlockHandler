using System.Collections.Generic;
using System.Xml.Linq;
using BlocksHandler.Domain;

namespace BlocksHandler.Services.Abstraction
{
    public interface IContentBlockSerializer
    {
        IEnumerable<ContentBlock> Deserialize(XDocument document);
    }
}