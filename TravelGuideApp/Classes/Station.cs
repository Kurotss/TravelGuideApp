using System.Data.Linq.Mapping;

namespace TravelGuideApp.Classes
{
	public class Station
	{
		[Column(Name = "id_station")]
		public int IdStation { get; set; }

		[Column(Name = "id_line")]
		public int IdLine { get; set; }

		[Column(Name = "name_station")]
		public string NameStation { get; set; }

		[Column(Name = "coordinates")]
		public string Coordinates { get; set; }
	}
}
