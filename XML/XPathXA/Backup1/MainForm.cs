using System;
using System.Xml;
using System.Xml.XPath;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace XPathXA
{
	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		//Indexes in image list
		private enum ETreeNodes
		{
			tnElement = 0,
			tnInnerText = 1,
			tnAttribute = 2
		};

		private const string XML_NAMESPACE = "xmlns";
		private const string DEF_NAMESPACE = "d";

		private System.Windows.Forms.Panel pnlLeft;
		private System.Windows.Forms.Splitter ctlSplitter;
		private System.Windows.Forms.Panel pnlRight;
		private System.Windows.Forms.Button cmdParse;
		private System.Windows.Forms.Button cmdBrowse;
		private System.Windows.Forms.Label lblFileName;
		private System.Windows.Forms.TextBox txtFileName;
		private System.Windows.Forms.TreeView tvwSource;
		private System.Windows.Forms.TreeView tvwResult;
		private System.Windows.Forms.Button cmdRun;
		private System.Windows.Forms.Label lblExpression;
		private System.Windows.Forms.TextBox txtExpression;
		private System.Windows.Forms.ImageList imlXml;
		private System.ComponentModel.IContainer components;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

//			m_doc = null;
//			m_nav = null;
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.pnlLeft = new System.Windows.Forms.Panel();
			this.tvwSource = new System.Windows.Forms.TreeView();
			this.imlXml = new System.Windows.Forms.ImageList(this.components);
			this.cmdParse = new System.Windows.Forms.Button();
			this.cmdBrowse = new System.Windows.Forms.Button();
			this.lblFileName = new System.Windows.Forms.Label();
			this.txtFileName = new System.Windows.Forms.TextBox();
			this.ctlSplitter = new System.Windows.Forms.Splitter();
			this.pnlRight = new System.Windows.Forms.Panel();
			this.tvwResult = new System.Windows.Forms.TreeView();
			this.cmdRun = new System.Windows.Forms.Button();
			this.lblExpression = new System.Windows.Forms.Label();
			this.txtExpression = new System.Windows.Forms.TextBox();
			this.pnlLeft.SuspendLayout();
			this.pnlRight.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlLeft
			// 
			this.pnlLeft.Controls.Add(this.tvwSource);
			this.pnlLeft.Controls.Add(this.cmdParse);
			this.pnlLeft.Controls.Add(this.cmdBrowse);
			this.pnlLeft.Controls.Add(this.lblFileName);
			this.pnlLeft.Controls.Add(this.txtFileName);
			this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlLeft.Location = new System.Drawing.Point(0, 0);
			this.pnlLeft.Name = "pnlLeft";
			this.pnlLeft.Size = new System.Drawing.Size(184, 261);
			this.pnlLeft.TabIndex = 0;
			// 
			// tvwSource
			// 
			this.tvwSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tvwSource.ImageList = this.imlXml;
			this.tvwSource.Location = new System.Drawing.Point(8, 96);
			this.tvwSource.Name = "tvwSource";
			this.tvwSource.Size = new System.Drawing.Size(176, 160);
			this.tvwSource.TabIndex = 8;
			// 
			// imlXml
			// 
			this.imlXml.ImageSize = new System.Drawing.Size(16, 16);
			this.imlXml.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlXml.ImageStream")));
			this.imlXml.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// cmdParse
			// 
			this.cmdParse.Location = new System.Drawing.Point(8, 64);
			this.cmdParse.Name = "cmdParse";
			this.cmdParse.Size = new System.Drawing.Size(56, 20);
			this.cmdParse.TabIndex = 7;
			this.cmdParse.Text = "&Parse";
			this.cmdParse.Click += new System.EventHandler(this.cmdParse_Click);
			// 
			// cmdBrowse
			// 
			this.cmdBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdBrowse.Location = new System.Drawing.Point(152, 32);
			this.cmdBrowse.Name = "cmdBrowse";
			this.cmdBrowse.Size = new System.Drawing.Size(32, 20);
			this.cmdBrowse.TabIndex = 6;
			this.cmdBrowse.Text = "...";
			this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
			// 
			// lblFileName
			// 
			this.lblFileName.Location = new System.Drawing.Point(8, 8);
			this.lblFileName.Name = "lblFileName";
			this.lblFileName.Size = new System.Drawing.Size(128, 20);
			this.lblFileName.TabIndex = 5;
			this.lblFileName.Text = "&XML Source File:";
			// 
			// txtFileName
			// 
			this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtFileName.Location = new System.Drawing.Point(8, 32);
			this.txtFileName.Name = "txtFileName";
			this.txtFileName.Size = new System.Drawing.Size(140, 20);
			this.txtFileName.TabIndex = 4;
			this.txtFileName.Text = "";
			// 
			// ctlSplitter
			// 
			this.ctlSplitter.BackColor = System.Drawing.SystemColors.ControlLight;
			this.ctlSplitter.Location = new System.Drawing.Point(184, 0);
			this.ctlSplitter.Name = "ctlSplitter";
			this.ctlSplitter.Size = new System.Drawing.Size(3, 261);
			this.ctlSplitter.TabIndex = 1;
			this.ctlSplitter.TabStop = false;
			// 
			// pnlRight
			// 
			this.pnlRight.Controls.Add(this.tvwResult);
			this.pnlRight.Controls.Add(this.cmdRun);
			this.pnlRight.Controls.Add(this.lblExpression);
			this.pnlRight.Controls.Add(this.txtExpression);
			this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlRight.Location = new System.Drawing.Point(187, 0);
			this.pnlRight.Name = "pnlRight";
			this.pnlRight.Size = new System.Drawing.Size(181, 261);
			this.pnlRight.TabIndex = 2;
			// 
			// tvwResult
			// 
			this.tvwResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tvwResult.ImageList = this.imlXml;
			this.tvwResult.Location = new System.Drawing.Point(0, 96);
			this.tvwResult.Name = "tvwResult";
			this.tvwResult.Size = new System.Drawing.Size(176, 160);
			this.tvwResult.TabIndex = 12;
			// 
			// cmdRun
			// 
			this.cmdRun.Location = new System.Drawing.Point(6, 64);
			this.cmdRun.Name = "cmdRun";
			this.cmdRun.Size = new System.Drawing.Size(56, 20);
			this.cmdRun.TabIndex = 11;
			this.cmdRun.Text = "&Run";
			this.cmdRun.Click += new System.EventHandler(this.cmdRun_Click);
			// 
			// lblExpression
			// 
			this.lblExpression.Location = new System.Drawing.Point(6, 8);
			this.lblExpression.Name = "lblExpression";
			this.lblExpression.Size = new System.Drawing.Size(128, 20);
			this.lblExpression.TabIndex = 10;
			this.lblExpression.Text = "&XPath Expression:";
			// 
			// txtExpression
			// 
			this.txtExpression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtExpression.Location = new System.Drawing.Point(6, 32);
			this.txtExpression.Name = "txtExpression";
			this.txtExpression.Size = new System.Drawing.Size(168, 20);
			this.txtExpression.TabIndex = 9;
			this.txtExpression.Text = "";
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(368, 221);
			this.Controls.Add(this.pnlRight);
			this.Controls.Add(this.ctlSplitter);
			this.Controls.Add(this.pnlLeft);
			this.Name = "MainForm";
			this.Text = "XPath Expression Analyzer";
			this.pnlLeft.ResumeLayout(false);
			this.pnlRight.ResumeLayout(false);
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

		//Implementation
		#region Control Handlers
		private void cmdBrowse_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
			ofd.FilterIndex = 1;
			ofd.RestoreDirectory = true;

			if (DialogResult.OK == ofd.ShowDialog())
			{
				txtFileName.Text = "file://" + ofd.FileName;
			}
		}

		private void cmdParse_Click(object sender, System.EventArgs e)
		{
			XmlDocument doc;
			try
			{
				doc = new XmlDocument();
				doc.Load(txtFileName.Text);
				tvwSource.Nodes.Clear();
				XmlHelpers.XML2Tree.DOMNodeToTree(doc.DocumentElement, tvwSource.Nodes);

				m_ns = new XmlNamespaceManager(new NameTable());
				foreach (XmlAttribute a in doc.DocumentElement.Attributes)
				{
					if (XML_NAMESPACE == a.Prefix)
					{
						//user namespace
						m_ns.AddNamespace(a.LocalName, a.Value);
					}
					else if (String.Empty == a.Prefix && XML_NAMESPACE == a.LocalName)
					{
						//default namespace
						MessageBox.Show(this, String.Format("This document uses a default namespace.\n" +
							"Use \"{0}\" prefix to refer to this namespace in XPath expressions.", DEF_NAMESPACE));
						m_ns.AddNamespace(DEF_NAMESPACE, a.Value);
					}
				}
			}
			catch (XmlException xex)
			{
				System.Windows.Forms.MessageBox.Show(this, "Cannot parse XML:\n" + xex.ToString());
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(this, "General exception:\n" + ex.ToString());
			}
		}

		private void cmdRun_Click(object sender, System.EventArgs e)
		{
			XPathNodeIterator i;
			XPathDocument doc;
			XPathNavigator nav;
			XPathExpression expr;
			
			try
			{
				doc = new XPathDocument(txtFileName.Text);
				nav = doc.CreateNavigator();

				if (true)
				{
					System.Diagnostics.Debug.Assert(m_ns != null, "Namespace Manager should be initialized!");
					expr = nav.Compile(txtExpression.Text);
					expr.SetContext(m_ns);

					i =	nav.Select(expr);
				}
				else
				{
					i = nav.Select(txtExpression.Text);
				}

				tvwResult.Nodes.Clear();
				XmlHelpers.XML2Tree.XPathCollectionToTree(i, tvwResult.Nodes);
			}
			catch (XPathException xex)
			{
				System.Windows.Forms.MessageBox.Show(this, "XPath error:\n" + xex.ToString());
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(this, "General exception:\n" + ex.ToString());
			}
		}
		#endregion

		//Data Members
		//private XPathDocument m_doc;
		//private XPathNavigator m_nav;
		private XmlNamespaceManager m_ns;
	}
}
