using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using rgparkins.PrometheusMetrics.Netstandard.Serialisation;

namespace rgparkins.PrometheusMetrics.Netstandard
{
    public class Counter : MetricPayload
    {
        public int Value => 1;
        public override string Type => "counter";

        protected Counter()
        {
        }
        
        public Counter(IMetricStore store, string metricName) : base(store, metricName)
        {
            
        }

        public void Log<T>(T labels)
        {
            var dictionaryLabels = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(labels));
            
            AddLabels(dictionaryLabels);

            Timestamp = DateTime.Now;

            _store.Log(new Metric
            {
                Data = this
            });
        }
    }
}