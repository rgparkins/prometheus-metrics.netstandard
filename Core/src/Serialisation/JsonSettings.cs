using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace rgparkins.PrometheusMetrics.Netstandard.Serialisation
{
    public class JsonSettings
    {
        public static JsonSerializerSettings Default => new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.None,
            TypeNameHandling = TypeNameHandling.None,
            DateFormatHandling = DateFormatHandling.IsoDateFormat
        };
    }
}