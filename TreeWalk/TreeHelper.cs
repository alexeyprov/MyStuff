using System;
using System.Xml.Linq;

namespace TreeWalk
{
	internal static class TreeHelper
	{
		internal static XElement CreateTree()
		{
			return new XElement("books",
				new XElement("book",
					new XAttribute("name", "CLR via C#"),
					new XElement("author",
						new XAttribute("name", "Jeffrey Richter"))),
				new XElement("book",
					new XAttribute("name", "Pro ASP.NET 4 in C# 2010"),
					new XElement("author",
						new XAttribute("name", "Matthew MacDonald")),
					new XElement("author",
						new XAttribute("name", "Adam Freeman")),
					new XElement("author",
						new XAttribute("name", "Mario Szpuszta"))),
				new XElement("book",
					new XAttribute("name", "Microsoft .NET Framework 3.5 - Windows Communication Foundation"),
					new XElement("author",
						new XAttribute("name", "Bruce Johnson")),
					new XElement("author",
						new XAttribute("name", "Peter Madzyak")),
					new XElement("author",
						new XAttribute("name", "Sara Morgan"))));
		}

		internal static void PrintNode(XElement node)
		{
		    if (null == node)
			{
				throw new ArgumentNullException("node");
			}

			string nodeInfo = String.Format("[{0}]", node.Name);
			
			XAttribute name = node.Attribute("name");
			if (name != null)
			{
				nodeInfo += String.Format(" - {0}", name);
			}
			
			Console.WriteLine(nodeInfo);
		}
	}
}