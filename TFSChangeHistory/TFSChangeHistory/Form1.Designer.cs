namespace TFSHistory
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTFSUrl = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRelBranch = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.fromDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.toDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIgnoreUsers = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnCopyGrid = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCopyGrid);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.txtIgnoreUsers);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.toDate);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.fromDate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtRelBranch);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtTFSUrl);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(775, 169);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TFS Search Options";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(102, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "TFS URL";
            // 
            // txtTFSUrl
            // 
            this.txtTFSUrl.Location = new System.Drawing.Point(160, 26);
            this.txtTFSUrl.Name = "txtTFSUrl";
            this.txtTFSUrl.Size = new System.Drawing.Size(569, 20);
            this.txtTFSUrl.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Release Branch";
            // 
            // txtRelBranch
            // 
            this.txtRelBranch.Location = new System.Drawing.Point(160, 52);
            this.txtRelBranch.Name = "txtRelBranch";
            this.txtRelBranch.Size = new System.Drawing.Size(569, 20);
            this.txtRelBranch.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "From Date";
            // 
            // fromDate
            // 
            this.fromDate.Location = new System.Drawing.Point(16, 143);
            this.fromDate.Name = "fromDate";
            this.fromDate.Size = new System.Drawing.Size(200, 20);
            this.fromDate.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(274, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "To Date";
            // 
            // toDate
            // 
            this.toDate.Location = new System.Drawing.Point(277, 143);
            this.toDate.Name = "toDate";
            this.toDate.Size = new System.Drawing.Size(200, 20);
            this.toDate.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Ignore CheckIns from Users";
            // 
            // txtIgnoreUsers
            // 
            this.txtIgnoreUsers.Location = new System.Drawing.Point(160, 78);
            this.txtIgnoreUsers.Name = "txtIgnoreUsers";
            this.txtIgnoreUsers.Size = new System.Drawing.Size(569, 20);
            this.txtIgnoreUsers.TabIndex = 9;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(516, 140);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnCopyGrid
            // 
            this.btnCopyGrid.Location = new System.Drawing.Point(609, 140);
            this.btnCopyGrid.Name = "btnCopyGrid";
            this.btnCopyGrid.Size = new System.Drawing.Size(120, 23);
            this.btnCopyGrid.TabIndex = 11;
            this.btnCopyGrid.Text = "Copy Grid Contents";
            this.btnCopyGrid.UseVisualStyleBackColor = true;
            this.btnCopyGrid.Click += new System.EventHandler(this.btnCopyGrid_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 189);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(775, 483);
            this.dataGridView1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 684);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCopyGrid;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtIgnoreUsers;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker toDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker fromDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRelBranch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTFSUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

