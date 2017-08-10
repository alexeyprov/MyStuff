using System;
using System.IO;

namespace LineCount
{
	public enum Language
	{
		CSharp,
		CPP,
		VB,
		VBNET
	}

	/// <summary>
	/// Summary description for LineCounter.
	/// </summary>
	public class LineCounter
	{
		public LineCounter(bool recurse, Language l)
		{
			_bRecurse = recurse;
			_lng = l;
		}

		// Operations
		public int Search(string strPath)
		{
			int cnt = 0;
			foreach (string sMask in FILTER_MASK[(int) _lng])
			{
				foreach (string fname in Directory.GetFiles(strPath, sMask))
				{
					cnt += CountLines(fname);
				}
			}

			if (_bRecurse)
			{
				foreach (string dir in Directory.GetDirectories(strPath))
				{
					cnt += Search(dir);
				}
			}

			return cnt;
		}

		// Implementation
		protected int CountLines(string fname)
		{
			int cnt = 0;
			using (StreamReader reader = new StreamReader(fname))
			{
				while (reader.ReadLine() != null)
				{
					cnt++;
				}
			}
			return cnt;
		}

		// Data Members
		bool _bRecurse;
		Language _lng;
		readonly string[][] FILTER_MASK = {
				new string[] {"*.cs"},
				new string[] {"*.cpp", "*.inl", "*.h", "*.idl"},
				new string[] {"*.bas", "*.frm", "*.cls"},
				new string[] {"*.vb"}
			};
	}
}
