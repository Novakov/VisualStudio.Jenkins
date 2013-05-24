using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Jenkins;

namespace JenkinsBuilds
{
    public class BackgroundJenkinsMonitor : JenkinsMonitor
    {
        private Thread thread;        

        public BackgroundJenkinsMonitor(Uri baseUri)
            : base(baseUri)
        {            
            this.thread = new Thread(ThreadMethod);
            this.thread.Name = "Polling thread for " + baseUri.ToString();
        }

        public void Start()
        {
            this.thread.Start();
        }

        public void Stop()
        {
            this.thread.Abort();
        }

        private void ThreadMethod()
        {
            try
            {
                while (this.thread.IsAlive)
                {
                    this.Poll();
                    Thread.Sleep(2000);
                }
            }
            catch (ThreadAbortException)
            {
                return;
            }
        }
    }
}
