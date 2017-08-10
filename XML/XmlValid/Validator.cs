using System;
using System.Xml;

namespace XmlValid
{
	/// <summary>
	/// Summary description for Validator
	/// </summary>
	class Validator
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			if (args.Length != 2)
			{
				System.Console.WriteLine("usage: xmlvalid.exe xml xsd");
				return;
			}

			XmlTextReader nvr = new XmlTextReader(args[0]);
			XmlValidatingReader vr = new XmlValidatingReader(nvr);
			try
			{	
				nvr.WhitespaceHandling = WhitespaceHandling.None;
				vr.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(vr_ValidationEventHandler);
				vr.Schemas.Add(GetTargetNamespace(args[1]), args[1]);
				while (vr.Read());
			}
			catch (XmlException xex)
			{
				System.Console.WriteLine("Xml Exception: " + xex.ToString());
			}
			catch (Exception ex)
			{
				System.Console.WriteLine("Exception: " + ex.ToString());
			}
			finally 
			{
				if (vr != null)
				{
					vr.Close();
				}

				if (nvr != null)
				{
					nvr.Close();
				}
			}
		}

		private static void vr_ValidationEventHandler(object sender, System.Xml.Schema.ValidationEventArgs e)
		{
			System.Console.WriteLine("Validation Error: " + e.Message);
		}

		private static string GetTargetNamespace(string strXSDURL)
		{
			XmlTextReader r = new XmlTextReader(strXSDURL);
			try
			{
				r.WhitespaceHandling = WhitespaceHandling.None;
				while (r.Read())
				{
					if ((XmlNodeType.Element == r.NodeType) && ("schema" == r.LocalName) && r.HasAttributes)
					{
						while (r.MoveToNextAttribute())
						{
							if ("targetNamespace" == r.Name)
							{
								return r.Value;
							}
						}
					}
				}
			}
			finally
			{
				if (r != null)
				{
					r.Close();
				}
			}
			return "";
		}
	}
}
