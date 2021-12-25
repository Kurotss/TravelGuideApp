using System;
using System.Configuration;
using System.Windows;
using TravelGuideApp.DataContexts;

namespace TravelGuideApp.ProceduresClasses
{
	public class RouteProcedures
	{
		public static int SaveChanges(int? idRoute, string nameRoute, string descr, byte[] picture)
		{
			try
			{
				var dataContext = new RouteDataContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var result = dataContext.UpdateRoute(idRoute, nameRoute, descr, picture);
				if (idRoute != null) MessageBox.Show("Данные обновлены!");
				else MessageBox.Show("Данные успешно добавлены!");
				return result;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				return 0;
			}
		}

		public static int DeleteRoute(int idRoute)
		{
			try
			{
				var dataContext = new RouteDataContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var result = dataContext.DeleteRoute(idRoute);
				MessageBox.Show("Данные успешно удалены!");
				return result;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				return 0;
			}
		}

		public static int DeleteListOfPlaces(int idRoute)
		{
			try
			{
				var dataContext = new RouteDataContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var result = dataContext.DeleteListOfPlaces(idRoute);
				return result;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				return 0;
			}
		}

		public static int InsertPlaceInRoute(int idRoute, int idPlace)
		{
			try
			{
				var dataContext = new RouteDataContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var result = dataContext.InsertPlaceInRoute(idRoute, idPlace);
				return result;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				return 0;
			}
		}
	}
}
