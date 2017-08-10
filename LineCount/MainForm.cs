using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using LineCount.Config;

namespace LineCount
{
	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtDirectory;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cmbLanguage;
		private System.Windows.Forms.CheckBox chkRecurse;
		private System.Windows.Forms.Label lblCount;
		private System.Windows.Forms.Button btnCalculate;
		private System.Windows.Forms.FolderBrowserDialog dlgBrowser;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.txtDirectory = new System.Windows.Forms.TextBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbLanguage = new System.Windows.Forms.ComboBox();
			this.chkRecurse = new System.Windows.Forms.CheckBox();
			this.btnCalculate = new System.Windows.Forms.Button();
			this.lblCount = new System.Windows.Forms.Label();
			this.dlgBrowser = new System.Windows.Forms.FolderBrowserDialog();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Select &Directory:";
			// 
			// txtDirectory
			// 
			this.txtDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtDirectory.Location = new System.Drawing.Point(8, 32);
			this.txtDirectory.Name = "txtDirectory";
			this.txtDirectory.Size = new System.Drawing.Size(232, 20);
			this.txtDirectory.TabIndex = 1;
			this.txtDirectory.Text = "";
			this.txtDirectory.TextChanged += new System.EventHandler(this.txtDirectory_TextChanged);
			// 
			// btnBrowse
			// 
			this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowse.Location = new System.Drawing.Point(248, 32);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(40, 20);
			this.btnBrowse.TabIndex = 2;
			this.btnBrowse.Text = "...";
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(136, 21);
			this.label2.TabIndex = 3;
			this.label2.Text = "Programming &Language:";
			// 
			// cmbLanguage
			// 
			this.cmbLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbLanguage.Items.AddRange(new object[] {
															 "C#",
															 "C++",
															 "VB 6.0",
															 "VB.NET"});
			this.cmbLanguage.Location = new System.Drawing.Point(152, 64);
			this.cmbLanguage.Name = "cmbLanguage";
			this.cmbLanguage.Size = new System.Drawing.Size(136, 21);
			this.cmbLanguage.Sorted = true;
			this.cmbLanguage.TabIndex = 4;
			// 
			// chkRecurse
			// 
			this.chkRecurse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.chkRecurse.Location = new System.Drawing.Point(8, 96);
			this.chkRecurse.Name = "chkRecurse";
			this.chkRecurse.Size = new System.Drawing.Size(280, 16);
			this.chkRecurse.TabIndex = 5;
			this.chkRecurse.Text = "&Recurse Subdirectories";
			// 
			// btnCalculate
			// 
			this.btnCalculate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCalculate.Enabled = false;
			this.btnCalculate.Location = new System.Drawing.Point(8, 120);
			this.btnCalculate.Name = "btnCalculate";
			this.btnCalculate.TabIndex = 6;
			this.btnCalculate.Text = "&Calculate";
			this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
			// 
			// lblCount
			// 
			this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblCount.Location = new System.Drawing.Point(96, 123);
			this.lblCount.Name = "lblCount";
			this.lblCount.Size = new System.Drawing.Size(192, 16);
			this.lblCount.TabIndex = 7;
			this.lblCount.Text = "Line Count:";
			// 
			// dlgBrowser
			// 
			this.dlgBrowser.Description = "Select a folder with sources:";
			this.dlgBrowser.SelectedPath = "D:\\Projects";
			this.dlgBrowser.ShowNewFolderButton = false;
			// 
			// MainForm
			// 
			this.AcceptButton = this.btnCalculate;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 157);
			this.Controls.Add(this.lblCount);
			this.Controls.Add(this.btnCalculate);
			this.Controls.Add(this.chkRecurse);
			this.Controls.Add(this.cmbLanguage);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.txtDirectory);
			this.Controls.Add(this.label1);
			this.MinimumSize = new System.Drawing.Size(300, 184);
			this.Name = "MainForm";
			this.Text = "Source Code Line Counter";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private void btnBrowse_Click(object sender, System.EventArgs e)
		{
			if (dlgBrowser.ShowDialog(this) == DialogResult.OK)
			{
				this.txtDirectory.Text = dlgBrowser.SelectedPath;
			}
			btnCalculate.Enabled = (txtDirectory.Text.Length > 0);
		}

		private void btnCalculate_Click(object sender, System.EventArgs e)
		{
			try
			{
				LineCounter ctr = new LineCounter(chkRecurse.Checked,
					((LanguageElement) cmbLanguage.SelectedItem).MaskList);
				lblCount.Text = String.Format("Line Count: {0}",
					ctr.Search(txtDirectory.Text));
			}
			catch (Exception ex)
			{
				lblCount.Text = ex.Message;
			}
		}

		private void MainForm_Load(object sender, System.EventArgs e)
		{
            cmbLanguage.Items.Clear();
            cmbLanguage.Items.AddRange(AppPreferencesSection.Instance.ProgrammingLanguages.GetAll());
			cmbLanguage.SelectedIndex = 0;
		}

		private void txtDirectory_TextChanged(object sender, System.EventArgs e)
		{
			btnCalculate.Enabled = (txtDirectory.Text.Length > 0);		
		}
	}
}
