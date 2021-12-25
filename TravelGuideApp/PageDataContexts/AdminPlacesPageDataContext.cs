using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelGuideApp.Classes;
using TravelGuideApp.DataContexts;
using TravelGuideApp.Pages;
using TravelGuideApp.ProceduresClasses;

namespace TravelGuideApp.PageDataContexts
{
	public class AdminPlacesPageDataContext : INotifyPropertyChanged
	{
		public AdminPlacesPageDataContext()
		{
			IsDelete = false;
		}

		private bool _isDelete;

		public bool IsDelete
		{
			get => _isDelete;
			set
			{
				_isDelete = value;
				OnPropertyChanged("IsDelete");
			}
		}

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
				if (value != null)
				{
					_selectedPlace = value;
					if (!IsDelete) OpenPlace();
					else DeletePlace();
				}
			}
		}

		public List<Place> LoadPlaces()
		{
			try
			{
				var dataContext = new PlaceContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var result = dataContext.LoadPlaces(null, IdTypeFilter, SearchExpression, null).ToList();
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
			AdminPlacePage page = new AdminPlacePage
			{
				DataContext = new AdminPlacePageDataContext(SelectedPlace, this)
			};
			Manager.adminWindow.MainFrame.Navigate(page);
		}

		private RelayCommand _clearCommmand;

		public RelayCommand ClearCommand
		{
			get { return _clearCommmand ?? (_clearCommmand = new RelayCommand(ClearFilter)); }
		}

		public void ClearFilter()
		{
			IdTypeFilter = null;
			LoadPlaces();
			OnPropertyChanged("IdTypeFilter");
			OnPropertyChanged("ListPlaces");
		}

		private RelayCommand _addPlaceCommmand;

		public RelayCommand AddPlaceCommand
		{
			get { return _addPlaceCommmand ?? (_addPlaceCommmand = new RelayCommand(AddPlace)); }
		}

		public void AddPlace()
		{
			AdminPlacePage page = new AdminPlacePage
			{
				DataContext = new AdminPlacePageDataContext(null, this)
			};
			Manager.adminWindow.MainFrame.Navigate(page);
		}

		private RelayCommand _deletePlaceCommmand;

		public RelayCommand DeletePlaceCommand => _deletePlaceCommmand ?? (_deletePlaceCommmand = new RelayCommand(DeletePlace));

		public void DeletePlace()
		{
			MessageBoxResult result = MessageBox.Show($"Вы точно хотите удалить эту {SelectedPlace.NamePlace}?", "Подтверждение", MessageBoxButton.YesNo);
			if (result == MessageBoxResult.Yes)
			{
				PlaceProcedures.DeletePlace((int)SelectedPlace.IdPlace);
				ListPlaces = LoadPlaces();
			}
		}

		private RelayCommand _deletButtonCommmand;

		public RelayCommand DeleteButtonCommand => _deletButtonCommmand ?? (_deletButtonCommmand = new RelayCommand(DeleteButton));

		public void DeleteButton()
		{
			IsDelete = !IsDelete;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
