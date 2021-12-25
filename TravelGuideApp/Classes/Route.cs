using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Windows;
using TravelGuideApp.DataContexts;

namespace TravelGuideApp.Classes
{
	public class Route : INotifyPropertyChanged
	{
		[Column(Name = "id_route")]
		public int? IdRoute { get; set; }

		[Column(Name = "name_route")]
		public string NameRoute { get; set; }

		[Column(Name = "descr")]
		public string Descr { get; set; }

		[Column(Name = "raiting")]
		public string Raiting { get; set; }

		[Column(Name = "id_user")]
		public int? IdUser { get; set; }

		[Column(Name = "fi_user")]
		public string FiUser { get; set; }

		[Column(Name = "is_not_null_raiting")]
		public bool IsNotNullRaiting { get; set; }

		[Column(Name = "picture")]
		public byte[] Picture { get; set; }

		private List<Place> _listPlaces;

		public List<Place> ListPlaces
		{
			get { return _listPlaces; }
			set
			{
				try
				{
					var dataContext = new PlaceContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
					_listPlaces = dataContext.LoadPlaces(IdRoute, null, null, null).ToList();
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
				}
			}
		}

		private List<Comment> _listComments;

		public List<Comment> ListComments
		{
			get { return _listComments; }
			set
			{
				_listComments = LoadComments();
				OnPropertyChanged("ListComments");
			}
		}

		public List<Comment> LoadComments()
		{
			try
			{
				var dataContext = new CommentContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var result = dataContext.LoadComments((int)IdRoute, Tables.Route).ToList();
				return result;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				return new List<Comment>();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
