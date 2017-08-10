using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Base64Decoder
{
	internal class Decoder
	{
		public static void DecodeStream(Stream inStream, Stream outStream)
		{
			using (TextReader reader = new StreamReader(inStream))
			{
				string data = reader.ReadToEnd();

				using (BinaryWriter writer = new BinaryWriter(outStream))
				{
					writer.Write(Convert.FromBase64String(data));
				}
			}
		}
	}
}
