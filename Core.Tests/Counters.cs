using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using rgparkins.PrometheusMetrics.Netstandard.Tests.fixtures;

namespace rgparkins.PrometheusMetrics.Netstandard.Tests
{
    public class Counters : context
    {
        public Counters()
        {
            Given_a_metric_factory();

            When_logging_a_counter("metric_a",
                new
                {
                    newlabel = 100
                });
        }

        [Test]
        public void Metric_is_logged()
        {
            Assert.That(Store.Logs.Count, Is.EqualTo(1));
        }
        
        [Test]
        public void Metric_contains_all_data()
        {
            var counter = JObject.Parse(Store.Logs.First());
            
            Assert.That(counter["metric"]["type"].Value<string>(), Is.EqualTo("counter"));
            Assert.That(counter["metric"]["value"].Value<int>, Is.EqualTo(1));
            Assert.That(counter["metric"]["name"].Value<string>(), Is.EqualTo("metric_a"));

            //Assert.That(counter["metric"]["timestamp"].ToString(), . Contains("Z");
            var labels = counter["metric"]["labels"].ToObject<Dictionary<string, object>>();
            Assert.That(labels.Count, Is.EqualTo(1));
            Assert.That(labels["newlabel"], Is.EqualTo(100));
        }
        
        [Test]
        public void Metric_contains_correct_date_format()
        {
            var regex = new Regex(@"(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2}).(\d{3})");
            var result = regex.Match(Store.Logs.First());
            
            Assert.That(result.Length, Is.GreaterThan(0));
        }
    }
}