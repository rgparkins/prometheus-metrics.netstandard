namespace rgparkins.PrometheusMetrics.Netstandard
{
    public class MetricFactory
    {
        private readonly IMetricStore _store;

        public MetricFactory(IMetricStore store)
        {
            _store = store;
        }
        
        public Counter CreateCounter(string name)
        {
            return new Counter(_store, name);
        } 
        
        public Summary CreateSummary(string name)
        {
            return new Summary(_store, name);
        } 
    }
}