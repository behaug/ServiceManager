using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using NServiceBus;

namespace ServiceManager.Model
{
    public class Service
    {
        public Service(string configPath)
        {
            ConfigPath = configPath;
            ServiceName = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(ConfigPath));

            if (!File.Exists(Path.Combine(Path.GetDirectoryName(configPath), "NServiceBus.Host.exe")))
                throw new Exception("NServiceBus.Host.exe not found at the config path");

            var config = XDocument.Load(configPath);
            var el = config.Element("configuration").Element("MsmqTransportConfig");

            InputQueue = ServiceQueue.FromName(el.Attribute("InputQueue").Value, this);
            ErrorQueue = ServiceQueue.FromName(el.Attribute("ErrorQueue").Value, this, ServiceQueueType.Error);
        }

        [DebuggerStepThrough]
        public static Service TryLoad(string configPath)
        {
            try
            {
                return new Service(configPath);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string ConfigPath { get; private set; }
        public string ServiceName { get; private set; }
        public ServiceQueue InputQueue { get; private set; }
        public ServiceQueue ErrorQueue { get; private set; }

        public List<Type> GetAcceptedMessageTypes()
        {
            string dir = Path.GetDirectoryName(ConfigPath);
            var dlls = Directory.GetFiles(dir, "*.dll")
                .Where(f => !Path.GetFileName(f).StartsWith("NServiceBus")) // NSB internals are not interesting to scan
                .Where(f => !Path.GetFileName(f).StartsWith("K2ROM")); // K2ROM causes a hang when loaded

            var types = dlls.SelectMany(dll => ScanForMessageTypes(dll)).OrderBy(t => t.FullName).ToList();
            return types;
        }

        private IEnumerable<Type> ScanForMessageTypes(string assemblyPath)
        {
            try
            {
                var asm = Assembly.LoadFrom(assemblyPath);

                var handlers = asm.GetTypes()
                    .Where(t => t.IsClass)
                    .SelectMany(t => t.GetInterfaces())
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMessageHandler<>));

                var messageTypes = handlers.Select(t => t.GetGenericArguments().First());

                return messageTypes.Distinct();
            }
            catch (Exception)
            {
                return new Type[0];
            }
        }

        public override bool Equals(object obj)
        {
            var service = obj as Service;
            if (service == null)
                return false;

            return service.ConfigPath.ToUpper() == ConfigPath.ToUpper();
        }

        public override int GetHashCode()
        {
            return ConfigPath.GetHashCode();
        }

        public override string ToString()
        {
            return ServiceName;
        }
    }
}