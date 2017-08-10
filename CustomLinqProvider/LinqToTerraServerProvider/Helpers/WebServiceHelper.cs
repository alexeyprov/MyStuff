using System;
using System.Collections.Generic;
using LinqToTerraServerProvider.Helpers;
using LinqToTerraServerProvider.TerraServiceReference;
using Place = LinqToTerraServerProvider.BusinessEntities.Place;

namespace LinqToTerraServerProvider
{
	internal static class WebServiceHelper
	{
// ReSharper disable InconsistentNaming
		private const int MAX_RESULTS = 200;
		private const bool IMAGE_FLAG = false;
// ReSharper restore InconsistentNaming

		internal static Place[] GetPlacesFromTerraServer(IList<string> locations)
		{
			// Limit the total number of Web service calls.
			if (locations.Count > 5)
			{
				throw new InvalidQueryException(
					"This query requires more than five separate calls to the Web service. Please decrease the number of locations in your query.");
			}

			List<Place> allPlaces = new List<Place>();

			// For each location, call the Web service method to get data.
			foreach (string location in locations)
			{
				allPlaces.AddRange(
					CallGetPlaceListMethod(location));
			}

			return allPlaces.ToArray();
		}

		private static IEnumerable<Place> CallGetPlaceListMethod(string location)
		{
			TerraServiceSoapClient client = new TerraServiceSoapClient();

			try
			{
				// Call the Web service method "GetPlaceList".
				PlaceFacts[] placeFacts = client.GetPlaceList(location, MAX_RESULTS, IMAGE_FLAG);

				// If there are exactly 'numResults' results, they are probably truncated.
				if (placeFacts.Length == MAX_RESULTS)
					throw new Exception("The results have been truncated by the Web service and would not be complete. Please try a different query.");

				// Create Place objects from the PlaceFacts objects returned by the Web service.
				Place[] places = new Place[placeFacts.Length];
				for (int i = 0; i < placeFacts.Length; i++)
				{
					places[i] = new Place(
						placeFacts[i].Place.City,
						placeFacts[i].Place.State,
						placeFacts[i].PlaceTypeId);
				}

				// Close the WCF client.
				client.Close();

				return places;
			}
			catch (TimeoutException)
			{
				client.Abort();
				throw;
			}
			catch (System.ServiceModel.CommunicationException)
			{
				client.Abort();
				throw;
			}
		}
	}
}