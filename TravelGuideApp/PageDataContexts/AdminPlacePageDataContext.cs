using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
	public class AdminPlacePageDataContext : INotifyPropertyChanged
	{
		public AdminPlacePageDataContext(Place currentPlace, AdminPlacesPageDataContext parent)
		{
			if (currentPlace == null) _place = new Place();
			else _place = currentPlace;
			if (Place.Picture != null) _picturePlace = Place.Picture;
			_parent = parent;
		}

		private Place _place;
		public Place Place => _place;

		private AdminPlacesPageDataContext _parent;

		private RelayCommand _saveChangesCommand;

		public RelayCommand SaveChangesCommand
		{
			get { return _saveChangesCommand ?? (_saveChangesCommand = new RelayCommand(SaveChanges)); }
		}

		public void SaveChanges()
		{
			PlaceProcedures.SaveChanges(Place.IdPlace, Place.NamePlace, Place.Descr, Place.AddressPlace, Place.Coordinates, Place.IdType, Place.Picture);
			if (Place.IdPlace == null)
			{
				_place = new Place();
				PicturePlace = null;
				OnPropertyChanged(nameof(Place));
			}
		}

		private List<PlacesType> _listPlacesTypes;

		public List<PlacesType> ListPlacesTypes => _listPlacesTypes ?? (_listPlacesTypes = PlacesTypeProcedures.LoadPlacesTypes());

		private RelayCommand _addNewPhotoCommand;

		public RelayCommand AddNewPhotoCommand
		{
			get { return _addNewPhotoCommand ?? (_addNewPhotoCommand = new RelayCommand(AddNewPhoto)); }
		}

		private byte[] _picturePlace;
		public byte[] PicturePlace
		{
			get
			{ return _picturePlace; }
			set
			{
				_picturePlace = value;
				OnPropertyChanged(nameof(PicturePlace));
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
					PicturePlace = (byte[])converter.ConvertTo(image, typeof(byte[]));
					Place.Picture = PicturePlace;
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
