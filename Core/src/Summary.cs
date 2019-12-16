using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using rgparkins.PrometheusMetrics.Netstandard.Serialisation;

namespace rgparkins.PrometheusMetrics.Netstandard
{
    public class Summary : MetricPayload
    {
        public long Value { get; set; }
        public override string Type => "summary";

        protected Summary()
        {
        }
        
        public Summary(IMetricStore store, string metricName) : base(store, metricName)
        {
            
        }
        
        public void Log<T>(T labels, long value)
        {
            var dictionaryLabels = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(labels));
            
            AddLabels(dictionaryLabels);

            Value = value;
            Timestamp = DateTime.Now;
            
            _store.Log(new Metric
            {
                Data = this
            });
        }
    }
}