using System;
using System.Threading;
using log4net;

namespace TopshelfHostedService
{
    public class TownCrier
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(TownCrier));

        private readonly object _lock = new object();
        private readonly Timer _timer;

        public TownCrier()
        {
            _timer = new Timer(_ => Cry(), null, Timeout.Infinite, Timeout.Infinite);
        }

        private void Cry()
        {
            lock (_lock)
            {
                try
                {
                    _log.Info($"It is {DateTime.Now} and all is well");
                }
                finally
                {
                    _timer.Change(TimeSpan.FromSeconds(1), Timeout.InfiniteTimeSpan);
                }
            }
        }

        public void Start()
        {
            lock (_lock)
            {
                _timer.Change(TimeSpan.FromSeconds(1), Timeout.InfiniteTimeSpan);
            }
        }

        public void Stop()
        {
            lock (_lock)
            {
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
            }
        }
    }
}