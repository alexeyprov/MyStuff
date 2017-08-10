using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;

using Microsoft.InternetExplorer.ActiveX;

namespace Sample1
{
	public class Form1 : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button blankButton;
        private Microsoft.InternetExplorer.ActiveX.WebBrowserControl webBrowser;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.Button writeFormButton;
        private System.Windows.Forms.TextBox urlTextBox;
        private System.Windows.Forms.Button setStatusButton;

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
            this.titleLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.blankButton = new System.Windows.Forms.Button();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.goButton = new System.Windows.Forms.Button();
            this.writeFormButton = new System.Windows.Forms.Button();
            this.setStatusButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.webBrowser)).BeginInit();
            this.SuspendLayout();
            // 
            // webBrowser
            // 
            this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser.Enabled = true;
            this.webBrowser.Location = new System.Drawing.Point(28, 116);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("webBrowser.OcxState")));
            this.webBrowser.Size = new System.Drawing.Size(324, 256);
            this.webBrowser.TabIndex = 0;
            this.webBrowser.StatusTextChange += new System.EventHandler(this.webBrowser_StatusTextChange);
            this.webBrowser.BeforeNavigate += new Microsoft.InternetExplorer.ActiveX.BeforeNavigateEventHandler(this.webBrowser_BeforeNavigate);
            this.webBrowser.NavigateComplete += new Microsoft.InternetExplorer.ActiveX.NavigateEventHandler(this.webBrowser_NavigateComplete);
            this.webBrowser.TitleChange += new System.EventHandler(this.webBrowser_TitleChange);
            // 
            // titleLabel
            // 
            this.titleLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.titleLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.titleLabel.Location = new System.Drawing.Point(28, 72);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(324, 36);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "<title>";
            // 
            // statusLabel
            // 
            this.statusLabel.BackColor = System.Drawing.SystemColors.Info;
            this.statusLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusLabel.ForeColor = System.Drawing.SystemColors.InfoText;
            this.statusLabel.Location = new System.Drawing.Point(28, 380);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(324, 23);
            this.statusLabel.TabIndex = 2;
            this.statusLabel.Text = "<status>";
            // 
            // blankButton
            // 
            this.blankButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.blankButton.Location = new System.Drawing.Point(404, 100);
            this.blankButton.Name = "blankButton";
            this.blankButton.Size = new System.Drawing.Size(188, 23);
            this.blankButton.TabIndex = 3;
            this.blankButton.Text = "about:&blank";
            this.blankButton.Click += new System.EventHandler(this.blankButton_Click);
            // 
            // urlTextBox
            // 
            this.urlTextBox.Location = new System.Drawing.Point(404, 152);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(136, 20);
            this.urlTextBox.TabIndex = 4;
            this.urlTextBox.Text = "http://msdn.microsoft.com/";
            // 
            // goButton
            // 
            this.goButton.Location = new System.Drawing.Point(544, 152);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(48, 23);
            this.goButton.TabIndex = 5;
            this.goButton.Text = "&Go";
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // writeFormButton
            // 
            this.writeFormButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.writeFormButton.Location = new System.Drawing.Point(404, 200);
            this.writeFormButton.Name = "writeFormButton";
            this.writeFormButton.Size = new System.Drawing.Size(188, 23);
            this.writeFormButton.TabIndex = 3;
            this.writeFormButton.Text = "write <&form>";
            this.writeFormButton.Click += new System.EventHandler(this.writeFormButton_Click);
            // 
            // setStatusButton
            // 
            this.setStatusButton.Location = new System.Drawing.Point(404, 248);
            this.setStatusButton.Name = "setStatusButton";
            this.setStatusButton.TabIndex = 6;
            this.setStatusButton.Text = "Properties";
            this.setStatusButton.Click += new System.EventHandler(this.setStatusButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(612, 422);
            this.Controls.Add(this.setStatusButton);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.urlTextBox);
            this.Controls.Add(this.blankButton);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.writeFormButton);
            this.Name = "Form1";
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

        private void blankButton_Click(object sender, System.EventArgs e)
        {
            webBrowser.Navigate("about:blank");
        }

        private void goButton_Click(object sender, System.EventArgs e)
        {
            webBrowser.Navigate(urlTextBox.Text);
        }

        private void writeFormButton_Click(object sender, System.EventArgs e)
        {
            webBrowser.HtmlBody.innerHTML=
                "<form method='POST'>"+
                "<input type=text name=input value='some text'>"+
                "<br>"+
                
                "<input type=submit value='POST'>"+
                "<br>"+
                "<select name=tormos>"+
                "<option selected>Tormos</option>"+
                "<option>Stop</option>"+
                "<option>Wait</option>"+
                "<option>Freeze</option>"+
                "</select>"+


                "</form>";
        }

        private void webBrowser_TitleChange(object sender, System.EventArgs e)
        {
            titleLabel.Text=webBrowser.Title;
        }

        private void webBrowser_StatusTextChange(object sender, System.EventArgs e)
        {
            statusLabel.Text=webBrowser.StatusText;
        }

        string GetHexString(byte[] bytes)
        {
            string result=null;
            foreach( byte b in bytes )
            {
                if( b<16 )
                    result+="0";
                result+=b.ToString("x");
            }
            return result;
        }

        private void webBrowser_BeforeNavigate(object sender, BeforeNavigateEventArgs e)
        {
            string postDataString=null;

            if( e.PostData==null )
                postDataString="(null)";
            else if( e.PostData.Length==0 )
                postDataString="(empty)";
            else
            {
                try { postDataString="[UTF8]"+Encoding.UTF8.GetString(e.PostData); }
                catch{}
                if( postDataString==null )
                    try { postDataString="[Unicode]"+Encoding.Unicode.GetString(e.PostData); }
                    catch{}
                if( postDataString==null )
                    try { postDataString="[BigEndianUnicode]"+Encoding.BigEndianUnicode.GetString(e.PostData); }
                    catch{}
                if( postDataString==null )
                    try { postDataString="["+Encoding.Default.EncodingName+"]"+Encoding.Default.GetString(e.PostData); }
                    catch{}
                if( postDataString==null )
                    postDataString="[Hex]"+GetHexString(e.PostData);
            }


            if( MessageBox.Show(
                "Url: "+e.Url+"\r\n"+
                "PostData: "+postDataString,
                "BeforeNavigate",
                MessageBoxButtons.OKCancel )!=DialogResult.OK )
            {
                e.Cancel=true;
            }

        }

        private void webBrowser_NavigateComplete(object sender, Microsoft.InternetExplorer.ActiveX.NavigateEventArgs e)
        {
            MessageBox.Show("NavigateComplete");
        }

        private void setStatusButton_Click(object sender, System.EventArgs e)
        {
            foreach( PropertyInfo prop in webBrowser.GetType().GetProperties() )
            {
                if( prop.DeclaringType!=webBrowser.GetType() )
                    continue;

                Console.Write(prop.Name+"= ");
                try
                {
                    Console.WriteLine( prop.GetValue(webBrowser, new object[]{}) );
                }
                catch( Exception err )
                {
                    Console.WriteLine(err.GetType().Name+"/"+err.Message);
                }
            }
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
        }
    }
}
