using ServiceManager.Controls;

namespace ServiceManager
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.serviceTree = new ServiceManager.Controls.ServiceTreeControl();
            this.queueOverviewControl1 = new ServiceManager.Controls.QueueOverviewControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.serviceTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.queueOverviewControl1);
            this.splitContainer1.Size = new System.Drawing.Size(776, 416);
            this.splitContainer1.SplitterDistance = 279;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.TabStop = false;
            // 
            // serviceTree
            // 
            this.serviceTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serviceTree.Location = new System.Drawing.Point(0, 0);
            this.serviceTree.Name = "serviceTree";
            this.serviceTree.Size = new System.Drawing.Size(279, 416);
            this.serviceTree.TabIndex = 0;
            this.serviceTree.QueueSelected += new ServiceManager.Controls.QueueSelectedHandler(this.serviceTree_QueueSelected);
            // 
            // queueOverviewControl1
            // 
            this.queueOverviewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.queueOverviewControl1.Location = new System.Drawing.Point(0, 0);
            this.queueOverviewControl1.Name = "queueOverviewControl1";
            this.queueOverviewControl1.Size = new System.Drawing.Size(493, 416);
            this.queueOverviewControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 416);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Service Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ServiceTreeControl serviceTree;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private QueueOverviewControl queueOverviewControl1;
    }
}

