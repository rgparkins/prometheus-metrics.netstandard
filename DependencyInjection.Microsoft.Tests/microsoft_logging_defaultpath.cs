using System.IO;
using System.Linq;
using NUnit.Framework;
using rgparkins.PrometheusMetrics.Netstandard.Microsoft.Tests.fixtures;
using rgparkins.PrometheusMetrics.Netstandard.Stores.log4net;

namespace rgparkins.PrometheusMetrics.Netstandard.Microsoft.Tests
{
    public class microsoft_logging_defaultpath : context
    {
        public microsoft_logging_defaultpath()
        {
            Given_a_new_metrics_logging_file();
            
            Given_the_metrics_module_is_registered(new Log4netMetricStore());

            When_logging_a_counter("metric_a",
                new
                {
                    newlabel = 100
                });
        }

        [Test]
        public void Metric_is_logged_as_json_line()
        {
            var lines = File.ReadLines(Path.Join(Directory.GetCurrentDirectory(), "metrics.log"));

            Assert.That(lines.Count(), Is.EqualTo(1));
        }
    }
}