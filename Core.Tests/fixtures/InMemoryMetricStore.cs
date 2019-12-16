using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using rgparkins.PrometheusMetrics.Netstandard.Serialisation;

namespace rgparkins.PrometheusMetrics.Netstandard.Tests.fixtures
{
    public class InMemoryMetricStore : IMetricStore
    {
        public List<string> Logs { get; set; }

        public void Init()
        {
            Logs = new List<string>();
        }

        public Task Log(Metric metric)
        {
            Logs.Add(JsonConvert.SerializeObject(metric, JsonSettings.Default));

            return Task.CompletedTask;
        }
    }
}