using System;
using log4net;
using Topshelf;

namespace TopshelfHostedService
{
    class Program
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            _log.Info("Starting up...");

            var rc = HostFactory.Run(x =>
            {
                x.Service<TownCrier>(s =>
                {
                    s.ConstructUsing(name => new TownCrier());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                    s.WhenShutdown(tc => tc.Dispose());
                });

                x.RunAsLocalSystem();

                x.SetDescription("TopshelfHostedService");
                x.SetDisplayName("TopshelfHostedService");
                x.SetServiceName("TopshelfHostedService");

                x.UseLog4Net();
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
