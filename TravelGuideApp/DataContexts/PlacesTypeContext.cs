using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using TravelGuideApp.Classes;

namespace TravelGuideApp.DataContexts
{
	public class PlacesTypeContext : DataContext
	{
		public PlacesTypeContext(string connection) : base(connection)
		{
		}

		[Function(Name = "LoadPlacesTypes")]
		public ISingleResult<PlacesType> LoadPlacesType()
		{
			IExecuteResult result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod());
			return (ISingleResult<PlacesType>)result.ReturnValue;
		}
	}
}
