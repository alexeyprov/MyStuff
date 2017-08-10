using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base64Decoder
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			try
			{
				using (DecoderData d = DecoderData.Parse(args))
				{
					Decoder.DecodeStream(d.InputStream, d.OutputStream);
				}
			}
			catch (ArgumentException ae)
			{
				Console.WriteLine("Error parsing command line" +
					Environment.NewLine + ae.Message);
				Console.WriteLine("Usage info: ");
				Console.WriteLine(DecoderData.USAGE_INFO);
			}

		}
	}
}
