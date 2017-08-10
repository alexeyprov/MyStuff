using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace IISMetaBaseWalker
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			if (1 == args.Length)
			{
				PrintAppPoolProperties(0, args[0],
					ApplicationPoolManager.GetAppPoolProperties(args[0]));
			}
			else
			{
				string[] pools = ApplicationPoolManager.GetApplicationPools();

				if (pools != null)
				{
					foreach (string appPool in pools)
					{
						Console.WriteLine(">>> Application pool \"{0}\"", appPool);
						PrintAppPoolProperties(1, appPool,
							ApplicationPoolManager.GetAppPoolProperties(appPool));
					}
				}
				else
				{
					Console.WriteLine("No application pools supported by this version of IIS");
				}
			}
		}

		private static void PrintAppPoolProperties(uint indent, string poolName, StringDictionary props)
		{
			if (props != null)
			{
				string prefix = new String('\t', (int)indent);

				Console.WriteLine("{0}>>> Properties for application pool \"{1}\"",
					prefix, poolName);

				foreach (DictionaryEntry prop in props)
				{
					Console.WriteLine("{0}\t{1} = \"{2}\"",
						prefix, prop.Key, prop.Value);
				}
			}
		}
	}
}
