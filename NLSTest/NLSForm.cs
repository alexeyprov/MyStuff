using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using System.Globalization;

namespace NLSTest
{

	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class NLSForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox grpTestMethod;
		private System.Windows.Forms.RadioButton btnAPI;
		private System.Windows.Forms.RadioButton btnFCL;
		private System.Windows.Forms.Button cmdGetInfo;
		private System.Windows.Forms.ListView lvwProps;
		private System.Windows.Forms.ComboBox cmbEncoding;
		private System.Windows.Forms.ColumnHeader ColName;
		private System.Windows.Forms.ColumnHeader ColValue;
		private System.Windows.Forms.Label lblEncoding;
		private System.Windows.Forms.Label lblLocales;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		enum NLSProps
		{
			PropACP,
			PropOCP,
			PropEnc,
			PropWnc
		}

		public NLSForm()
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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
																													 "ANSI Code Page",
																													 "<unknown>"}, -1);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
																													 "OEM Code Page",
																													 "<unknown>"}, -1);
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
																													 "Name",
																													 "<unknown>"}, -1);
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
																													 "Web Name",
																													 "<unknown>"}, -1);
			this.grpTestMethod = new System.Windows.Forms.GroupBox();
			this.btnFCL = new System.Windows.Forms.RadioButton();
			this.btnAPI = new System.Windows.Forms.RadioButton();
			this.cmdGetInfo = new System.Windows.Forms.Button();
			this.lvwProps = new System.Windows.Forms.ListView();
			this.ColName = new System.Windows.Forms.ColumnHeader();
			this.ColValue = new System.Windows.Forms.ColumnHeader();
			this.cmbEncoding = new System.Windows.Forms.ComboBox();
			this.lblEncoding = new System.Windows.Forms.Label();
			this.lblLocales = new System.Windows.Forms.Label();
			this.grpTestMethod.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpTestMethod
			// 
			this.grpTestMethod.Controls.Add(this.btnFCL);
			this.grpTestMethod.Controls.Add(this.btnAPI);
			this.grpTestMethod.Location = new System.Drawing.Point(0, 64);
			this.grpTestMethod.Name = "grpTestMethod";
			this.grpTestMethod.Size = new System.Drawing.Size(292, 48);
			this.grpTestMethod.TabIndex = 0;
			this.grpTestMethod.TabStop = false;
			this.grpTestMethod.Text = "Test via";
			// 
			// btnFCL
			// 
			this.btnFCL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFCL.Location = new System.Drawing.Point(176, 16);
			this.btnFCL.Name = "btnFCL";
			this.btnFCL.TabIndex = 1;
			this.btnFCL.Text = ".&NET FCL";
			// 
			// btnAPI
			// 
			this.btnAPI.Checked = true;
			this.btnAPI.Location = new System.Drawing.Point(16, 16);
			this.btnAPI.Name = "btnAPI";
			this.btnAPI.TabIndex = 0;
			this.btnAPI.TabStop = true;
			this.btnAPI.Text = "Windows &API";
			// 
			// cmdGetInfo
			// 
			this.cmdGetInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdGetInfo.Location = new System.Drawing.Point(216, 120);
			this.cmdGetInfo.Name = "cmdGetInfo";
			this.cmdGetInfo.TabIndex = 1;
			this.cmdGetInfo.Text = "Get &Info";
			this.cmdGetInfo.Click += new System.EventHandler(this.cmdGetInfo_Click);
			// 
			// lvwProps
			// 
			this.lvwProps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lvwProps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					   this.ColName,
																					   this.ColValue});
			listViewItem1.Tag = "0";
			listViewItem2.Tag = "1";
			this.lvwProps.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
																					 listViewItem1,
																					 listViewItem2,
																					 listViewItem3,
																					 listViewItem4});
			this.lvwProps.Location = new System.Drawing.Point(0, 152);
			this.lvwProps.Name = "lvwProps";
			this.lvwProps.Size = new System.Drawing.Size(296, 120);
			this.lvwProps.TabIndex = 2;
			this.lvwProps.View = System.Windows.Forms.View.Details;
			// 
			// ColName
			// 
			this.ColName.Text = "Name";
			this.ColName.Width = 100;
			// 
			// ColValue
			// 
			this.ColValue.Text = "Value";
			this.ColValue.Width = 150;
			// 
			// cmbEncoding
			// 
			this.cmbEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cmbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbEncoding.Items.AddRange(new object[] {
															 "(default)",
															 "ASCII",
															 "Unicode",
															 "UTF-8"});
			this.cmbEncoding.Location = new System.Drawing.Point(104, 120);
			this.cmbEncoding.Name = "cmbEncoding";
			this.cmbEncoding.Size = new System.Drawing.Size(104, 21);
			this.cmbEncoding.TabIndex = 3;
			// 
			// lblEncoding
			// 
			this.lblEncoding.Location = new System.Drawing.Point(8, 120);
			this.lblEncoding.Name = "lblEncoding";
			this.lblEncoding.Size = new System.Drawing.Size(88, 23);
			this.lblEncoding.TabIndex = 4;
			this.lblEncoding.Text = "&Encoding:";
			// 
			// lblLocales
			// 
			this.lblLocales.Location = new System.Drawing.Point(8, 24);
			this.lblLocales.Name = "lblLocales";
			this.lblLocales.Size = new System.Drawing.Size(280, 16);
			this.lblLocales.TabIndex = 5;
			this.lblLocales.Text = "System Locale: (unknown); User Locale: (unknown)";
			// 
			// NLSForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.Add(this.lblLocales);
			this.Controls.Add(this.lblEncoding);
			this.Controls.Add(this.cmbEncoding);
			this.Controls.Add(this.lvwProps);
			this.Controls.Add(this.cmdGetInfo);
			this.Controls.Add(this.grpTestMethod);
			this.MinimumSize = new System.Drawing.Size(300, 300);
			this.Name = "NLSForm";
			this.Text = "NLS Form";
			this.grpTestMethod.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new NLSForm());
		}

		private void cmdGetInfo_Click(object sender, System.EventArgs ea)
		{
			ClearProperties();
			if (this.btnAPI.Checked)
			{
				SetProperty(NLSProps.PropACP, WinAPI.GetACP().ToString());
				SetProperty(NLSProps.PropOCP, WinAPI.GetOEMCP().ToString());
				SetCultureInfo((int) WinAPI.GetSystemDefaultLangID(), (int) WinAPI.GetUserDefaultLangID());
			}
			else
			{
				Encoding e = Encoding.Default;
				switch (this.cmbEncoding.SelectedIndex)
				{
					case 1: //ASCII
						e = Encoding.ASCII;
						break;
					case 2: //Unicode
						e = Encoding.Unicode;
						break;
					case 3: //UTF-8
						e = Encoding.UTF8;
						break;
					default:
						break;
				}

				SetProperty(NLSProps.PropACP, e.WindowsCodePage.ToString());
				SetProperty(NLSProps.PropOCP, e.CodePage.ToString());
				SetProperty(NLSProps.PropEnc, e.EncodingName);
				SetProperty(NLSProps.PropWnc, e.WebName);

				SetCultureInfo(CultureInfo.InstalledUICulture.LCID, CultureInfo.CurrentCulture.LCID);
			}
		}

		private void ClearProperties()
		{
			SetProperty(NLSProps.PropACP, "<unknown>");
			SetProperty(NLSProps.PropOCP, "<unknown>");
			SetProperty(NLSProps.PropEnc, "<unknown>");
			SetProperty(NLSProps.PropWnc, "<unknown>");
		}

		private void SetProperty(NLSProps propName, string propValue)
		{
			this.lvwProps.Items[(int) propName].SubItems[1].Text = propValue;
		}

		private void SetCultureInfo(int nLCIDSystem, int nLCIDUser)
		{
			lblLocales.Text = String.Format("System Locale: {0}; User Locale: {1}",
				(nLCIDSystem != 0) ? "0x" + nLCIDSystem.ToString("X") : "unknown",
				(nLCIDUser != 0) ? "0x" + nLCIDUser.ToString("X") : "unknown");
		}

	}
}
