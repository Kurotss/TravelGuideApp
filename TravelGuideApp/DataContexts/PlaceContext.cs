using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using TravelGuideApp.Classes;

namespace TravelGuideApp.DataContexts
{
	public class PlaceContext : DataContext
	{
		public PlaceContext(string connection) : base(connection)
		{

		}

		[Function(Name = "LoadPlaces")]
		public ISingleResult<Place> LoadPlaces
		(
			[Parameter(Name = "id_route")] int? idRoute,
			[Parameter(Name = "id_type")] int? idType,
			[Parameter(Name = "search_expression")] string searchExpression,
			[Parameter(Name = "id_user")] int? idUser
		)
		{
			IExecuteResult result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), idRoute, idType, searchExpression, idUser);
			return (ISingleResult<Place>)result.ReturnValue;
		}

		[Function(Name = "UpdateBookmarks")]
		public int UpdateBookmarks
		(
			[Parameter(Name = "mode")] Actions mode,
			[Parameter(Name = "id_user")] int idUser,
			[Parameter(Name = "id_place")] int idPlace
		)
		{
			var result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), mode, idUser, idPlace);
			return (int)result.ReturnValue;
		}

		[Function(Name = "UpdatePlace")]
		public int UpdatePlace
		(
			[Parameter(Name = "id_place")] int? idPlace,
			[Parameter(Name = "name_place")] string namePlace,
			[Parameter(Name = "descr")] string descr,
			[Parameter(Name = "address_place")] string addressPlace,
			[Parameter(Name = "coordinates")] string coordinates,
			[Parameter(Name = "id_type")] int idType,
			[Parameter(Name = "picture")] byte[] picture
		)
		{
			var res = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), idPlace, namePlace, descr, addressPlace, coordinates, idType, picture);
			return (int)res.ReturnValue;
		}

		[Function(Name = "DeletePlace")]
		public int DeletePlace
		(
			[Parameter(Name = "id_place")] int idPlace
		)
		{
			var result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), idPlace);
			return (int)result.ReturnValue;
		}
	}
}
