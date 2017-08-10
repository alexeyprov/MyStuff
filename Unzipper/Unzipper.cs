using System;
using System.Diagnostics;
using System.IO;

using ICSharpCode.SharpZipLib.Zip;

namespace Unzipper
{
	internal class Unzipper
	{
		public static void Unzip(ZipFile zip, string targetDirectory)
		{
			foreach (ZipEntry entry in zip)
			{
				if (entry.IsFile)
				{
					string entryFileName;

					if (Path.IsPathRooted(entry.Name))
					{
						string workName = Path.GetPathRoot(entry.Name);

						Debug.Assert(workName != null, "workName != null");

						workName = entry.Name.Substring(workName.Length);

// ReSharper disable AssignNullToNotNullAttribute
						entryFileName = Path.Combine(Path.GetDirectoryName(workName), Path.GetFileName(entry.Name));
// ReSharper restore AssignNullToNotNullAttribute
					}
					else
					{
						entryFileName = entry.Name;
					}

					string targetName = Path.Combine(targetDirectory, entryFileName);
					string fullPath = Path.GetDirectoryName(Path.GetFullPath(targetName));

					Debug.Assert(fullPath != null, "fullPath != null");

					if (!Directory.Exists(fullPath))
					{
						Directory.CreateDirectory(fullPath);
					}

					if (!String.IsNullOrEmpty(targetName))
					{
						#region copy stream

						Stream zStream = zip.GetInputStream(entry);

						using (FileStream sWriter = File.Create(targetName))
						{
							int size = 64 * 1024;

							byte[] data = new byte[size];

							while (true)
							{
								size = zStream.Read(data, 0, data.Length);

								if (size > 0)
								{
									sWriter.Write(data, 0, size);
								}
								else
								{
									break;
								}
							}
						}

						#endregion
					}
				}
			}
		}
	}
}
