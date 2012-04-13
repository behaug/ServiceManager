using System;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Xml.Linq;

namespace ServiceManager.Model
{
    public class ServiceMessage
    {
        private readonly Message _message;

        public ServiceMessage(Message message, ServiceQueue serviceQueue)
        {
            ServiceQueue = serviceQueue;
            _message = message;
        }

        public string Label 
        {
            get { return _message.Label; }
        }

        public string Id
        {
            get { return _message.Id; }
        }

        public ServiceQueue ServiceQueue { get; private set; }

        public ServiceQueue FailedQueue
        {
            get
            {
                if (!Label.Contains("<FailedQ>"))
                    return null;

                try
                {
                    var xml = XDocument.Parse("<Label>" + Label + "</Label>");
                    return ServiceQueue.FromName(xml.Root.Element("FailedQ").Value, null);
                }
                catch
                {
                    return null;
                }
            }
        }

        public string GetBody()
        {
            var r = new StreamReader(_message.BodyStream, Encoding.UTF8);
            try
            {
                return XDocument.Load(r).ToString();
            }
            finally
            {
                _message.BodyStream.Position = 0;
            }
        }

        public XDocument GetBodyXml()
        {
            var r = new StreamReader(_message.BodyStream, Encoding.UTF8);
            try
            {
                return XDocument.Load(r);
            }
            finally
            {
                _message.BodyStream.Position = 0;
            }
        }

        public string Namespace 
        { 
            get
            {
                try
                {
                    var xml = GetBodyXml();
                    return new Uri(xml.Root.Attribute("xmlns").Value).GetComponents(UriComponents.Path, UriFormat.Unescaped);
                }
                catch (Exception)
                {
                    return "";
                }
            }            
        }

        public string Commands
        {
            get
            {
                try
                {
                    var xml = GetBodyXml();
                    return string.Join(", ", xml.Root.Elements().Select(e => e.Name.LocalName).ToArray());
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public override bool Equals(object obj)
        {
            var serviceMessage = obj as ServiceMessage;
            if (serviceMessage == null)
                return false;

            return Id == serviceMessage.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public void Delete()
        {
            ServiceQueue.DeleteMessage(this);
        }
    }
}