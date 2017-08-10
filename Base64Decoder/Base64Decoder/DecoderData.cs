using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Base64Decoder
{
	internal class DecoderData : IDisposable
	{
		#region Constants

		public const string USAGE_INFO = "Base64Decoder.exe <input_file> <output_file>";

		#endregion

		#region Private Fields

		private Stream _inStream;
		private Stream _outStream;

		#endregion

		#region Construction

		public DecoderData(Stream inStream, Stream outStream)
		{
			_inStream = inStream;
			_outStream = outStream;
		}

		#endregion

		#region Public Methods

		public static DecoderData Parse(string[] args)
		{
			if (args.Length != 2)
			{
				throw new ArgumentException("Invalid number of command line parameters");
			}

			Stream inStream = null;
			Stream outStream = null;
			Exception ex = null;

			try
			{
				inStream = new FileStream(args[0], FileMode.Open);
				outStream = new FileStream(args[1], FileMode.Create);
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

			return new DecoderData(inStream, outStream);
		}

		#endregion

		#region Public Properties

		public Stream InputStream
		{
			get
			{
				return _inStream;
			}
		}

		public Stream OutputStream
		{
			get
			{
				return _outStream;
			}
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			if (_inStream != null)
			{
				_inStream.Close();
				_inStream = null;
			}

			if (_outStream != null)
			{
				_outStream.Close();
				_outStream = null;
			}
		}

		#endregion
	}
}
