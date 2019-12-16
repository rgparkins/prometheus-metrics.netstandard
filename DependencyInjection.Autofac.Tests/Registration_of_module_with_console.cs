using Autofac;
using NUnit.Framework;
using rgparkins.PrometheusMetrics.Netstandard.Autofac.Tests.fixtures;
using rgparkins.PrometheusMetrics.Netstandard.Stores.Console;

namespace rgparkins.PrometheusMetrics.Netstandard.Autofac.Tests
{
    public class Registration_of_module_with_console : context
    {
        public Registration_of_module_with_console()
        {
            Given_an_autofac_builder();

            Given_the_metrics_module_is_registered(new ConsoleMetricStore());

            When_building_the_container();
        }

        [Test]
        public void Building_returns_the_default_Factory()
        {
            var factory = Container.Resolve<MetricFactory>();
            
            Assert.That(factory, Is.Not.Null);
        }
        
        [Test]
        public void Building_returns_the_default_Store()
        {
            var store = Container.Resolve<IMetricStore>();
            
            Assert.That(store, Is.TypeOf<ConsoleMetricStore>());
        }
    }
}