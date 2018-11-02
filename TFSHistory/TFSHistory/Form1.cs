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

namespace TFSHistory
{
	public partial class Form1 : Form
	{
		private BindingList<ChangesetViewModel> bindList;
		private string _findText;

		public Form1()
		{
			InitializeComponent();

			txtTFSUrl.Text = Properties.Settings.Default.TFSURL;
			txtRelBranch.Text = Properties.Settings.Default.BRANCHURL;
			txtIgnoreUsers.Text = Properties.Settings.Default.IGNOREUSERS;
			txtIncludeUsers.Text = Properties.Settings.Default.INCLUDEUSERS;
			fromDate.Value = Properties.Settings.Default.FROMDATE;
			toDate.Value = Properties.Settings.Default.TODATE;

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
			request.IncludeFromUsersString = txtIncludeUsers.Text.Trim();
			request.FromDate = fromDate.Value;
			request.ToDate = toDate.Value;

			UpdateSettings();

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


		private void UpdateSettings()
		{
			Properties.Settings.Default.TFSURL = txtTFSUrl.Text;
			Properties.Settings.Default.BRANCHURL = txtRelBranch.Text;
			Properties.Settings.Default.IGNOREUSERS = txtIgnoreUsers.Text;
			Properties.Settings.Default.INCLUDEUSERS = txtIncludeUsers.Text;
			Properties.Settings.Default.FROMDATE = fromDate.Value;
			Properties.Settings.Default.TODATE = toDate.Value;

			Properties.Settings.Default.Save();
		}


		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);

			UpdateSettings();
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


		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);

			var key = e.KeyCode;
			var ctl = e.Control;

			if (key == Keys.F3)
			{
				if (_findText == null)
				{
					key = Keys.F;
					ctl = true;
				}
				else
				{
					FindNext();
				}
			}

			if (key == Keys.F && ctl)
			{
				_findText = Microsoft.VisualBasic.Interaction.InputBox("Text to Search For", "Find What").ToLower();

				if (_findText != null)
				{
					FindNext();
				}
			}
		}


		private void FindNext()
		{
			if (_findText == null) return;
			var startRow = dataGridView1.CurrentRow.Index;
			var startCol = dataGridView1.CurrentCell.ColumnIndex;

			var firstCol = startCol + 1;
			var maxCol = dataGridView1.ColumnCount;
			var row = startRow;
			var beenround = false;
			do
			{
				for (var c = firstCol; c < maxCol; c++)
				{
					if (dataGridView1.Rows[row].Cells[c].Value.ToString().ToLower().Contains(_findText))
					{
						dataGridView1.CurrentCell = dataGridView1.Rows[row].Cells[c];
						return;
					}
				}
				firstCol = 0;
				row++;

				if (row < dataGridView1.RowCount) continue;
				if (beenround) break;
				row = 0;
				beenround = true;
			} while (true);
		}
	}
}
