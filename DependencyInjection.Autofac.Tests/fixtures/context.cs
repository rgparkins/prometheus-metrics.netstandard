using System;
using System.IO;
using Autofac;

namespace rgparkins.PrometheusMetrics.Netstandard.Autofac.Tests.fixtures
{
    public class context
    {
        private ContainerBuilder bldr;
        protected IContainer Container;

        private MetricFactory factory => Container.Resolve<MetricFactory>();

        protected void Given_the_metrics_module_is_registered<T>(T storeToUse) where T : IMetricStore
        {
            bldr.RegisterMetrics(storeToUse);
        }

        protected void Given_a_new_metrics_logging_file()
        {
            File.Delete(Path.Join(Directory.GetCurrentDirectory(), "metrics.log"));
        }
        
        protected void Given_an_autofac_builder()
        {
            bldr = new ContainerBuilder();
        }

        protected void When_building_the_container()
        {
            Container = bldr.Build();
        }

        protected void When_logging_a_counter<T>(string metricName, T labels)
        {
            factory.CreateCounter(metricName).Log(labels);
        }
    
        protected void When_logging_a_summary<T>(string metricName, long value, T labels)
        {
            factory.CreateSummary(metricName).Log(labels, value);
        }
    }
}