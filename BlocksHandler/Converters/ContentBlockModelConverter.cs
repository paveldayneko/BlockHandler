using BlocksHandler.Converters.Abstraction;
using BlocksHandler.Domain;
using BlocksHandler.Model;

namespace BlocksHandler.Converters
{
    public class ContentBlockModelConverter : IContentBlockModelConverter
    {
        public ContentBlockModel ConvertToModel(ContentBlock entity, double pageWidth, double pageHeight)
        {
            return new ContentBlockModel
            {
                Id = entity.Id,
                Left = entity.Coordinates[0] * 100 / pageWidth,
                Top = entity.Coordinates[1] * 100 / pageHeight,
                Width = (entity.Coordinates[2] - entity.Coordinates[0]) * 100 / pageWidth,
                Height = (entity.Coordinates[3] - entity.Coordinates[1]) * 100 / pageHeight
            };
        }
    }
}