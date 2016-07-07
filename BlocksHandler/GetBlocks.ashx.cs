using System;
using System.Web;
using System.Text;
using BlocksHandler.Converters.Abstraction;
using BlocksHandler.Converters;
using BlocksHandler.Services.Abstraction;
using BlocksHandler.Services;

namespace BlocksHandler
{
    public class GetBlocks : IHttpHandler
    {
        private readonly IContentBlockService _contentBlockService;
        private readonly IJsonConverter _jsonConverter;

        public GetBlocks()
        {
            //There is no way to use dependency resolver with http handlers.
            _contentBlockService = new ContentBlockService(new ContentBlockSerializer(), new ContentBlockModelConverter());
            _jsonConverter = new JsonConverter();
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = Constants.HttpResponseJsonContentType;
            context.Response.Charset = Encoding.UTF8.WebName;

            
           

            var response = string.Empty;
            try
            {
                var result = _contentBlockService.GetContentBlocks(Constants.InputXmlFileUri);
                response = _jsonConverter.ConverteToJson(result);
            }
            catch (Exception e)
            {
                response = _jsonConverter.ConverteToJson(new { e.Message });
                context.Response.StatusCode = Constants.InternalServerErrorCode;
            }
            finally
            {
                var callback = context.Request["callback"];
                if (!string.IsNullOrEmpty(callback))
                {
                    // if the callback parameter is present wrap the JSON
                    // into this parameter => convert to JSONP
                    response = string.Format("{0}({1})", callback, response);
                }
                context.Response.Write(response);
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}