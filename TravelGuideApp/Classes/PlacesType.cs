using System.Data.Linq.Mapping;

namespace TravelGuideApp.Classes
{
	public class PlacesType
	{
		[Column(Name = "id_type")]
		public int IdType { get; set; }

		[Column(Name = "name_type")]
		public string NameType { get; set; }
	}
}
