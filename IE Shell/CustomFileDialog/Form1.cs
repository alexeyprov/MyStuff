using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.IO;

namespace CustomFileDialog
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        private Microsoft.InternetExplorer.ActiveX.WebBrowserControl webBrowser;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ComboBox filenameComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button canelButton;
        private System.Windows.Forms.Button desktopButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox folderTextBox;
        private System.Windows.Forms.Button upButton;

        #region Default members
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public Form1()
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
        #endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
            this.webBrowser = new Microsoft.InternetExplorer.ActiveX.WebBrowserControl();
            this.okButton = new System.Windows.Forms.Button();
            this.filenameComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.canelButton = new System.Windows.Forms.Button();
            this.desktopButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.folderTextBox = new System.Windows.Forms.TextBox();
            this.upButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.webBrowser)).BeginInit();
            this.SuspendLayout();
            // 
            // webBrowser
            // 
            this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser.Enabled = true;
            this.webBrowser.Location = new System.Drawing.Point(116, 40);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("webBrowser.OcxState")));
            this.webBrowser.Size = new System.Drawing.Size(528, 360);
            this.webBrowser.TabIndex = 1;
            this.webBrowser.BeforeNavigate += new Microsoft.InternetExplorer.ActiveX.BeforeNavigateEventHandler(this.webBrowser_BeforeNavigate);
            this.webBrowser.NavigateComplete += new Microsoft.InternetExplorer.ActiveX.NavigateEventHandler(this.webBrowser_NavigateComplete);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.okButton.Location = new System.Drawing.Point(528, 408);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(116, 32);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "Open";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // filenameComboBox
            // 
            this.filenameComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.filenameComboBox.Location = new System.Drawing.Point(192, 412);
            this.filenameComboBox.Name = "filenameComboBox";
            this.filenameComboBox.Size = new System.Drawing.Size(324, 24);
            this.filenameComboBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(116, 412);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "Filename";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // canelButton
            // 
            this.canelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.canelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.canelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.canelButton.Location = new System.Drawing.Point(528, 448);
            this.canelButton.Name = "canelButton";
            this.canelButton.Size = new System.Drawing.Size(116, 32);
            this.canelButton.TabIndex = 2;
            this.canelButton.Text = "Cancel";
            this.canelButton.Click += new System.EventHandler(this.canelButton_Click);
            // 
            // desktopButton
            // 
            this.desktopButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.desktopButton.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
            this.desktopButton.Image = ((System.Drawing.Image)(resources.GetObject("desktopButton.Image")));
            this.desktopButton.Location = new System.Drawing.Point(12, 40);
            this.desktopButton.Name = "desktopButton";
            this.desktopButton.Size = new System.Drawing.Size(92, 80);
            this.desktopButton.TabIndex = 5;
            this.desktopButton.Text = "&Desktop";
            this.desktopButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.desktopButton.Click += new System.EventHandler(this.desktopButton_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(12, 136);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 80);
            this.button1.TabIndex = 5;
            this.button1.Text = "&Recycle bin";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // folderTextBox
            // 
            this.folderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.folderTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.folderTextBox.Location = new System.Drawing.Point(116, 8);
            this.folderTextBox.Name = "folderTextBox";
            this.folderTextBox.ReadOnly = true;
            this.folderTextBox.Size = new System.Drawing.Size(300, 24);
            this.folderTextBox.TabIndex = 6;
            this.folderTextBox.Text = "";
            // 
            // upButton
            // 
            this.upButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.upButton.Image = ((System.Drawing.Image)(resources.GetObject("upButton.Image")));
            this.upButton.Location = new System.Drawing.Point(424, 8);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(36, 24);
            this.upButton.TabIndex = 7;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 17);
            this.CancelButton = this.canelButton;
            this.ClientSize = new System.Drawing.Size(652, 498);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.folderTextBox);
            this.Controls.Add(this.desktopButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.filenameComboBox);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.canelButton);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.webBrowser)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            Application.Run(new Form1());
        }

        string getPath(string url)
        {
            try { return Path.GetFullPath(url); }
            catch { return new Uri(url).LocalPath; }
        }

        void StartMyDocuments()
        {
            webBrowser.Navigate( System.Environment.GetFolderPath( System.Environment.SpecialFolder.Personal ) );
        }

        private void webBrowser_BeforeNavigate(object sender, Microsoft.InternetExplorer.ActiveX.BeforeNavigateEventArgs e)
        {
            if( Directory.Exists( getPath(e.Url) ) )
                return;
            else
                MessageBox.Show(
                    "Arey you sure?\r\n\r\n"+e.Url,
                    "Go next level" );
        }

        private void webBrowser_NavigateComplete(object sender, Microsoft.InternetExplorer.ActiveX.NavigateEventArgs e)
        {
            folderTextBox.Text=getPath(e.Url);
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            StartMyDocuments();
            filenameComboBox.Focus();
        }

        private void desktopButton_Click(object sender, System.EventArgs e)
        {
            webBrowser.Navigate( System.Environment.GetFolderPath( System.Environment.SpecialFolder.Desktop ) );
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            webBrowser.Navigate( System.Environment.GetFolderPath( System.Environment.SpecialFolder.DesktopDirectory ) );
        }

        private void upButton_Click(object sender, System.EventArgs e)
        {
            webBrowser.Navigate( Path.GetDirectoryName( folderTextBox.Text ) );
        }

        private void canelButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void okButton_Click(object sender, System.EventArgs e)
        {
            webBrowser.Navigate( filenameComboBox.Text );
        }
	}
}
