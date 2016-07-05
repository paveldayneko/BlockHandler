using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Web;
using BlocksHandler.Domain;
using BlocksHandler.Model;
using System.Xml.Serialization;

namespace BlocksHandler.Services
{
    public class ContentBlockService : IContentBlockService
    {
        private string _fileUri = AppDomain.CurrentDomain.BaseDirectory + "/Pg001.xml";
        public List<ContentBlockModel> GetContentBlocks()
        {
            var document = new XDocument();

            document = XDocument.Load(_fileUri);

            var blocks = GetBlocks(document);

            double PageWidth = double.Parse(document.Root.Attribute("WIDTH").Value);
            double PageHeight = double.Parse(document.Root.Attribute("HEIGHT").Value);

            return ConvertToModel(blocks, PageWidth, PageHeight);
        }

        private List<ContentBlock> GetBlocks(XDocument document)
        {
            var result = new List<ContentBlock>();

            var elemnts = document.Descendants("Block").ToList();
            var xmlSerializer = new XmlSerializer(typeof(ContentBlock));

            foreach (var xElement in elemnts)
            {
                var item = (ContentBlock)xmlSerializer.Deserialize(xElement.CreateReader());
                result.Add(item);
            }

            return result;
        }

        private List<ContentBlockModel> ConvertToModel(List<ContentBlock> source, double pageWidth, double pageHeight)
        {
            var result = new List<ContentBlockModel>();
            foreach (var item in source)
            {
                var model = new ContentBlockModel { Id = item.Id };
                model.Left = item.Coordinates[0] * 100 / pageWidth;
                model.Top = item.Coordinates[1] * 100 / pageHeight;
                model.Width = (item.Coordinates[2] - item.Coordinates[0]) * 100 / pageWidth;
                model.Height = (item.Coordinates[3] - item.Coordinates[1]) * 100 / pageHeight;
                result.Add(model);
            }

            return result;
        }

    }
}