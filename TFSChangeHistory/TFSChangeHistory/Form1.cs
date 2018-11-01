using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TFSChangeHistory
{
    public partial class Form1 : Form
    {
        BindingList<ChangesetViewModel> bindList;

        public Form1()
        {
            InitializeComponent();

            txtTFSUrl.Text = ConfigurationManager.AppSettings["TFSApiUrl"];
            txtRelBranch.Text = ConfigurationManager.AppSettings["TFSReleaseBasePath"];
            txtIgnoreUsers.Text = ConfigurationManager.AppSettings["IgnoreChangesetFromOwner"];

            bindList = new BindingList<ChangesetViewModel>();
            dataGridView1.DataSource = bindList;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRelBranch.Text))
            {
                MessageBox.Show("Enter release branch to continue", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(txtTFSUrl.Text))
            {
                MessageBox.Show("Enter TFS URL to continue", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            ChangesetHistoryRequest request = new ChangesetHistoryRequest();
            request.TFSUrl = txtTFSUrl.Text.Trim();
            request.ReleaseBranchUrl = txtRelBranch.Text.Trim();
            request.IgnoreFromUsersString = txtIgnoreUsers.Text.Trim();
            request.FromDate = fromDate.Value;
            request.ToDate = toDate.Value;

            try
            {
                bindList.Clear();
                var response = ChangesetManager.GetChangesetHistory(request);
                foreach (var changeItem in response)
                {
                    bindList.Add(changeItem);
                }
            }
            catch (ArgumentException aex)
            {
                MessageBox.Show($"Check your inputs and try again: {aex.Message}", "Validation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Message: {0}; Stacktrace: {1}", ex.Message, ex.StackTrace), "Error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCopyGrid_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectAll();
            DataObject dataObject = dataGridView1.GetClipboardContent();
            if (dataObject != null)
            {
                Clipboard.SetDataObject(dataObject);
            }
        }
    }
}
