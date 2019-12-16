using Newtonsoft.Json;

namespace rgparkins.PrometheusMetrics.Netstandard.Serialisation
{
    public class Metric
    {
        [JsonProperty("metric")]
        public MetricPayload Data { get; set; }
    }
}