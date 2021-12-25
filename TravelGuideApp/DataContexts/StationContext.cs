using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using TravelGuideApp.Classes;

namespace TravelGuideApp.DataContexts
{
	public class StationContext : DataContext
	{
		public StationContext(string connection) : base(connection)
		{
		}

		[Function(Name = "LoadStations")]
		public ISingleResult<Station> LoadStations
		(
			[Parameter(Name = "id_line")] int idLine
		)
		{
			IExecuteResult result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), idLine);
			return (ISingleResult<Station>)result.ReturnValue;
		}
	}
}
