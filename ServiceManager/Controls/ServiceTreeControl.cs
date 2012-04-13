using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ServiceManager.Model;

namespace ServiceManager.Controls
{
    public delegate void QueueSelectedHandler(ServiceQueue serviceQueue);

    public partial class ServiceTreeControl : UserControl
    {
        public event QueueSelectedHandler QueueSelected;

        private readonly ContextMenuStrip _contextMenu = new ContextMenuStrip();
        private readonly ToolStripMenuItem _expandAllItem;
        private readonly ToolStripMenuItem _collapseAllItem;
        private readonly ToolStripMenuItem _removeServiceItem;

        #region Node types
        private class ServiceNode : TreeNode
        {
            private bool _hasMessages;
            private bool _hasErrors;
            private bool _isDirty;

            public ServiceNode(Service service)
            {
                Service = service;
                InputQueueNode = new QueueNode(Service.InputQueue);
                ErrorQueueNode = new QueueNode(Service.ErrorQueue);

                Nodes.Add(InputQueueNode);
                Nodes.Add(ErrorQueueNode);
                
                ImageIndex = SelectedImageIndex = 0;

                UpdateData();
                UpdateNodeText();
            }

            public ServiceNode(string configPath)
                : this(new Service(configPath))
            {}

            public bool IsDirty
            {
                get { return InputQueueNode.IsDirty || ErrorQueueNode.IsDirty; }
            }

            public Service Service { get; private set; }
            public QueueNode InputQueueNode { get; private set; }
            public QueueNode ErrorQueueNode { get; private set; }

            public void UpdateData()
            {
                InputQueueNode.UpdateData();
                ErrorQueueNode.UpdateData();

                HasMessages = (InputQueueNode.NumMessages > 0 || ErrorQueueNode.NumMessages > 0);
                HasErrors = ErrorQueueNode.NumMessages > 0;
            }

            private bool HasErrors
            {
                get { return _hasErrors; }
                set 
                {
                    if (_hasErrors == value)
                        return;

                    _hasErrors = value;
                    _isDirty = true;
                }
            }

            private bool HasMessages
            {
                get { return _hasMessages; }
                set
                {
                    if (_hasMessages == value)
                        return;

                    _hasMessages = value;
                    _isDirty = true;
                }
            }

            public void UpdateText()
            {
                InputQueueNode.UpdateText();
                ErrorQueueNode.UpdateText();

                if (!_isDirty)
                    return;

                UpdateNodeText();
            }

            private void UpdateNodeText()
            {
                ForeColor = HasErrors ? Color.Red : Color.Black;
                NodeFont = new Font(SystemFonts.DefaultFont, HasMessages ? FontStyle.Bold : FontStyle.Regular);
                Text = Service.ServiceName;
            }
        }

        private class QueueNode : TreeNode
        {
            private int _numMessages;
            private bool _isDirty = true;

            public QueueNode(ServiceQueue serviceQueue)
            {
                ServiceQueue = serviceQueue;
                ImageIndex = SelectedImageIndex = ServiceQueue.QueueType == ServiceQueueType.Error ? 2 : 1;

                if (ServiceQueue.UseJournal)
                    Nodes.Add(new QueueNode(ServiceQueue.GetJournalQueue()) { ImageIndex = 3, SelectedImageIndex = 3});

                UpdateData();
                UpdateNodeText();
            }

            public ServiceQueue ServiceQueue { get; private set; }

            public int NumMessages
            {
                get { return _numMessages; }
                private set 
                {
                    if (_numMessages == value)
                        return;

                    _numMessages = value;
                    _isDirty = true;
                }
            }

            private bool IsInvalid
            {
                get { return ServiceQueue.Invalid; }
            }

            public bool IsDirty 
            {
                get { return _isDirty; }
            }

            public void UpdateText()
            {
                if (!_isDirty)
                    return;

                _isDirty = false;

                UpdateNodeText();
            }

            private void UpdateNodeText()
            {
                if (ServiceQueue.QueueType == ServiceQueueType.Journal)
                {
                    NodeFont = new Font(SystemFonts.DefaultFont, FontStyle.Regular);
                    Text = "Journal";
                    return;
                }

                if (IsInvalid)
                {
                    NodeFont = new Font(SystemFonts.DefaultFont, FontStyle.Strikeout);
                    Text = string.Format("{0}", ServiceQueue);
                    return;
                }

                NodeFont = new Font(SystemFonts.DefaultFont, (NumMessages > 0) ? FontStyle.Bold : FontStyle.Regular);
                Text = string.Format("{0} ({1})", ServiceQueue, NumMessages);
            }

            public void UpdateData()
            {
                NumMessages = ServiceQueue.GetMessageCount();
            }
        }
        #endregion

        public ServiceTreeControl()
        {
            InitializeComponent();
            var imageList = new ImageList();
            imageList.Images.AddRange(new Image[]
            {
                Properties.Resources.ServiceNode, 
                Properties.Resources.QueueNode,
                Properties.Resources.QueueNodeError,
                Properties.Resources.QueueNodeJournal,
            });
            treeView1.ImageList = imageList;

            _expandAllItem = new ToolStripMenuItem("Expand All", null, (s,e) => ExpandAll());
            _collapseAllItem = new ToolStripMenuItem("Collapse All", null, (s,e) => treeView1.CollapseAll());
            _removeServiceItem = new ToolStripMenuItem("Remove Service", null, RemoveSelectedService);
            
            _contextMenu.Items.AddRange(new ToolStripItem[]
            {
                _expandAllItem, 
                _collapseAllItem, 
                new ToolStripSeparator(), 
                _removeServiceItem, 
            });
            treeView1.ContextMenuStrip = _contextMenu;

            Global.BackgroundTimer.Tick += BackgroundTimer_Tick;
        }

        private void RemoveSelectedService(object sender, EventArgs e)
        {
            treeView1.Nodes.Remove(treeView1.SelectedNode);
        }

        private void ExpandAll()
        {
            foreach (TreeNode node in treeView1.Nodes)
                node.Expand();
        }

        void BackgroundTimer_Tick()
        {
            UpdateData();
            Global.InvokeIfRequired(UpdateText);
        }

        private void UpdateData()
        {
            var serviceNodes = treeView1.Nodes.OfType<ServiceNode>();
            foreach (var serviceNode in serviceNodes)
                serviceNode.UpdateData();
        }

        private void UpdateText()
        {
            var serviceNodes = treeView1.Nodes.OfType<ServiceNode>().ToList();
            if (!serviceNodes.Any(n => n.IsDirty))
                return;

            treeView1.BeginUpdate();
            try
            {
                foreach (var serviceNode in serviceNodes)
                    serviceNode.UpdateText();
            }
            finally
            {
                treeView1.EndUpdate();
            }
        }

        public void AddServiceNode(string configPath)
        {
            try
            {
                treeView1.Nodes.Add(new ServiceNode(configPath));
            }
            catch (Exception)
            { }
        }

        void serviceTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var queueNode = e.Node as QueueNode;
            if (queueNode == null)
                return;

            var queue = queueNode.ServiceQueue;
            if (queue == null)
                return;

            OnQueueSelected(queue);
        }

        private void OnQueueSelected(ServiceQueue serviceQueue)
        {
            var ev = QueueSelected;
            if (ev != null)
                ev(serviceQueue);
        }

        private void selectServiceButton_Click(object sender, EventArgs e)
        {
            var form = new SelectServicesForm();
            form.Services = treeView1.Nodes.OfType<ServiceNode>().Select(n => n.Service);
            if (form.ShowDialog() != DialogResult.OK)
                return;

            CreateTree(form.Services);
        }

        private void CreateTree(IEnumerable<Service> services)
        {
            treeView1.Nodes.Clear();

            foreach (var service in services)
            {
                try
                {
                    treeView1.Nodes.Add(new ServiceNode(service));
                }
                catch (Exception)
                {}
            }
        }

        public IEnumerable<Service> Services
        {
            get { return treeView1.Nodes.OfType<ServiceNode>().Select(n => n.Service); }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            var node = treeView1.GetNodeAt(e.Location);
            if (node == null)
                return;

            treeView1.SelectedNode = node;
        }
    }
}
