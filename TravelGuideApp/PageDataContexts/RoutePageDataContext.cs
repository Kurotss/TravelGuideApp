using System;
using System.ComponentModel;
using System.Configuration;
using System.Windows;
using TravelGuideApp.Classes;
using TravelGuideApp.DataContexts;
using TravelGuideApp.Pages;

namespace TravelGuideApp.PageDataContexts
{
	public class RoutePageDataContext : INotifyPropertyChanged
	{
		public RoutePageDataContext(Route currentRoute, MainWindowDataContext parent)
		{
			_route = currentRoute;
			_parent = parent;
		}

		private readonly MainWindowDataContext _parent;

		public MainWindowDataContext Parent => _parent;

		private Route _route;
		public Route Route => _route;

		private Place _selectedPlace;

		public Place SelectedPlace
		{
			get { return _selectedPlace; }
			set
			{
				_selectedPlace = value;
				OpenPlace();
			}
		}

		private string _descr;

		public string Descr
		{
			get { return _descr; }
			set
			{
				_descr = value;
				OnPropertyChanged("Descr");
			}
		}

		private int? _selectedRaiting;

		public int? SelectedRaiting
		{
			get { return _selectedRaiting; }
			set
			{
				_selectedRaiting = value;
				OnPropertyChanged("SelectedRaiting");
			}
		}

		private RelayCommand _postCommentCommmand;

		public RelayCommand PostCommentCommmand
		{
			get { return _postCommentCommmand ?? (_postCommentCommmand = new RelayCommand(PostComment)); }
		}

		public void PostComment()
		{
			if (SelectedRaiting == -1) MessageBox.Show("Выберите оценку");
			else
			{
				try
				{
					var dataContext = new CommentContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
					dataContext.PostComment((int)Route.IdRoute, Manager.currentUser.IdUser, Descr, (int)SelectedRaiting + 1, Tables.Route);
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
				}
				Route.ListComments = Route.LoadComments();
				Descr = null;
				SelectedRaiting = -1;
			}
		}

		private void OpenPlace()
		{
			PlacePage page = new PlacePage
			{
				DataContext = new PlacePageDataContext(SelectedPlace, Parent)
			};
			Manager.window.MainFrame.Navigate(page);
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
