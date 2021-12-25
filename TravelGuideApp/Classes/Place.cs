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
	public class Place : INotifyPropertyChanged
	{
		[Column(Name = "id_place")]
		public int? IdPlace { get; set; }

		[Column(Name = "name_place")]
		public string NamePlace { get; set; }

		[Column(Name = "descr")]
		public string Descr { get; set; }

		[Column(Name = "id_type")]
		public int IdType { get; set; }

		[Column(Name = "name_type")]
		public string NameType { get; set; }

		[Column(Name = "raiting")]
		public string Raiting { get; set; }

		[Column(Name = "coordinates")]
		public string Coordinates { get; set; }

		[Column(Name = "address_place")]
		public string AddressPlace { get; set; }

		[Column(Name = "picture")]
		public byte[] Picture { get; set; }

		[Column(Name = "is_not_null_raiting")]
		public bool IsNotNullRaiting { get; set; }

		[Column(Name = "is_in_bookmarks")]
		public bool IsInBookmarks { get; set; }

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
				var result = dataContext.LoadComments((int)IdPlace, Tables.Place).ToList();
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