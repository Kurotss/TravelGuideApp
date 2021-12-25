using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using TravelGuideApp.Classes;

namespace TravelGuideApp.DataContexts
{
	public class LineContext : DataContext
	{
		public LineContext(string connection) : base(connection)
		{
		}

		[Function(Name = "LoadLines")]
		public ISingleResult<Line> LoadLines()
		{
			IExecuteResult result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod());
			return (ISingleResult<Line>)result.ReturnValue;
		}
	}
}
