using System;
using System.Runtime.InteropServices;

namespace NLSTest
{
	/// <summary>
	/// Summary description for WinAPI.
	/// </summary>
	public class WinAPI
	{
		public WinAPI()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		[DllImport("kernel32.dll")]
		public static extern uint GetACP();

		[DllImport("kernel32.dll")]
		public static extern uint GetOEMCP();

		[DllImport("kernel32.dll")]
		public static extern ushort GetUserDefaultLangID();

		[DllImport("kernel32.dll")]
		public static extern ushort GetSystemDefaultLangID();
	}
}
