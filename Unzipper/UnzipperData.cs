using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace Unzipper
{
	internal class UnzipperData : IDisposable
	{
		#region Constants

// ReSharper disable InconsistentNaming
		public const string USAGE_INFO = "Unzipper.exe <input_file> <output_folder>";
// ReSharper restore InconsistentNaming

		#endregion

		#region Private Fields

		private ZipFile _zip;
		private string _targetDirectory;

		#endregion

		#region Construction

		public UnzipperData(ZipFile zip, string targetDirectory)
		{
			_zip = zip;
			_targetDirectory = targetDirectory;
		}

		#endregion

		#region Public Methods

		public static UnzipperData Parse(string[] args)
		{
			if (args.Length != 2)
			{
				throw new ArgumentException("Invalid number of command line parameters");
			}

			ZipFile zip = null;
			string targetDirectory = String.Empty;
			Exception ex = null;

			try
			{
				zip = new ZipFile(args[0]);
				targetDirectory = args[1];

				if (!Directory.Exists(targetDirectory))
				{
					Directory.CreateDirectory(targetDirectory);
				}
			}
			catch (ArgumentException ae)
			{
				ex = ae;
			}
			catch (PathTooLongException pe)
			{
				ex = pe;
			}
			catch (DirectoryNotFoundException de)
			{
				ex = de;
			}
			catch (NotSupportedException ne)
			{
				ex = ne;
			}
			catch (UnauthorizedAccessException ue)
			{
				ex = ue;
			}

			if (ex != null)
			{
				throw new ArgumentException("Invalid file name provided: " + ex.Message, ex);
			}

			return new UnzipperData(zip, targetDirectory);
		}

		#endregion

		#region Public Properties

		public ZipFile Zip
		{
			get
			{
				return _zip;
			}
		}

		public string TargetDirectory
		{
			get
			{
				return _targetDirectory;
			}
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			if (_zip != null)
			{
				_zip.Close();
				_zip = null;
			}

			_targetDirectory = null;
		}

		#endregion
	}
}
