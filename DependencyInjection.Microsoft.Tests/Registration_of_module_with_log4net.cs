using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using rgparkins.PrometheusMetrics.Netstandard.Microsoft.Tests.fixtures;
using rgparkins.PrometheusMetrics.Netstandard.Stores.log4net;

namespace rgparkins.PrometheusMetrics.Netstandard.Microsoft.Tests
{
    public class Registration_of_module_with_log4net : context
    {
        public Registration_of_module_with_log4net()
        {
            Given_the_metrics_module_is_registered(new Log4netMetricStore());
        }

        [Test]
        public void Building_returns_the_default_Factory()
        {
            var factory = Container.GetRequiredService<MetricFactory>();
            
            Assert.That(factory, Is.Not.Null);
        }
        
        [Test]
        public void Building_returns_the_default_Store()
        {
            var store = Container.GetRequiredService<IMetricStore>();
            
            Assert.That(store, Is.TypeOf<Log4netMetricStore>());
        }
    }
}