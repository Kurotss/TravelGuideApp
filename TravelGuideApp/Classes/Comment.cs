using System.Data.Linq.Mapping;

namespace TravelGuideApp.Classes
{
	public class Comment
	{
		[Column(Name = "id_comment")]
		public int IdComment { get; set; }

		[Column(Name = "id_object")]
		public int IdPlace { get; set; }

		[Column(Name = "id_user")]
		public int IdUser { get; set; }

		[Column(Name = "fi_user")]
		public string FiUser { get; set; }

		[Column(Name = "avatar")]
		public byte[] Avatar { get; set; }

		[Column(Name = "raiting")]
		public int Raiting { get; set; }

		[Column(Name = "descr")]
		public string Descr { get; set; }

		[Column(Name = "id_table")]
		public int Idtable { get; set; }
	}
}
