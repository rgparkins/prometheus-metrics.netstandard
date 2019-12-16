namespace rgparkins.PrometheusMetrics.Netstandard.Tests.fixtures
{
    public class context
    {
        MetricFactory factory;
        Summary summariesInstance;
        Counter counterInstance;
        protected InMemoryMetricStore Store = new InMemoryMetricStore();
        
        protected void When_logging_a_counter<T>(string metricName, T labels)
        {
            counterInstance = factory.CreateCounter(metricName);
            
            counterInstance.Log(labels);
        }
        
        protected void When_logging_a_summary<T>(string metricName, long value, T labels)
        {
            summariesInstance = factory.CreateSummary(metricName);
            
            summariesInstance.Log(labels, value);
        }
        
        protected void Given_a_metric_factory()
        {
            Store.Init();
            factory = new MetricFactory(Store);
        }
    }
}