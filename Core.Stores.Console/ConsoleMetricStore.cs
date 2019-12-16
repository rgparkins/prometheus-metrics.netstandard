using System.Threading.Tasks;
using Newtonsoft.Json;
using rgparkins.PrometheusMetrics.Netstandard.Serialisation;

namespace rgparkins.PrometheusMetrics.Netstandard.Stores.Console
{
    public class ConsoleMetricStore : IMetricStore
    {
        public void Init()
        {
            
        }

        public Task Log(Metric metric)
        {
            System.Console.WriteLine(JsonConvert.SerializeObject(metric, JsonSettings.Default));
            return Task.CompletedTask;
        }
    }
}