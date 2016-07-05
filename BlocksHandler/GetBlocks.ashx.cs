using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

using BlocksHandler.Services;
using BlocksHandler.Model;

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

            string response =string.Empty;
            try {

                var result = _contentBlockService.GetContentBlocks();
                response = ConverteToJson(result);
            }
            catch(Exception e)
            {
                response = ConverteToJson(new { Message = e.Message });
                context.Response.StatusCode = 500;
            }
            finally
            {
                context.Response.Write(response);
            }

           
        }

        private string ConverteToJson(object obj)
        {
            var result = JsonConvert.SerializeObject(
                      obj,
                      Formatting.Indented,
                      new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return result;
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