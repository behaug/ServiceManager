using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ServiceManager.Model;

namespace ServiceManager.Controls
{
    public partial class AddServicesForm : Form
    {
        public AddServicesForm()
        {
            InitializeComponent();
            ExistingServices = new Service[0];
        }

        public IEnumerable<Service> ExistingServices { get; set; }

        public IEnumerable<Service> SelectedServices
        {
            get
            {
                return dataGridView1.Rows.Cast<DataGridViewRow>()
                    .Where(r => (bool)r.Cells[0].Value)
                    .Select(r => r.Tag as Service)
                    .ToList();
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            var browser = new FolderBrowserDialog();
            browser.Description = "Select folder to scan for services";
            browser.ShowNewFolderButton = false;
            
            if (browser.ShowDialog() != DialogResult.OK)
                return;

            pathToScanText.Text = browser.SelectedPath;
        }

        private void scanButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {

                var path = pathToScanText.Text;
                var services = ScanForServices(path).Where(s => !ExistingServices.Contains(s));

                dataGridView1.Rows.Clear();
                foreach (var service in services)
                {
                    int i = dataGridView1.Rows.Add(true, service.ServiceName, service.ConfigPath);
                    var row = dataGridView1.Rows[i];
                    row.Tag = service;
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private IEnumerable<Service> ScanForServices(string path)
        {
            var dir = new DirectoryInfo(path);
            if (!dir.Exists)
                yield break;

            foreach (var configFile in dir.EnumerateFiles("*.dll.config"))
            {
                var service = Service.TryLoad(configFile.FullName);
                if (service == null)
                    continue;

                yield return service;
            }

            foreach (var subdir in dir.EnumerateDirectories().ToArray())
            {
                foreach (var service in ScanForServices(subdir.FullName))
                    yield return service;
            }
        }

        private void checkAllButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
                row.Cells[0].Value = true;
        }

        private void uncheckAllButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
                row.Cells[0].Value = false;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
