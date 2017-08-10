using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;

namespace XmlHelpers
{
	/// <summary>
	/// XML -> TreeView formatter class
	/// </summary>
	public class XML2Tree : Component
	{
		public enum TreeNodeType
		{
			Element = 0,
			InnerText = 1,
			Attribute = 2
		};

		private const string XML_NAMESPACE = "xmlns";

		private TreeNodeCollection _nodes;

		public event EventHandler<NamespaceEventArgs> NamespaceFound;

		public XML2Tree()
		{
		}

		public void DOMNodeToTree(XmlNode x, TreeNodeCollection nodes)
		{
			_nodes = nodes;

			switch (x.NodeType)
			{
				case XmlNodeType.Element:
					{
						//1. Add element node to tree
						TreeNode node = AddElementNode(x.Name);

						//2. Add attribute nodes to tree
						foreach (XmlAttribute attr in x.Attributes)
						{
							if (NamespaceFound != null)
							{
								if (XML_NAMESPACE == attr.Prefix)
								{
									//user namespace
									NamespaceFound(this, new NamespaceEventArgs(attr.LocalName, attr.Value));
								}
								else if (String.IsNullOrEmpty(attr.Prefix) && XML_NAMESPACE == attr.LocalName)
								{
									//default namespace
									NamespaceFound(this, new NamespaceEventArgs(String.Empty, attr.Value));
								}
							}
							AddAttributeNode(attr.Name, attr.Value, node);
						}

						//3. Add sub-elements recursively
						foreach (XmlNode xChild in x.ChildNodes)
						{
							DOMNodeToTree(xChild, node.Nodes);
						}
						break;
					}
				default:
					AddTextNode(x.InnerText);
					break;
			}
		}

		public void XPathCollectionToTree(XPathNodeIterator i, TreeNodeCollection nodes)
		{
			while (i.MoveNext())
			{
				XPathNodeToTree(i.Current, nodes);
			}
		}

		private void XPathNodeToTree(XPathNavigator nav, TreeNodeCollection nodes)
		{
			_nodes = nodes;

			switch (nav.NodeType)
			{
				case XPathNodeType.Root:
				case XPathNodeType.Element:
					{
						//1. Add element node to tree
						TreeNode node = AddElementNode(nav.Name);

						//2. Add attributes recursively
						if (nav.MoveToFirstAttribute())
						{
							do
							{
								AddAttributeNode(nav.Name, nav.Value, node);
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
					}

				case XPathNodeType.Attribute:
					AddAttributeNode(nav.Name, nav.Value);
					break;

				case XPathNodeType.Text:
					AddTextNode(nav.Value);
					break;

				case XPathNodeType.Comment:
					break;

				default:
					Debug.Assert(false);
					break;
			}
		}

		private TreeNode AddElementNode(string name)
		{
			TreeNode node = new TreeNode(name, 
				(int)TreeNodeType.Element, (int)TreeNodeType.Element);
			_nodes.Add(node);

			return node;
		}

		private void AddAttributeNode(string name, string value, TreeNode parentNode = null)
		{
			TreeNodeCollection nodes = (parentNode != null) ? parentNode.Nodes : _nodes;
			nodes.Add(new TreeNode(String.Format("{0} = {1}", name, value),
				(int)TreeNodeType.Attribute, (int)TreeNodeType.Attribute));
		}

		private void AddTextNode(string value)
		{
			_nodes.Add(new TreeNode(value, (int)TreeNodeType.InnerText, (int)TreeNodeType.InnerText));
		}
	}
}
