using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using ServiceManager.Model;

namespace ServiceManager.Controls
{
    public partial class QueueOverviewControl : UserControl
    {
        private ServiceQueue _serviceQueue;
        private bool _unfiltered;

        public QueueOverviewControl()
        {
            InitializeComponent();
            Global.BackgroundTimer.Tick += BackgroundTimer_Tick;

            ShowQueue(null);
        }

        public void ShowQueue(ServiceQueue serviceQueue)
        {
            _serviceQueue = serviceQueue;

            Enabled = (_serviceQueue != null) && !_serviceQueue.Invalid;

            bool isErrorQueue = (serviceQueue != null) && serviceQueue.QueueType == ServiceQueueType.Error;

            colSourceQueue.Visible = isErrorQueue;
            returnAllToSourceButton.Visible = isErrorQueue;
            returnToSourceButton.Visible = isErrorQueue;
            createMessageButton.Visible = _serviceQueue != null && serviceQueue.QueueType == ServiceQueueType.Input;
            chkShowAll.Visible = isErrorQueue;
            
            ReloadMessages();
        }

        private void ReloadMessages()
        {
            if (_serviceQueue == null)
                return;

            if (_serviceQueue.Invalid)
            {
                messagesGrid.Rows.Clear();
                errorLabel.Text = _serviceQueue.InvalidMessage;
                errorLabel.Visible = true;
                return;
            }

            try
            {
                errorLabel.Visible = false;
                messageDetailsControl1.Clear();
                messagesGrid.ClearSelection();
                messagesGrid.Rows.Clear();

                var messages = _serviceQueue.PeekMessages(_unfiltered);

                foreach (var message in messages)
                    AddMessageRow(message);

                if (messagesGrid.Rows.Count != 0)
                {
                    messagesGrid.ClearSelection();
                    messagesGrid.Rows[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                messagesGrid.Rows.Clear();
                errorLabel.Text = ex.Message;
                errorLabel.Visible = true;
            }
        }

        private void AddMessageRow(ServiceMessage message)
        {
            int i = messagesGrid.Rows.Add(message.Commands, message.Namespace, message.FailedQueue);
            messagesGrid.Rows[i].Tag = message;
        }

        void BackgroundTimer_Tick()
        {
            PollMessages();
        }

        private void PollMessages()
        {
            if (_serviceQueue == null)
                return;

            try
            {
                var messages = _serviceQueue.PeekMessages(_unfiltered);

                var existingMessages = Messages;
                var addedMessages = messages.Except(existingMessages).ToList();
                var removedMessages = existingMessages.Except(messages).ToList();

                foreach (var message in removedMessages)
                {
                    var removedRow = FindMessageRow(message);
                    //removedRow.DefaultCellStyle.ForeColor = Color.Gray;
                    messagesGrid.Rows.Remove(removedRow);
                }

                foreach (var message in addedMessages)
                    AddMessageRow(message);
            }
            catch {}
        }

        DataGridViewRow FindMessageRow(ServiceMessage message)
        {
            return messagesGrid.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => r.Tag == message);
        }

        private List<ServiceMessage> Messages
        {
            get { return messagesGrid.Rows.Cast<DataGridViewRow>().Select(r => (ServiceMessage)r.Tag).ToList(); }
        }

        private List<ServiceMessage> SelectedMessages
        {
            get { return messagesGrid.SelectedRows.Cast<DataGridViewRow>().Select(r => (ServiceMessage)r.Tag).ToList(); }
        }

        private void messagesGrid_SelectionChanged(object sender, EventArgs e)
        {
            var message = SelectedMessages.FirstOrDefault();
            if (message == null)
                return;

            messageDetailsControl1.ShowMessageDetails(message);
        }

        private void returnToSourceButton_Click(object sender, EventArgs e)
        {
            _serviceQueue.ReturnMessagesToSourceQueue(SelectedMessages);
            ReloadMessages();
        }

        private void returnAllToSourceButton_Click(object sender, EventArgs e)
        {
            _serviceQueue.ReturnAllMessagesToSourceQueue(_unfiltered);
            ReloadMessages();
        }

        private void createMessageButton_Click(object sender, EventArgs e)
        {
            var form = new CreateMessageForm {ServiceQueue = _serviceQueue};
            form.ShowDialog();
        }

        private void messagesGrid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (_serviceQueue == null || _serviceQueue.QueueType == ServiceQueueType.Journal)
                    return;

                var selectedMessages = SelectedMessages;
                if (selectedMessages.Count == 0)
                    return;

                string numMessages = (selectedMessages.Count > 1) ? selectedMessages.Count + " messages" : "the message";
                if (MessageBox.Show("This will delete " + numMessages, "Delete Messages", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;

                foreach (var message in selectedMessages)
                {
                    var row = FindMessageRow(message);
                    message.Delete();
                    messagesGrid.Rows.Remove(row);
                }
            }
        }

        private void chkShowAll_CheckedChanged(object sender, EventArgs e)
        {
            _unfiltered = chkShowAll.Checked;
            ReloadMessages();
        }
    }
}
