using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;
using rgparkins.PrometheusMetrics.Netstandard.Serialisation;

namespace rgparkins.PrometheusMetrics.Netstandard.Stores.log4net
{
    public class Log4netMetricStore : IMetricStore
    {
        private readonly string _path;

        public Log4netMetricStore(): this(Path.Join(Directory.GetCurrentDirectory(), "metrics.log"))
        {
        }
        
        public Log4netMetricStore(string path)
        {
            _path = path;
        }

        public void Init()
        {
            Logger.Setup(_path);
        }

        public Task Log(Metric metric)
        {
            var logger = LogManager.GetLogger("log4net", MethodBase.GetCurrentMethod().DeclaringType);
            
            logger.Info(JsonConvert.SerializeObject(metric, JsonSettings.Default));

            return Task.FromResult(0);
        }
    }
}