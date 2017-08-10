using System;
using System.Xml;
using System.Xml.XPath;
using System.Windows.Forms;

namespace XmlHelpers
{
	/// <summary>
	/// XML -> TreeView formatter class
	/// </summary>
	public class XML2Tree
	{
		public enum ETreeNodes
		{
			tnElement = 0,
			tnInnerText = 1,
			tnAttribute = 2
		};

		private XML2Tree()
		{
		}

		public static void DOMNodeToTree(XmlNode x, TreeNodeCollection nodes)
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
						DOMNodeToTree(xChild, node.Nodes);
					}
					break;
				default:
					nodes.Add(new TreeNode(x.InnerText, (int) ETreeNodes.tnInnerText, (int) ETreeNodes.tnInnerText));
					break;
			}
		}

		public static void XPathCollectionToTree(XPathNodeIterator i, TreeNodeCollection nodes)
		{
			while (i.MoveNext())
			{
				XPathNodeToTree(i.Current, nodes);
			}
		}

		private static void XPathNodeToTree(XPathNavigator nav, TreeNodeCollection nodes)
		{
			TreeNode node;
			//XPathNavigator navAttr;
			//XPathNavigator navSecn;

			switch (nav.NodeType)
			{
				case XPathNodeType.Root:
				case XPathNodeType.Element:
					//1. Add element node to tree
					node = new TreeNode(nav.Name, (int) ETreeNodes.tnElement, (int) ETreeNodes.tnElement);
					nodes.Add(node);

					//2. Add attributes recursively
					if (nav.MoveToFirstAttribute())
					{
						do
						{
							XPathNodeToTree(nav, node.Nodes);
						}
						while (nav.MoveToNextAttribute());
						nav.MoveToParent();
					}

					//3. Add children recursively
					if (nav.MoveToFirstChild())
					{
						do
						{
							XPathNodeToTree(nav, node.Nodes);
						}
						while (nav.MoveToNext());
						nav.MoveToParent();
					}
					break;

				case XPathNodeType.Text:
					nodes.Add(new TreeNode(nav.Value, (int) ETreeNodes.tnInnerText, (int) ETreeNodes.tnInnerText));
					break;

				case XPathNodeType.Attribute:
					nodes.Add(new TreeNode(String.Format("{0} = {1}", nav.Name, nav.Value),
						(int) ETreeNodes.tnAttribute, (int) ETreeNodes.tnAttribute));
					break;

				case XPathNodeType.Comment:
					break;

				default:
					System.Diagnostics.Debug.Assert(false);
					break;
			}
		}
	}
}
