namespace ServiceManager.Controls
{
    partial class QueueOverviewControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.errorLabel = new System.Windows.Forms.Label();
            this.messagesGrid = new System.Windows.Forms.DataGridView();
            this.colCommand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNamespace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSourceQueue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFiller = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.returnToSourceButton = new System.Windows.Forms.Button();
            this.returnAllToSourceButton = new System.Windows.Forms.Button();
            this.createMessageButton = new System.Windows.Forms.Button();
            this.chkShowAll = new System.Windows.Forms.CheckBox();
            this.messageDetailsControl1 = new ServiceManager.Controls.MessageDetailsControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messagesGrid)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 33);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.errorLabel);
            this.splitContainer2.Panel1.Controls.Add(this.messagesGrid);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.messageDetailsControl1);
            this.splitContainer2.Size = new System.Drawing.Size(578, 317);
            this.splitContainer2.SplitterDistance = 120;
            this.splitContainer2.TabIndex = 2;
            this.splitContainer2.TabStop = false;
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.errorLabel.Location = new System.Drawing.Point(7, 28);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(74, 13);
            this.errorLabel.TabIndex = 1;
            this.errorLabel.Text = "Error message";
            this.errorLabel.Visible = false;
            // 
            // messagesGrid
            // 
            this.messagesGrid.AllowUserToAddRows = false;
            this.messagesGrid.AllowUserToDeleteRows = false;
            this.messagesGrid.AllowUserToResizeRows = false;
            this.messagesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.messagesGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.messagesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.messagesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCommand,
            this.colNamespace,
            this.colSourceQueue,
            this.colFiller});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.messagesGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.messagesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messagesGrid.Location = new System.Drawing.Point(0, 0);
            this.messagesGrid.Name = "messagesGrid";
            this.messagesGrid.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.messagesGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.messagesGrid.RowHeadersWidth = 25;
            this.messagesGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.messagesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.messagesGrid.Size = new System.Drawing.Size(578, 120);
            this.messagesGrid.StandardTab = true;
            this.messagesGrid.TabIndex = 0;
            this.messagesGrid.SelectionChanged += new System.EventHandler(this.messagesGrid_SelectionChanged);
            this.messagesGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.messagesGrid_KeyUp);
            // 
            // colCommand
            // 
            this.colCommand.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colCommand.HeaderText = "Command";
            this.colCommand.MinimumWidth = 200;
            this.colCommand.Name = "colCommand";
            this.colCommand.ReadOnly = true;
            this.colCommand.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colCommand.Width = 200;
            // 
            // colNamespace
            // 
            this.colNamespace.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colNamespace.HeaderText = "Namespace";
            this.colNamespace.Name = "colNamespace";
            this.colNamespace.ReadOnly = true;
            this.colNamespace.Width = 89;
            // 
            // colSourceQueue
            // 
            this.colSourceQueue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colSourceQueue.HeaderText = "Source Queue";
            this.colSourceQueue.Name = "colSourceQueue";
            this.colSourceQueue.ReadOnly = true;
            this.colSourceQueue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colSourceQueue.Width = 82;
            // 
            // colFiller
            // 
            this.colFiller.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFiller.HeaderText = "";
            this.colFiller.Name = "colFiller";
            this.colFiller.ReadOnly = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(584, 353);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.returnToSourceButton);
            this.flowLayoutPanel1.Controls.Add(this.returnAllToSourceButton);
            this.flowLayoutPanel1.Controls.Add(this.createMessageButton);
            this.flowLayoutPanel1.Controls.Add(this.chkShowAll);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(584, 30);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // returnToSourceButton
            // 
            this.returnToSourceButton.Location = new System.Drawing.Point(3, 3);
            this.returnToSourceButton.Name = "returnToSourceButton";
            this.returnToSourceButton.Size = new System.Drawing.Size(149, 23);
            this.returnToSourceButton.TabIndex = 0;
            this.returnToSourceButton.Text = "Return to Source Queue";
            this.returnToSourceButton.UseVisualStyleBackColor = true;
            this.returnToSourceButton.Click += new System.EventHandler(this.returnToSourceButton_Click);
            // 
            // returnAllToSourceButton
            // 
            this.returnAllToSourceButton.Location = new System.Drawing.Point(158, 3);
            this.returnAllToSourceButton.Name = "returnAllToSourceButton";
            this.returnAllToSourceButton.Size = new System.Drawing.Size(149, 23);
            this.returnAllToSourceButton.TabIndex = 1;
            this.returnAllToSourceButton.Text = "Return All to Source Queue";
            this.returnAllToSourceButton.UseVisualStyleBackColor = true;
            this.returnAllToSourceButton.Click += new System.EventHandler(this.returnAllToSourceButton_Click);
            // 
            // createMessageButton
            // 
            this.createMessageButton.Location = new System.Drawing.Point(313, 3);
            this.createMessageButton.Name = "createMessageButton";
            this.createMessageButton.Size = new System.Drawing.Size(109, 23);
            this.createMessageButton.TabIndex = 2;
            this.createMessageButton.Text = "Create Message...";
            this.createMessageButton.UseVisualStyleBackColor = true;
            this.createMessageButton.Click += new System.EventHandler(this.createMessageButton_Click);
            // 
            // chkShowAll
            // 
            this.chkShowAll.Location = new System.Drawing.Point(428, 3);
            this.chkShowAll.Name = "chkShowAll";
            this.chkShowAll.Size = new System.Drawing.Size(133, 24);
            this.chkShowAll.TabIndex = 3;
            this.chkShowAll.Text = "Show from any source";
            this.chkShowAll.UseVisualStyleBackColor = true;
            this.chkShowAll.CheckedChanged += new System.EventHandler(this.chkShowAll_CheckedChanged);
            // 
            // messageDetailsControl1
            // 
            this.messageDetailsControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageDetailsControl1.Location = new System.Drawing.Point(0, 0);
            this.messageDetailsControl1.Name = "messageDetailsControl1";
            this.messageDetailsControl1.Size = new System.Drawing.Size(578, 193);
            this.messageDetailsControl1.TabIndex = 0;
            // 
            // QueueOverviewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "QueueOverviewControl";
            this.Size = new System.Drawing.Size(584, 353);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.messagesGrid)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView messagesGrid;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button returnToSourceButton;
        private System.Windows.Forms.Button returnAllToSourceButton;
        private MessageDetailsControl messageDetailsControl1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCommand;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNamespace;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSourceQueue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFiller;
        private System.Windows.Forms.Button createMessageButton;
        private System.Windows.Forms.CheckBox chkShowAll;
    }
}
