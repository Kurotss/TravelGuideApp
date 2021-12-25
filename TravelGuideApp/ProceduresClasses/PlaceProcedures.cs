using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using TravelGuideApp.Classes;
using TravelGuideApp.DataContexts;

namespace TravelGuideApp.ProceduresClasses
{
	public class PlaceProcedures
	{
		public static int SaveChanges(int? idPlace, string namePlace, string descr, string addressPlace, string coordinates, int idType, byte[] picture)
		{
			try
			{
				var dataContext = new PlaceContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var result = dataContext.UpdatePlace(idPlace, namePlace, descr, addressPlace, coordinates, idType, picture);
				if (idPlace != null) MessageBox.Show("Данные обновлены!");
				else MessageBox.Show("Данные успешно добавлены!");
				return result;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				return 0;
			}
		}

		public static int DeletePlace(int idPlace)
		{
			try
			{
				var dataContext = new PlaceContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var result = dataContext.DeletePlace(idPlace);
				MessageBox.Show("Данные успешно удалены!");
				return result;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				return 0;
			}
		}

		public static List<Place> LoadPlaces(int? idRoute, int? idType, string searchExpression, int? idUser)
		{
			try
			{
				var dataContext = new PlaceContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var result = dataContext.LoadPlaces(idRoute, idType, searchExpression, idUser).ToList();
				return result;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				return new List<Place>();
			}
		}
	}
}
