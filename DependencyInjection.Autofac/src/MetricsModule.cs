using Autofac;

namespace rgparkins.PrometheusMetrics.Netstandard.Autofac
{
    public class MetricsModule : Module
    {
        private readonly IMetricStore _storeToUse;

        public MetricsModule(IMetricStore storeToUse)
        {
            _storeToUse = storeToUse;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(_ => _storeToUse)
                .AsImplementedInterfaces()
                .SingleInstance()
                .OnActivated(h => h.Instance.Init());
            
            builder.Register(_ => new MetricFactory(_.Resolve<IMetricStore>()))
                .AsSelf()
                .SingleInstance();
        }
    }
}