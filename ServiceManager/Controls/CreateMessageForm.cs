using System;
using System.Windows.Forms;
using ServiceManager.Model;

namespace ServiceManager.Controls
{
    public partial class CreateMessageForm : Form
    {
        public CreateMessageForm()
        {
            InitializeComponent();
        }

        public ServiceQueue ServiceQueue
        {
            get { return createMessageControl1.ServiceQueue; }
            set 
            { 
                createMessageControl1.ServiceQueue = value;
                Text = "Create Message for " + ServiceQueue.Service.ServiceName;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            var message = createMessageControl1.CreateMessage();
            if (message == null)
            {
                MessageBox.Show("Error creating message");
                return;
            }

            ServiceQueue.SendMessage(message);

            Close();
        }
    }
}
