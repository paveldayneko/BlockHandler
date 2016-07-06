using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using BlocksHandler.Model;
using BlocksHandler.Converters.Abstraction;
using BlocksHandler.Services.Abstraction;

namespace BlocksHandler.Services
{
    public class ContentBlockService : IContentBlockService
    {
        private readonly IContentBlockSerializer _contentBlockSerializer;
        private readonly IContentBlockModelConverter _contentBlockModelConverter;

        public ContentBlockService(IContentBlockSerializer contentBlockSerializer, IContentBlockModelConverter contentBlockModelConverter)
        {
            _contentBlockSerializer = contentBlockSerializer;
            _contentBlockModelConverter = contentBlockModelConverter;
        }

        public IEnumerable<ContentBlockModel> GetContentBlocks(string fileUri)
        {
            var document = XDocument.Load(fileUri);
            if (document.Root == null || document.Root.IsEmpty)
                return Enumerable.Empty<ContentBlockModel>();

            var pageWidth = double.Parse(document.Root.Attribute("WIDTH").Value);
            var pageHeight = double.Parse(document.Root.Attribute("HEIGHT").Value);

            return _contentBlockSerializer.Deserialize(document).Select(item => _contentBlockModelConverter.ConvertToModel(item, pageWidth, pageHeight));
        }
    }
}