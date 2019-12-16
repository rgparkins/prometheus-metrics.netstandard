using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace rgparkins.PrometheusMetrics.Netstandard.Microsoft.Tests.fixtures
{
    public class context : IDisposable
    {
        private IWebHost _host;
        protected IServiceProvider Container => _host.Services;

        private MetricFactory factory => Container.GetRequiredService<MetricFactory>();
        

        protected void Given_the_metrics_module_is_registered<T>(T storeToUse) where T : IMetricStore
        {
            var builder = new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureServices(services =>
                {
                    services.RegisterPrometheusMetrics(storeToUse);
                });

            _host = builder.Build();
        }
        
        
        protected void Given_a_new_metrics_logging_file()
        {
            File.Delete(Path.Join(Directory.GetCurrentDirectory(), "metrics.log"));
        }
        
        protected void When_logging_a_counter<T>(string metricName, T labels)
        {
            factory.CreateCounter(metricName).Log(labels);
        }
        
        public void Dispose()
        {
            _host?.Dispose();
        }
    }
}