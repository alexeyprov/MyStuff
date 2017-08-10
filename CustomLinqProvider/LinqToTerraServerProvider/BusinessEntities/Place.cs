namespace LinqToTerraServerProvider.BusinessEntities
{
	public class Place
	{
		// Properties.
		public string Name
		{
			get;
			private set;
		}
		public string State
		{
			get;
			private set;
		}
		public PlaceType PlaceType
		{
			get;
			private set;
		}

		// Constructor.
		internal Place(
			string name,
			string state,
			TerraServiceReference.PlaceType placeType)
		{
			this.Name = name;
			this.State = state;
			this.PlaceType = (PlaceType) placeType;
		}
	}
}
