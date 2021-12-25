using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelGuideApp.Classes
{
	public class User
	{
		[Column(Name = "id_user")]
		public int IdUser { get; set; }

		[Column(Name = "name_user")]
		public string NameUser { get; set; }

		[Column(Name = "surname")]
		public string Surname { get; set; }

		[Column(Name = "age")]
		public int Age { get; set; }

		[Column(Name = "id_station")]
		public int? IdStation { get; set; }

		[Column(Name = "id_role")]
		public int IdRole { get; set; }

		[Column(Name = "login_user")]
		public string LoginUser { get; set; }

		[Column(Name = "password_user")]
		public string PasswordUser { get; set; }

		[Column(Name = "avatar")]
		public byte[] Avatar { get; set; }

		[Column(Name = "id_line")]
		public int? IdLine { get; set; }
	}
}
