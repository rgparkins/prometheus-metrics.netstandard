using Autofac;

namespace rgparkins.PrometheusMetrics.Netstandard.Autofac
{
    public static class ContainerExtensions
    {
        public static void RegisterMetrics(this ContainerBuilder bldr, IMetricStore storeToUse)
        {
            bldr.RegisterModule(new MetricsModule(storeToUse));
        }
    }
}