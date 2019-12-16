using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace rgparkins.PrometheusMetrics.Netstandard.Stores.log4net
{
    internal static class Logger
    {
        public static void Setup(string path)
        {
            Hierarchy hierarchy;

            if (LoggerManager.RepositorySelector.ExistsRepository("log4net"))
            {
                hierarchy = (Hierarchy) LogManager.GetRepository("log4net");
            }
            else
            {
                hierarchy = (Hierarchy) LogManager.CreateRepository("log4net");    
            }

            var patternLayout = new PatternLayout
            {
                ConversionPattern = "%message%newline"
            };
            
            patternLayout.ActivateOptions();

            var roller = new RollingFileAppender
            {
                AppendToFile = true,
                File = path,
                Layout = patternLayout,
                MaxSizeRollBackups = 5,
                MaximumFileSize = "1GB",
                RollingStyle = RollingFileAppender.RollingMode.Size,
                StaticLogFileName = true
            };

            roller.ActivateOptions();
            
            hierarchy.Root.AddAppender(roller);
            hierarchy.Root.Level = Level.Info;
            hierarchy.Configured = true;
        }
    }
}