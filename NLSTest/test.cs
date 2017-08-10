using System;
using System.Text;
using System.Globalization;

class TestApp
{
	public static void Main()
	{
		CultureInfo ci = new CultureInfo(0x0436); //Afrikaans
		Console.WriteLine(Encoding.GetEncoding(ci.TextInfo.ANSICodePage).EncodingName);
		string[] arNames = {"ar", "zh-CHS", "de", "en", "es",
					"fr", "it", "nl", "no", "pt", "mk", "ms", "uz" };
		foreach (string s in arNames)
		{
			try
			{
				ci = new CultureInfo(s); //Neutral
				Console.WriteLine("{0} -> {1}", s, ci.LCID);
			}
			catch (ArgumentException)
			{
				Console.WriteLine("{0} not supported", s);
			}
		}
	}
}