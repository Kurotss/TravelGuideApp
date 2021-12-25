using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using TravelGuideApp.Classes;

namespace TravelGuideApp.DataContexts
{
	public class UserContext : DataContext
	{
		public UserContext(string connection) : base(connection)
		{
		}

		[Function(Name = "ExecLogin")]
		public ISingleResult<User> ExecLogin
		(
			[Parameter(Name = "login")] string login,
			[Parameter(Name = "password")] string password
		)
		{
			IExecuteResult result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), login, password);
			return (ISingleResult<User>)result.ReturnValue;
		}

		[Function(Name = "UpdateUser")]
		public int UpdateUser
		(
			[Parameter(Name = "id_user")] int idUser,
			[Parameter(Name = "name")] string nameUser,
			[Parameter(Name = "surname")] string surname,
			[Parameter(Name = "age")] int age,
			[Parameter(Name = "login")] string login,
			[Parameter(Name = "password")] string password,
			[Parameter(Name = "id_station")] int? idStation,
			[Parameter(Name = "id_line")] int? idLine,
			[Parameter(Name = "avatar")] byte[] avatar
		)
		{
			var res = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), idUser, nameUser, surname, age, login, password,
				idStation, idLine, avatar);
			return (int)res.ReturnValue;
		}
	}
}
