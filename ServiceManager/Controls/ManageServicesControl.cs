using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ServiceManager.Model;

namespace ServiceManager.Controls
{
    public partial class ManageServicesControl : UserControl
    {
        public ManageServicesControl()
        {
            InitializeComponent();
        }

        private void AddServiceRow(Service service)
        {
            int i = dataGridView1.Rows.Add(service.ServiceName, service.ConfigPath);
            var row = dataGridView1.Rows[i];
            row.Tag = service;
        }

        public IEnumerable<Service> Services
        {
            get { return dataGridView1.Rows.Cast<DataGridViewRow>().Select(r => r.Tag as Service); }
            set
            {
                dataGridView1.Rows.Clear();
                foreach (var service in value)
                    AddServiceRow(service);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var form = new AddServicesForm();
            form.ExistingServices = Services.ToList();
            var dialogResult = form.ShowDialog();
            if (dialogResult != DialogResult.OK)
                return;

            foreach (var service in form.SelectedServices)
            {
                if (Services.Contains(service))
                    continue;

                AddServiceRow(service);
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            RemoveSelectedRows();
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                RemoveSelectedRows();
        }

        private void RemoveSelectedRows()
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                dataGridView1.Rows.Remove(row);
        }
    }
}
