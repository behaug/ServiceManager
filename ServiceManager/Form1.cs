using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NServiceBus;
using ServiceManager.Model;
using ServiceManager.Properties;

namespace ServiceManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Global.Bus = NServiceBus.Configure.With(new Type[] { })
                .DefaultBuilder()
                .XmlSerializer()
                .MsmqTransport()
                    .IsTransactional(false)
                    .PurgeOnStartup(false)
                    .DoNotCreateQueues()
                .UnicastBus()
                    .ImpersonateSender(false)
                    .DoNotAutoSubscribe()
                .CreateBus()
                .Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Global.SyncContext = SynchronizationContext.Current;

            if (Settings.Default.Services != null)
            {
                foreach (var service in Settings.Default.Services)
                    serviceTree.AddServiceNode(service);
            }

            Global.BackgroundTimer.Start(1000);
        }

        private void serviceTree_QueueSelected(ServiceQueue serviceQueue)
        {
            queueOverviewControl1.ShowQueue(serviceQueue);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.BackgroundTimer.Stop();

            Settings.Default.Services = new StringCollection();
            Settings.Default.Services.AddRange(serviceTree.Services.Select(s => s.ConfigPath).ToArray());
            Settings.Default.Save();
        }
    }
}
