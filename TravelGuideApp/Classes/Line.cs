using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Windows;
using TravelGuideApp.DataContexts;

namespace TravelGuideApp.Classes
{
	public class Line
	{
		[Column(Name = "id_line")]
		public int IdLine { get; set; }

		[Column(Name = "color")]
		public string Color { get; set; }

		[Column(Name = "name_line")]
		public string NameLine { get; set; }

		private List<Station> _listStations;

		public List<Station> ListStations
		{
			get { return _listStations; }
			set
			{
				try
				{
					var dataContext = new StationContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
					_listStations = dataContext.LoadStations(IdLine).ToList();
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
				}
			}
		}
	}
}
