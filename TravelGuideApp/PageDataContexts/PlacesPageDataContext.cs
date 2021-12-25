using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Windows;
using TravelGuideApp.Classes;
using TravelGuideApp.DataContexts;
using TravelGuideApp.Pages;
using TravelGuideApp.ProceduresClasses;

namespace TravelGuideApp.PageDataContexts
{
	public class PlacesPageDataContext : INotifyPropertyChanged
	{
		public PlacesPageDataContext(MainWindowDataContext parent)
		{
			_parent = parent;
		}

		private readonly MainWindowDataContext _parent;

		public List<Place> _listPlaces;

		public List<Place> ListPlaces
		{
			get { return _listPlaces; }
			set
			{
				_listPlaces = value;
				OnPropertyChanged("ListPlaces");
			}
		}

		private List<PlacesType> _listPlacesTypes;

		public List<PlacesType> ListPlacesTypes => _listPlacesTypes ?? (_listPlacesTypes = PlacesTypeProcedures.LoadPlacesTypes());

		private int? _idTypeFilter;

		public int? IdTypeFilter
		{
			get { return _idTypeFilter ?? null; }
			set
			{
				_idTypeFilter = value;
				ListPlaces = LoadPlaces();
			}
		}

		private string _searchExpression;

		public string SearchExpression
		{
			get { return _searchExpression ?? null; }
			set
			{
				_searchExpression = value;
				ListPlaces = LoadPlaces();
			}
		}

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

		public List<Place> LoadPlaces()
		{
			try
			{
				var dataContext = new PlaceContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var result = dataContext.LoadPlaces(null, IdTypeFilter, SearchExpression, Manager.currentUser?.IdUser).ToList();
				return result;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				return new List<Place>();
			}
		}

		private void OpenPlace()
		{
			PlacePage page = new PlacePage
			{
				DataContext = new PlacePageDataContext(SelectedPlace, _parent)
			};
			Manager.window.MainFrame.Navigate(page);
		}

		private RelayCommand _clearCommmand;

		public RelayCommand ClearCommand
		{
			get { return _clearCommmand ??( _clearCommmand = new RelayCommand(ClearFilter)); }
		}

		public void ClearFilter()
		{
			IdTypeFilter = null;
			LoadPlaces();
			OnPropertyChanged("IdTypeFilter");
			OnPropertyChanged("ListPlaces");
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
