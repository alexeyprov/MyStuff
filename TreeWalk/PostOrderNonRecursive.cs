using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace TreeWalk
{
	internal static class PostOrderNonRecursive
	{
		private static void Main()
		{
			XElement root = TreeHelper.CreateTree();

			Stack<NodeHistory<XElement>> stack = new Stack<NodeHistory<XElement>>(
				new [] 
				{
					null, 
					new NodeHistory<XElement>() 
					{ 
						Node = root
					}
				});

			NodeHistory<XElement> nodeWrapper;

			while ((nodeWrapper = stack.Peek()) != null)
			{
				XElement node = nodeWrapper.Node;

				if (!node.Elements().Any() || nodeWrapper.IsVisited)
				{
					stack.Pop();
					TreeHelper.PrintNode(node);
				}
				else
				{
					nodeWrapper.IsVisited = true;

					foreach (XElement child in node.Elements())
					{
						stack.Push(new NodeHistory<XElement>()
							{
								Node = child
							});
					}
				}
			}
		}
	}
}