using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;

namespace XMLView
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

		private System.Windows.Forms.GroupBox grpSource;
		private System.Windows.Forms.GroupBox grpResult;
		private System.Windows.Forms.Label lblFileName;
		private System.Windows.Forms.Button cmdBrowse;
		private System.Windows.Forms.Button cmdParse;
		private System.Windows.Forms.TreeView tvwResult;
		private System.Windows.Forms.TextBox txtFileName;
		private System.Windows.Forms.ImageList imlNodes;
		private System.ComponentModel.IContainer components;

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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.grpSource = new System.Windows.Forms.GroupBox();
			this.cmdParse = new System.Windows.Forms.Button();
			this.cmdBrowse = new System.Windows.Forms.Button();
			this.lblFileName = new System.Windows.Forms.Label();
			this.txtFileName = new System.Windows.Forms.TextBox();
			this.grpResult = new System.Windows.Forms.GroupBox();
			this.tvwResult = new System.Windows.Forms.TreeView();
			this.imlNodes = new System.Windows.Forms.ImageList(this.components);
			this.grpSource.SuspendLayout();
			this.grpResult.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpSource
			// 
			this.grpSource.Controls.Add(this.cmdParse);
			this.grpSource.Controls.Add(this.cmdBrowse);
			this.grpSource.Controls.Add(this.lblFileName);
			this.grpSource.Controls.Add(this.txtFileName);
			this.grpSource.Dock = System.Windows.Forms.DockStyle.Top;
			this.grpSource.Location = new System.Drawing.Point(0, 0);
			this.grpSource.Name = "grpSource";
			this.grpSource.Size = new System.Drawing.Size(280, 80);
			this.grpSource.TabIndex = 0;
			this.grpSource.TabStop = false;
			this.grpSource.Text = "XML Source";
			// 
			// cmdParse
			// 
			this.cmdParse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdParse.Location = new System.Drawing.Point(216, 48);
			this.cmdParse.Name = "cmdParse";
			this.cmdParse.Size = new System.Drawing.Size(56, 20);
			this.cmdParse.TabIndex = 3;
			this.cmdParse.Text = "&Parse";
			this.cmdParse.Click += new System.EventHandler(this.cmdParse_Click);
			// 
			// cmdBrowse
			// 
			this.cmdBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdBrowse.Location = new System.Drawing.Point(240, 16);
			this.cmdBrowse.Name = "cmdBrowse";
			this.cmdBrowse.Size = new System.Drawing.Size(32, 20);
			this.cmdBrowse.TabIndex = 2;
			this.cmdBrowse.Text = "...";
			this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
			// 
			// lblFileName
			// 
			this.lblFileName.Location = new System.Drawing.Point(16, 16);
			this.lblFileName.Name = "lblFileName";
			this.lblFileName.Size = new System.Drawing.Size(32, 20);
			this.lblFileName.TabIndex = 1;
			this.lblFileName.Text = "&File:";
			// 
			// txtFileName
			// 
			this.txtFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtFileName.Location = new System.Drawing.Point(56, 16);
			this.txtFileName.Name = "txtFileName";
			this.txtFileName.Size = new System.Drawing.Size(176, 20);
			this.txtFileName.TabIndex = 0;
			this.txtFileName.Text = "";
			// 
			// grpResult
			// 
			this.grpResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grpResult.Controls.Add(this.tvwResult);
			this.grpResult.Location = new System.Drawing.Point(0, 96);
			this.grpResult.Name = "grpResult";
			this.grpResult.Size = new System.Drawing.Size(280, 176);
			this.grpResult.TabIndex = 1;
			this.grpResult.TabStop = false;
			this.grpResult.Text = "Result";
			// 
			// tvwResult
			// 
			this.tvwResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tvwResult.ImageList = this.imlNodes;
			this.tvwResult.Location = new System.Drawing.Point(8, 16);
			this.tvwResult.Name = "tvwResult";
			this.tvwResult.Size = new System.Drawing.Size(264, 152);
			this.tvwResult.TabIndex = 0;
			// 
			// imlNodes
			// 
			this.imlNodes.ImageSize = new System.Drawing.Size(16, 16);
			this.imlNodes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlNodes.ImageStream")));
			this.imlNodes.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(280, 273);
			this.Controls.Add(this.grpResult);
			this.Controls.Add(this.grpSource);
			this.Name = "MainForm";
			this.Text = "XML Viewer";
			this.grpSource.ResumeLayout(false);
			this.grpResult.ResumeLayout(false);
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

		private void cmdBrowse_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
			ofd.FilterIndex = 1;
			ofd.RestoreDirectory = true;

			if (DialogResult.OK == ofd.ShowDialog())
			{
				txtFileName.Text = ofd.FileName;
			}
		}

		private void cmdParse_Click(object sender, System.EventArgs e)
		{
			System.Xml.XmlDocument doc;

			try
			{
				doc = new System.Xml.XmlDocument();
				doc.Load(txtFileName.Text);

				tvwResult.Nodes.Clear();
				XmlHelpers.XML2Tree.DOMNodeToTree(doc.DocumentElement, tvwResult.Nodes);
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

		private void AddNodeWithChildren(XmlNode x, TreeNodeCollection nodes)
		{
			TreeNode node;

			switch (x.NodeType)
			{
			case XmlNodeType.Element:
				//1. Add element node to tree
				node = new TreeNode(x.Name, (int) ETreeNodes.tnElement, (int) ETreeNodes.tnElement);
				nodes.Add(node);

				//2. Add attribute nodes to tree
				foreach (XmlAttribute attr in x.Attributes)
				{
					node.Nodes.Add(new TreeNode(String.Format("{0} = {1}", attr.Name, attr.Value),
						(int) ETreeNodes.tnAttribute, (int) ETreeNodes.tnAttribute));
				}

				//3. Add sub-elements recursively
				foreach (XmlNode xChild in x.ChildNodes)
				{
					AddNodeWithChildren(xChild, node.Nodes);
				}
				break;
			default:
				nodes.Add(new TreeNode(x.InnerText, (int) ETreeNodes.tnInnerText, (int) ETreeNodes.tnInnerText));
				break;
			}
		}
	}
}
