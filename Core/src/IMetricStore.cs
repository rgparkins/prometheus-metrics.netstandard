using System.Threading.Tasks;
using rgparkins.PrometheusMetrics.Netstandard.Serialisation;

namespace rgparkins.PrometheusMetrics.Netstandard
{
    public interface IMetricStore
    {
        void Init();
        Task Log(Metric metric);
    }
}