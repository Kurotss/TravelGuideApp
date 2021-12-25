using System;
using System.Configuration;
using System.Windows;
using TravelGuideApp.DataContexts;

namespace TravelGuideApp.ProceduresClasses
{
	public class UserProcedures
	{
		public static void SaveChanges(int IdUser, string NameUser, string Surname, int Age, string LoginUser, string PasswordUser, int? IdStation, int? IdLine, byte[] Avatar)
		{
			try
			{
				var dataContext = new UserContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				dataContext.UpdateUser(IdUser, NameUser, Surname, Age, LoginUser, PasswordUser, IdStation, IdLine, Avatar);
				MessageBox.Show("Данные обновлены!");
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}
	}
}
