using System;

namespace Unzipper
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			try
			{
				using (UnzipperData d = UnzipperData.Parse(args))
				{
					Unzipper.Unzip(d.Zip, d.TargetDirectory);
				}
			}
			catch (ArgumentException ae)
			{
				Console.WriteLine("Error parsing command line" +
					Environment.NewLine + ae.Message);
				Console.WriteLine("Usage info: ");
				Console.WriteLine(UnzipperData.USAGE_INFO);
			}
		}
	}
}
