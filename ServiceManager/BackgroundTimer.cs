using System;
using System.Windows.Forms;

namespace ServiceManager
{
    class BackgroundTimer
    {
        private System.Threading.Timer _timer;
        
        public event Action Tick;

        public void Start(int interval)
        {
            _timer = new System.Threading.Timer(DoTick);
            _timer.Change(0, interval);
        }

        public void Stop()
        {
            _timer.Dispose();
            Application.DoEvents(); // Process any pending invokes
        }

        private void DoTick(object state)
        {
            var tick = Tick;
            if (tick != null)
                tick();
        }
    }
}
