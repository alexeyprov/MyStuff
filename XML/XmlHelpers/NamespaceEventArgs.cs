using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlHelpers
{
	public class NamespaceEventArgs : EventArgs
	{
		public NamespaceEventArgs(string prefix, string name)
		{
			this.Prefix = prefix;
			this.Name = name;
		}

		public string Prefix
		{
			get;
			private set;
		}

		public string Name
		{
			get;
			private set;
		}
	}
}
