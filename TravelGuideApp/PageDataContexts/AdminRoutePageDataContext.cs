using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelGuideApp.Classes;
using TravelGuideApp.ProceduresClasses;

namespace TravelGuideApp.PageDataContexts
{
	public class AdminRoutePageDataContext : INotifyPropertyChanged
	{
		public AdminRoutePageDataContext(Route currentRoute)
		{
			if (currentRoute == null)
			{
				_route = new Route
				{
					ListPlaces = new List<Place>()
				};
			}
			else _route = currentRoute;
			if (Route.Picture != null) _pictureRoute = Route.Picture;
			RoutePlaces = new ObservableCollection<Place>(_route.ListPlaces);
		}

		private Route _route;

		public Route Route => _route;

		private ObservableCollection<Place> _routePlaces;

		public ObservableCollection<Place> RoutePlaces
		{
			get => _routePlaces;
			set
			{
				_routePlaces = value;
				OnPropertyChanged(nameof(RoutePlaces));
			}
		}

		public List<Place> _listPlaces;

		public List<Place> ListPlaces
		{
			get => _listPlaces;
			set
			{
				_listPlaces = value;
				OnPropertyChanged(nameof(ListPlaces));
			}
		}

		private List<PlacesType> _listPlacesTypes;

		public List<PlacesType> ListPlacesTypes => _listPlacesTypes ?? (_listPlacesTypes = PlacesTypeProcedures.LoadPlacesTypes());

		private int _selectedType;

		public int SelectedType
		{
			get => _selectedType;
			set
			{
				_selectedType = value;
				ListPlaces = PlaceProcedures.LoadPlaces(null, SelectedType, null, null);
			}
		}

		private Place _selectedPlace;

		public Place SelectedPlace
		{
			get => _selectedPlace;
			set
			{
				if (value != null)
				{
					_selectedPlace = value;
					if (RoutePlaces.Where(p => p.IdPlace == _selectedPlace.IdPlace).Count() == 0)
					{
						RoutePlaces.Add(_selectedPlace);
					}
					else MessageBox.Show("Данная достопримечательность уже есть в маршруте!");
				}
			}
		}

		private Place _selectedPlaceView;

		public Place SelectedPlaceView
		{
			get => _selectedPlaceView;
			set
			{
				if (value != null)
				{
					_selectedPlaceView = value;
					RoutePlaces.Remove(_selectedPlaceView);
				}
			}
		}

		private RelayCommand _saveChangesCommand;

		public RelayCommand SaveChangesCommand
		{
			get { return _saveChangesCommand ?? (_saveChangesCommand = new RelayCommand(SaveChanges)); }
		}

		public void SaveChanges()
		{
			RouteProcedures.SaveChanges(Route.IdRoute, Route.NameRoute, Route.Descr, Route.Picture);
			if (Route.IdRoute != null)
			RouteProcedures.DeleteListOfPlaces((int)Route.IdRoute);
			foreach (Place place in RoutePlaces)
			{
				RouteProcedures.InsertPlaceInRoute((int)Route.IdRoute, (int)place.IdPlace);
			}
			if (Route.IdRoute == null)
			{
				_route = new Route();
				PictureRoute = null;
				OnPropertyChanged(nameof(Route));
			}
		}

		private RelayCommand _addNewPhotoCommand;

		public RelayCommand AddNewPhotoCommand
		{
			get { return _addNewPhotoCommand ?? (_addNewPhotoCommand = new RelayCommand(AddNewPhoto)); }
		}

		private byte[] _pictureRoute;
		public byte[] PictureRoute
		{
			get
			{ return _pictureRoute; }
			set
			{
				_pictureRoute = value;
				OnPropertyChanged(nameof(PictureRoute));
			}
		}

		public void AddNewPhoto()
		{
			OpenFileDialog dialog = new OpenFileDialog() { Filter = "Image files(*.png)|*.png" };
			if ((bool)dialog.ShowDialog())
			{
				try

				{
					Image image = Image.FromFile(dialog.FileName);
					ImageConverter converter = new ImageConverter();
					PictureRoute = (byte[])converter.ConvertTo(image, typeof(byte[]));
					Route.Picture = PictureRoute;
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
