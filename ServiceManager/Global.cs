using System;
using System.Threading;
using NServiceBus;

namespace ServiceManager
{
    static class Global
    {
        public static readonly BackgroundTimer BackgroundTimer = new BackgroundTimer();
        public static IBus Bus;
        public static SynchronizationContext SyncContext { get; set; }

        public static bool InvokeIfRequired(Action action)
        {
            if (!InvokeRequired)
                return false;

            Invoke(action);
            return true;
        }

        public static bool InvokeRequired
        {
            get { return SynchronizationContext.Current != SyncContext; }
        }

        public static void Invoke(Action action)
        {
            SyncContext.Send(state => action(), null);
        }
    }
}
