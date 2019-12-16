using System;
using System.Collections.Generic;
using System.Linq;

namespace rgparkins.PrometheusMetrics.Netstandard.Serialisation
{
    public class MetricPayload
    {
        protected readonly IMetricStore _store;

        public virtual string Type { get; set; }
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }
        public Dictionary<string, object> Labels { get; set; }

        protected MetricPayload()
        {
        }
        
        protected MetricPayload(IMetricStore store, string metricName)
        {
            _store = store;
            Name = metricName;
            Timestamp = DateTime.Now;
            Labels = new Dictionary<string, object>();
        }

        protected void AddLabels(Dictionary<string, object> extraLabels)
        {
            Labels = Labels.Concat(extraLabels)
                .ToDictionary(x=>x.Key,x=>x.Value);
        }
    }
}