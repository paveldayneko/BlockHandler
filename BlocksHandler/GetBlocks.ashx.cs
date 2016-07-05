using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

using BlocksHandler.Services;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BlocksHandler
{
    public class GetBlocks : IHttpHandler
    {
        private IContentBlockService _contentBlockService { get; set; }

        public GetBlocks()
        {
            //There is no way to use dependency resolver with http handlers.
            _contentBlockService = new ContentBlockService();
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.Charset = Encoding.UTF8.WebName;

            var result = _contentBlockService.GetContentBlocks();

            context.Response.Write(
                JsonConvert.SerializeObject(
                    result,
                    Formatting.Indented,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() })
                );
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}