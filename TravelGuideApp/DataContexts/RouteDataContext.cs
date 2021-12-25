using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using TravelGuideApp.Classes;

namespace TravelGuideApp.DataContexts
{
	public class RouteDataContext : DataContext
	{
		public RouteDataContext(string connection) : base(connection)
		{
		}

		[Function(Name = "LoadRoutes")]
		public ISingleResult<Route> LoadRoutes
		(
			[Parameter(Name = "search_expression")] string searchExpression
		)
		{
			IExecuteResult result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), searchExpression);
			return (ISingleResult<Route>)result.ReturnValue;
		}

		[Function(Name = "UpdateRoute")]
		public int UpdateRoute
		(
			[Parameter(Name = "id_route")] int? idRoute,
			[Parameter(Name = "name_route")] string nameRoute,
			[Parameter(Name = "descr")] string descr,
			[Parameter(Name = "picture")] byte[] picture
		)
		{
			var res = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), idRoute, nameRoute, descr, picture);
			return (int)res.ReturnValue;
		}

		[Function(Name = "DeleteRoute")]
		public int DeleteRoute
		(
			[Parameter(Name = "id_route")] int idRoute
		)
		{
			var result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), idRoute);
			return (int)result.ReturnValue;
		}

		[Function(Name = "DeleteListOfPlaces")]
		public int DeleteListOfPlaces
		(
			[Parameter(Name = "id_route")] int idRoute
		)
		{
			var result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), idRoute);
			return (int)result.ReturnValue;
		}

		[Function(Name = "InsertPlaceInRoute")]
		public int InsertPlaceInRoute
		(
			[Parameter(Name = "id_route")] int idRoute,
			[Parameter(Name = "id_place")] int idPlace
		)
		{
			var result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), idRoute, idPlace);
			return (int)result.ReturnValue;
		}
	}
}
