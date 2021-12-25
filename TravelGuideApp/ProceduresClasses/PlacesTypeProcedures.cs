using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelGuideApp.Classes;
using TravelGuideApp.DataContexts;

namespace TravelGuideApp.ProceduresClasses
{
	public class PlacesTypeProcedures
	{
		public static List<PlacesType> LoadPlacesTypes()
		{
			try
			{
				var dataContext = new PlacesTypeContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var result = dataContext.LoadPlacesType().ToList();
				return result;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				return new List<PlacesType>();
			}
		}
	}
}
