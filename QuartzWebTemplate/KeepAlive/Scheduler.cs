using System;
using System.Net;
using System.Threading;
using System.Web;

namespace QuartzWebTemplate.KeepAlive
{
    public class Scheduler : IDisposable
    {
        /// <summary>
        /// Determines the status fo the Scheduler
        /// </summary>        
        public bool Cancelled { get; set; }

        /// <summary>
        /// The frequency of checks against hte POP3 box are 
        /// performed in Seconds.
        /// </summary>
        private int CheckFrequency = 180;

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
            CheckFrequency = checkFrequency;
            Cancelled = false;

            var t = new Thread(Run);
            t.Start();
        }

        /// <summary>
        /// Causes the processing to stop. If the operation is still
        /// active it will stop after the current message processing
        /// completes
        /// </summary>
        public void Stop()
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
                _waitHandle.WaitOne(CheckFrequency * 1000, true);
            }   

            while (!Cancelled)
            {
                // *** Http Ping to force the server to stay alive 
                PingServer();

                // *** Put in 
                lock (_syncLock)
                {
                    _waitHandle.WaitOne(CheckFrequency * 1000, true);
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
            catch (Exception ex)
            {
                string message = ex.Message;
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