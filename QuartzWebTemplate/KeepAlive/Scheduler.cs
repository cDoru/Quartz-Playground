using System;
using System.Net;
using System.Threading;

namespace QuartzWebTemplate.KeepAlive
{
    public class Scheduler : IDisposable
    {
        /// <summary>
        /// Determines the status fo the Scheduler
        /// </summary>        
        private bool Cancelled { get; set; }

        /// <summary>
        /// The frequency of checks against hte POP3 box are 
        /// performed in Seconds.
        /// </summary>
        private int _checkFrequency = 180;

        private readonly AutoResetEvent _waitHandle = new AutoResetEvent(false);

        private readonly object _syncLock = new Object();

        public Scheduler()
        {
            Cancelled = false;
        }

        /// <summary>
        /// Starts the background thread processing       
        /// </summary>
        /// <param name="checkFrequency">Frequency that checks are performed in seconds</param>
        public void Start(int checkFrequency)
        {
            _checkFrequency = checkFrequency;
            Cancelled = false;

            var t = new Thread(Run);
            t.Start();
        }

        /// <summary>
        /// Causes the processing to stop. If the operation is still
        /// active it will stop after the current message processing
        /// completes
        /// </summary>
        private void Stop()
        {
            lock (_syncLock)
            {
                if (Cancelled)
                    return;

                Cancelled = true;
                _waitHandle.Set();
            }
        }

        /// <summary>
        /// Runs the actual processing loop by checking the mail box
        /// </summary>
        private void Run()
        {

            // *** Start out  waiting
            lock (_syncLock)
            {
                _waitHandle.WaitOne(_checkFrequency * 1000, true);
            }   

            while (!Cancelled)
            {
                // *** Http Ping to force the server to stay alive 
                PingServer();

                lock (_syncLock)
                {
                    _waitHandle.WaitOne(_checkFrequency * 1000, true);
                }
            }
        }

        // ReSharper disable once MemberCanBeMadeStatic.Global
        public void PingServer()
        {
            try
            {
                var http = new WebClient();
                var path = BasePathHolder.BasePath;
                http.DownloadString(path + KeepAliveConstants.RelativeKeepAlivePath);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Stop();
        }

        #endregion
    }
}