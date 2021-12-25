using System;
using System.ComponentModel;
using System.Configuration;
using System.Windows;
using TravelGuideApp.Classes;
using TravelGuideApp.DataContexts;

namespace TravelGuideApp.PageDataContexts
{
	public class PlacePageDataContext : INotifyPropertyChanged
	{
		public PlacePageDataContext(Place currentPlace, MainWindowDataContext parent)
		{
			_place = currentPlace;
			_parent = parent;
			_isInBookmarks = currentPlace.IsInBookmarks;
		}

		private readonly MainWindowDataContext _parent;

		public MainWindowDataContext Parent => _parent;

		private readonly Place _place;
		public Place Place { get { return _place; } }

		private string _descr;

		public string Descr
		{
			get { return _descr; }
			set { _descr = value;
				OnPropertyChanged("Descr");
			}
		}

		private int? _selectedRaiting;

		public int? SelectedRaiting
		{
			get { return _selectedRaiting; }
			set { _selectedRaiting = value;
				OnPropertyChanged("SelectedRaiting");
			}
		}

		private bool _isInBookmarks;

		public bool IsInBookmarks
		{
			get {return _isInBookmarks; }
			set
			{
				_isInBookmarks = value;
				OnPropertyChanged("IsInBookmarks");
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
					dataContext.PostComment((int)Place.IdPlace, Manager.currentUser.IdUser, Descr, (int)SelectedRaiting + 1, Tables.Place);
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
				}
				Place.ListComments = Place.LoadComments();
				Descr = null;
				SelectedRaiting = -1;
			}
		}

		private RelayCommand _updateBookmarksCommmand;

		public RelayCommand UpdateBookmarksCommmand
		{
			get { return _updateBookmarksCommmand ?? (_updateBookmarksCommmand = new RelayCommand(UpdateBookmarks)); }
		}

		public void UpdateBookmarks()
		{
			try
			{
				var dataContext = new PlaceContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				dataContext.UpdateBookmarks(IsInBookmarks ? Actions.Delete : Actions.Add, Manager.currentUser.IdUser, (int)Place.IdPlace);
				IsInBookmarks = !IsInBookmarks;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
