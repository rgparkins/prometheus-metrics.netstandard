# net standard metrics for prometheus

This library is the net standard library for prometheus metrics. Currently supporting Counters and Summaries, it 
also supports the following nuget packages:

* Core package for creating metrics with DI
* Autofac package that allows the consumer to register a Module against a container.
* Microsoft DI package that allows the registeration against the IServiceCollection
* A console target store
* A log4net target store

As a minimum you will need to nuget up the Core and the store package you wish to utilise.

# Autofac example


```
var bldr = new ContainerBuilder();
            
bldr.RegisterMetrics(new Log4netStore()); // Can be the `ConsoleMetricStore`

var container = bldr.Build();

var factory = container.Resolve<MetricFactory>();

var summary = factory.CreateSummary("http_latency");

summary.Log(new
{
    label_a = "a test"
}, 3452);
```

# IServiceCollection example

```
var builder = new WebHostBuilder()
    .UseStartup<Startup>()
    .ConfigureServices(services =>
    {
        services.RegisterPrometheusMetrics(new Log4netStore()); // Can be the `ConsoleMetricStore`
    });

_host = builder.Build();

var factory = _host.Services.GetRequiredService<MetricFactory>();

var summary = factory.CreateSummary("http_latency");

summary.Log(new
{
    label_a = "a test"
}, 3452);
```
           
# Log4net store

This store will utilise the log4net rolling file appender. As a default the path pf the file is the current executing 
directory of the program and `metrics.log` as a file. The file will roll over after 1G

You can optionally pass a full path to the store

# Console store

The store will log to console