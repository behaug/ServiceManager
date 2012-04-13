using System;
using System.Windows.Forms;
using ServiceManager.Model;

namespace ServiceManager.Controls
{
    public partial class MessageDetailsControl : UserControl
    {
        public MessageDetailsControl()
        {
            InitializeComponent();
        }

        public void ShowMessageDetails(ServiceMessage message)
        {
            messageIdText.Text = message.Id;
            messageLabelText.Text = message.Label;

            try
            {
                bodyText.Text = message.GetBody();
            }
            catch (Exception ex)
            {
                bodyText.Text = "** " + ex.Message;
            }
        }

        public void Clear()
        {
            bodyText.Text = "";
            messageIdText.Text = "";
            messageLabelText.Text = "";
        }
    }
}
