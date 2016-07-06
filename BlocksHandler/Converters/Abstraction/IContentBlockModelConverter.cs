using BlocksHandler.Domain;
using BlocksHandler.Model;

namespace BlocksHandler.Converters.Abstraction
{
    public interface IContentBlockModelConverter
    {
        ContentBlockModel ConvertToModel(ContentBlock entity, double pageWidth, double pageHeight);
    }
}