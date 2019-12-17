using Autofac;

namespace rgparkins.PrometheusMetrics.Netstandard.Autofac
{
    public static class ContainerExtensions
    {
        public static void RegisterMetrics(this ContainerBuilder bldr, IMetricStore storeToUse)
        {
            bldr.RegisterModule(new MetricsModule(storeToUse));
        }
        
        public static void RegisterMetrics(this ContainerBuilder bldr)
        {
            bldr.RegisterModule(c => new MetricsModule(;
        }
    }
}