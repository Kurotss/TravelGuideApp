using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TravelGuideApp.Classes;

namespace TravelGuideApp.DataContexts
{
	public class CommentContext : DataContext
	{
		public CommentContext(string connection) : base(connection)
		{

		}

		[Function(Name = "LoadComments")]
		public ISingleResult<Comment> LoadComments
		(
			[Parameter(Name = "id_object")] int idObject,
			[Parameter(Name = "id_table")] Tables idTable
		)
		{
			IExecuteResult result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), idObject, idTable);
			return (ISingleResult<Comment>)result.ReturnValue;
		}

		[Function(Name = "PostComment")]
		public int PostComment
		(
			[Parameter(Name = "id_object")] int idObject,
			[Parameter(Name = "id_user")] int idUser,
			[Parameter(Name = "descr")] string descr,
			[Parameter(Name = "raiting")] int raiting,
			[Parameter(Name = "id_table")] Tables idTable
		)
		{
			var result = ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), idObject, idUser, descr, raiting, idTable);
			return (int)result.ReturnValue;
		}
	}
}
