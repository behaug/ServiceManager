using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using NServiceBus;
using NServiceBus.MessageInterfaces.MessageMapper.Reflection;
using NServiceBus.Serializers.XML;
using ServiceManager.Model;

namespace ServiceManager.Controls
{
    public partial class CreateMessageControl : UserControl
    {
        private readonly MessageSerializer _serializer;

        public CreateMessageControl()
        {
            InitializeComponent();

            _serializer = new NServiceBus.Serializers.XML.MessageSerializer();
            propertyGrid1.GotFocus += PropertyGridChanged;
        }

        public ServiceQueue ServiceQueue { get; set; }

        private void CreateMessageControl_Load(object sender, EventArgs e)
        {
            if (ServiceQueue == null)
                return;

            var messages = ServiceQueue.Service.GetAcceptedMessageTypes();
            messageTypeList.DisplayMember = "FullName";
            messageTypeList.DataSource = messages;
        }

        private void messageTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var messageType = messageTypeList.SelectedItem as Type;
            if (messageType == null)
                return;

            var mapper = new MessageMapper();
            mapper.Initialize(new[] { messageType });
            _serializer.MessageMapper = mapper;
            _serializer.InitType(messageType);

            var message = mapper.CreateInstance(messageType);
            InitializeProperties(message);

            propertyGrid1.SelectedObject = message;
            bodyText.Text = SerializeMessage(message);
        }

        private void InitializeProperties(object obj)
        {
            foreach (var prop in obj.GetType().GetProperties())
            {
                if (prop.PropertyType.IsValueType)
                    continue;

                if (!prop.CanRead || !prop.CanWrite || prop.GetIndexParameters().Length != 0)
                    continue;

                if (prop.GetValue(obj, null) != null)
                    continue;

                object value = null;

                if (prop.PropertyType == typeof(string))
                {
                    value = "";
                }
                else
                {
                    try
                    {
                        value = Activator.CreateInstance(prop.PropertyType);
                        InitializeProperties(value);
                    }
                    catch (Exception)
                    { }
                }

                prop.SetValue(obj, value, null);
            }
        }

        private string SerializeMessage(object message)
        {
            var ms = new MemoryStream();
            _serializer.Serialize(new[] { (IMessage)message }, ms);
            ms.Position = 0;

            var r = new StreamReader(ms);
            return XDocument.Parse(r.ReadToEnd()).ToString();
        }

        private void PropertyGridChanged(object sender, EventArgs e)
        {
            var message = (IMessage)propertyGrid1.SelectedObject;
            InitializeProperties(message);
            propertyGrid1.Refresh();
            int pos = bodyText.SelectionStart;
            bodyText.Text = SerializeMessage(message);
            bodyText.SelectionStart = pos;
        }

        private void bodyText_TextChanged(object sender, EventArgs e)
        {
            var message = DeserializeMessage(bodyText.Text);
            if (message == null)
                return;

            propertyGrid1.SelectedObject = message;
        }

        private IMessage DeserializeMessage(string xml)
        {
            try
            {
                var ms = new MemoryStream(Encoding.UTF8.GetBytes(xml));
                var messages = _serializer.Deserialize(ms);
                return messages[0];
            }
            catch
            {
                return null;
            }
        }

        public IMessage CreateMessage()
        {
            return DeserializeMessage(bodyText.Text);
        }
    }
}
