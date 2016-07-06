using BlocksHandler.Converters.Abstraction;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BlocksHandler.Converters
{
    public class JsonConverter : IJsonConverter
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public JsonConverter()
            : this(new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() })
        {
        }

        public JsonConverter(JsonSerializerSettings jsonSerializerSettings)
        {
            _jsonSerializerSettings = jsonSerializerSettings;
        }

        public string ConverteToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, _jsonSerializerSettings);
        }
    }
}