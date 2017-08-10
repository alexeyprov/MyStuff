using System;
using System.IO;

namespace LineCount
{
    //public enum Language
    //{
    //    CSharp,
    //    CPP,
    //    VB,
    //    VBNET
    //}

	/// <summary>
	/// Summary description for LineCounter.
	/// </summary>
	public class LineCounter
	{
		public LineCounter(bool recurse, string[] maskList)
		{
			_recurse = recurse;
            _maskList = new string[maskList.Length];
            int i = 0;
            Array.ForEach(maskList, delegate(string s)
            {
                _maskList[i++] = (null == s) ? null : s.Trim(); // copy with transformation
            });
		}

		// Operations
		public int Search(string path)
		{
			int cnt = 0;
			foreach (string mask in _maskList)
			{
				foreach (string fname in Directory.GetFiles(path, mask))
				{
					cnt += CountLines(fname);
				}
			}

			if (_recurse)
			{
				foreach (string dir in Directory.GetDirectories(path))
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
		bool _recurse;
        string[] _maskList;
		//Language _lng;
		/*readonly string[][] FILTER_MASK = {
				new string[] {"*.cs"},
				new string[] {"*.cpp", "*.inl", "*.h", "*.idl"},
				new string[] {"*.bas", "*.frm", "*.cls"},
				new string[] {"*.vb"}
			};
         */
	}
}
