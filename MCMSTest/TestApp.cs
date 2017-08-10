using System;
using Microsoft.ContentManagement.Publishing;
using Microsoft.ContentManagement.Publishing.Extensions.Placeholders;

namespace MCMSTest
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class TestApp
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			CmsApplicationContext cmsContext = new CmsApplicationContext();
			//cmsContext.AuthenticateAsUser("WinNT://NIKOLAYZL2K/mcms_admin", "admin", PublishingMode.Update);
			cmsContext.AuthenticateAsCurrentUser(PublishingMode.Update);

			Console.WriteLine("Enter query strings for channels. Empty string - exit.");
			string strQuery = ReadQuery();
			while (strQuery.Length > 0)
			{			
				Channel startChannel = cmsContext.Searches.GetByPath(strQuery) as Channel;
				if (startChannel != null)
				{
					Console.WriteLine("Starting in channel: " + startChannel.Path);

					foreach (Posting p in startChannel.Postings)
					{
						Console.WriteLine("============================");
						Console.WriteLine("Posting Name: " + p.DisplayName);
						Console.WriteLine("Template: " + p.Template.SourceFile);

						Console.WriteLine("PH count: " + p.Placeholders.Count);

						foreach (PlaceholderDefinition pd in p.Template.PlaceholderDefinitions) 
						{
							Console.WriteLine("Placeholder Definition: " + pd.Name + " _ Type: " + pd.GetType());
						}
						Console.WriteLine(">>>");

						foreach (Placeholder ph in p.Placeholders) 
						{
							Console.WriteLine("Placeholder: " + ph.Name + " _ Type: " + ph.GetType());
							Console.WriteLine(ph.ToString() + Environment.NewLine);
							if (ph is HtmlPlaceholder)
							{
								Console.WriteLine(((HtmlPlaceholder) ph).Html);
							}
							else if (ph is XmlPlaceholder)
							{
								Console.WriteLine(((XmlPlaceholder) ph).XmlAsString);
							}

						}

						Console.WriteLine("============================");
					}
				}
				else
				{
					Console.WriteLine("Error: Could not retrieve starting channel.");
				}
				strQuery = ReadQuery();
			}
		}

		static string ReadQuery()
		{
			Console.Write("Your query: ");
			return Console.ReadLine();
		}
	}
}
