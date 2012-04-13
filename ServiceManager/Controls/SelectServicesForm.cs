using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ServiceManager.Model;

namespace ServiceManager.Controls
{
    public partial class SelectServicesForm : Form
    {
        public SelectServicesForm()
        {
            InitializeComponent();
        }

        public IEnumerable<Service> Services
        {
            get { return manageServicesControl1.Services; }
            set { manageServicesControl1.Services = value; }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
