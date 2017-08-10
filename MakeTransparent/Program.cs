using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace MakeTransparent
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length != 1 || !File.Exists(args[0]))
			{
				Console.WriteLine("Usage: MakeTransparent.exe <image_file>");
				return;
			}

			ProcessImage(args[0]);
		}

		private static void ProcessImage(string path)
		{
			using (Bitmap image = new Bitmap(path))
			{
				Color transparentColor = image.GetPixel(0, 0);
				image.MakeTransparent(transparentColor);
				image.Save(Path.ChangeExtension(path, ".gif"), ImageFormat.Gif);
			}
		}
	}
}
