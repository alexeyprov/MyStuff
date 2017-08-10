using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.DirectoryServices;
using System.Linq;
using System.Text;

namespace IISMetaBaseWalker
{
	internal class ApplicationPoolManager
	{
		#region "Private Fileds"

		private static WebServerTypes _serverType;

		#endregion

		#region "Public Properties"

		public static String ServerName
		{
			get
			{
				return Environment.MachineName.ToLower();
			}
		}

		public static WebServerTypes ServerType
		{
			get
			{
				if (_serverType == WebServerTypes.Unknown) _serverType = GetIISServerType();
				return _serverType;
			}
			set
			{
				_serverType = value;
			}
		}

		//public static string 

		#endregion

		#region "Public Methods"

		/// <summary>
		/// Create a new Application Pool and return an instance of the entry
		/// </summary>
		/// <param name="AppPoolName"></param>
		/// <returns></returns>
		public static DirectoryEntry CreateApplicationPool(string AppPoolName)
		{

			if (ServerType != WebServerTypes.IIS6 &&

				ServerType != WebServerTypes.IIS7)

				return null;

			DirectoryEntry root = GetDirectoryEntry(String.Format("IIS://{0}/W3SVC/AppPools", ServerName));

			if (root == null)

				return null;

			DirectoryEntry AppPool = root.Invoke("Create", "IIsApplicationPool", AppPoolName) as DirectoryEntry;

			AppPool.CommitChanges();

			return AppPool;

		}

		public static string[] GetApplicationPools()
		{
			if ((ServerType != WebServerTypes.IIS6) && (ServerType != WebServerTypes.IIS7))
			{
				return null;
			}
			DirectoryEntry directoryEntry = GetDirectoryEntry(String.Format("IIS://{0}/W3SVC/AppPools", ServerName));
			if (directoryEntry == null)
			{
				return null;
			}
			List<String> list = new List<String>();
			foreach (DirectoryEntry dirEntry in directoryEntry.Children)
			{
				PropertyCollection properties = dirEntry.Properties;
				String item = String.Empty;
				item = dirEntry.Name;
				list.Add(item);
			}
			return list.ToArray();
		}

		public static DirectoryEntry GetApplicationPool(string appPoolName)
		{
			return GetDirectoryEntry(String.Format("IIS://{0}/W3SVC/AppPools/{1}",
				ServerName, appPoolName));
		}

		public static StringDictionary GetAppPoolProperties(string appPoolName)
		{
			DirectoryEntry pool = GetApplicationPool(appPoolName);
			StringDictionary properties = null;

			if (pool != null && pool.Properties != null)
			{
				PropertyCollection props = pool.Properties;
				properties = new StringDictionary();

				foreach (string key in props.PropertyNames)
				{
					string value = String.Empty;
					PropertyValueCollection values = props[key];
					if (values != null && values.Count > 0)
					{
						value = values[0].ToString();
					}

					properties.Add(key, value);
				}
			}

			return properties;
		}

		public static WebServerTypes GetIISServerType()
		{
			string path = String.Format("IIS://{0}/W3SVC/INFO", ServerName);
			DirectoryEntry entry = null;
			try
			{
				entry = new DirectoryEntry(path);
			}
			catch
			{
				return WebServerTypes.Unknown;
			}
			int num = 5;
			try
			{
				num = (int)entry.Properties["MajorIISVersionNumber"].Value;
			}
			catch
			{
				return WebServerTypes.IIS5;
			}
			switch (num)
			{
				case 6:
					return WebServerTypes.IIS6;

				case 7:
					return WebServerTypes.IIS7;
			}
			return WebServerTypes.IIS6;
		}

		#endregion

		#region "Private Methods"

		private static DirectoryEntry GetDirectoryEntry(string Path)
		{
			DirectoryEntry entry = null;
			try
			{
				entry = new DirectoryEntry(Path);
			}
			catch
			{
				Console.WriteLine("Couldn't access node");
				return null;
			}
			if (entry == null)
			{
				Console.WriteLine("Couldn't access node");
				return null;
			}
			return entry;
		}

		#endregion

		public enum WebServerTypes
		{
			Unknown,
			IIS4,
			IIS5,
			IIS6,
			IIS7
		}

	}
}
