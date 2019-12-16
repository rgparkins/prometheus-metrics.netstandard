using Microsoft.Extensions.DependencyInjection;

namespace rgparkins.PrometheusMetrics.Netstandard.Microsoft
{
    public static class ServiceExtensions
    {
        public static void RegisterPrometheusMetrics(this IServiceCollection services, IMetricStore storeToUse)
        {
            //ouch
            storeToUse.Init();
            
            services.AddSingleton<MetricFactory, MetricFactory>();
            services.AddSingleton(c => storeToUse);
        }
    }
}