using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Messaging;
using NServiceBus;

namespace ServiceManager.Model
{
    public enum ServiceQueueType
    {
        Input,
        Error,
        Journal
    }

    public class ServiceQueue
    {
        private PerformanceCounter _queueLengthCounter;
        private bool _useJournal;

        public ServiceQueue(string queuePath, Service service, ServiceQueueType queueType = ServiceQueueType.Input, ServiceQueue parentQueue = null)
        {
            ParentQueue = parentQueue;
            QueueType = queueType;
            Path = queuePath;
            Service = service;
            Name = GetQueueName(queuePath);
            _queueLengthCounter = new PerformanceCounter("MSMQ Queue", "Messages in Queue", Path, true);

            TestIfQueueIsValid();
        }

        public static ServiceQueue FromName(string queueName, Service service, ServiceQueueType queueType = ServiceQueueType.Input)
        {
            return new ServiceQueue(GetQueuePath(queueName), service, queueType);
        }

        public Service Service { get; private set; }
        public string Path { get; private set; }
        public string Name { get; private set; }
        public ServiceQueueType QueueType { get; private set; }
        public bool Invalid { get; private set; }
        public string InvalidMessage { get; private set; }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            var queue = obj as ServiceQueue;
            if (queue == null)
                return false;

            return Path.ToLower() == queue.Path.ToLower();
        }

        public override int GetHashCode()
        {
            return Path.GetHashCode();
        }

        public bool UseJournal
        {
            get { return _useJournal; }
        }

        public ServiceQueue ParentQueue { get; private set; }

        [DebuggerNonUserCode]
        private void TestIfQueueIsValid()
        {
            try
            {
                using (var q = new MessageQueue(Path, QueueAccessMode.Peek))
                {
                    q.GetMessageEnumerator2().Dispose();

                    _useJournal = !Path.EndsWith(";journal") && q.UseJournalQueue;
                }

                Invalid = false;
            }
            catch (Exception ex)
            {
                Invalid = true;
                InvalidMessage = ex.Message;
            }
        }

        public ServiceMessage[] PeekMessages(bool unfiltered = false)
        {
            if (Invalid)
                return new ServiceMessage[0];

            try
            {
                using (var q = new MessageQueue(Path, QueueAccessMode.Peek))
                {
                    q.MessageReadPropertyFilter.DestinationQueue = true;
                    var messages = q.GetAllMessages().Select(m => new ServiceMessage(m, this));

                    if (QueueType == ServiceQueueType.Error && !unfiltered)
                        messages = messages.Where(m => Service.InputQueue.Equals(m.FailedQueue));

                    return messages.ToArray();
                }
            }
            catch (Exception ex)
            {
                Invalid = true;
                InvalidMessage = ex.Message;
                return new ServiceMessage[0];
            }
        }

        static string GetQueuePath(string queueName)
        {
            var machineName = Environment.MachineName;
            if (queueName.Contains("@"))
            {
                machineName = queueName.Substring(queueName.IndexOf("@") + 1);
                queueName = queueName.Substring(0, queueName.IndexOf("@"));
            }

            return String.Format(@"{0}\private$\{1}", machineName, queueName);
        }

        static string GetJournalQueuePath(string queueName)
        {
            var machineName = Environment.MachineName;
            if (queueName.Contains("@"))
            {
                machineName = queueName.Substring(queueName.IndexOf("@") + 1);
                queueName = queueName.Substring(0, queueName.IndexOf("@"));
            }

            //if (machineName.ToUpper() == Environment.MachineName.ToUpper())
            //    machineName = ".";

            return String.Format(@"{0}\private$\{1};journal", machineName, queueName);
        }

        static string GetQueueName(string queuePath)
        {
            string name = queuePath;
            string machine = "";

            if (queuePath.Contains("\\"))
            {
                name = queuePath.Substring(queuePath.LastIndexOf("\\") + 1);
                machine = queuePath.Substring(0, queuePath.IndexOf("\\"));
            }

            string queueName = name;
            if (machine != "" && machine != Environment.MachineName)
                queueName += "@" + machine;

            return queueName;
        }

        public void SendMessage(IMessage message)
        {
            Global.Bus.Send(Name, message);
        }

        public void DeleteMessage(ServiceMessage serviceMessage)
        {
            using (var q = new MessageQueue(Path, QueueAccessMode.Receive))
            {
                q.ReceiveById(serviceMessage.Id);
            }
        }

        public void ReturnMessagesToSourceQueue(IEnumerable<ServiceMessage> messages)
        {
            var returner = new NServiceBus.Tools.Management.Errors.ReturnToSourceQueue.Class1 {InputQueue = Name};

            foreach (var message in messages)
                returner.ReturnMessageToSourceQueue(message.Id);
        }

        public void ReturnAllMessagesToSourceQueue(bool unfiltered = false)
        {
            ReturnMessagesToSourceQueue(PeekMessages(unfiltered));
        }

        public int GetMessageCount()
        {
            if (QueueType == ServiceQueueType.Journal)
                return -1;

            if (QueueType == ServiceQueueType.Input)
                return GetMessageCountFromPerfCounter();

            return GetMessageCountFromQueue();
        }

        [DebuggerNonUserCode]
        private int GetMessageCountFromQueue()
        {
            if (Invalid)
                return -1;

            try
            {
                return PeekMessages().Count();
            }
            catch (Exception ex)
            {
                Invalid = true;
                InvalidMessage = ex.Message;
                return -1;
            }
        }

        [DebuggerNonUserCode]
        private int GetMessageCountFromPerfCounter()
        {
            if (_queueLengthCounter == null)
                return GetMessageCountFromQueue();

            try
            {
                return (int)_queueLengthCounter.NextValue();
            }
            catch (Exception)
            {
                _queueLengthCounter = null;
                return GetMessageCountFromQueue();
            }
        }

        public ServiceQueue GetJournalQueue()
        {
            return new ServiceQueue(GetJournalQueuePath(Name), Service, ServiceQueueType.Journal, this);
        }
    }
}