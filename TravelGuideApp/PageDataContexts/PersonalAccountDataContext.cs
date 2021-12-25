using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Windows;
using TravelGuideApp.Classes;
using TravelGuideApp.DataContexts;
using TravelGuideApp.ProceduresClasses;

namespace TravelGuideApp.PageDataContexts
{
	public class PersonalAccountDataContext : INotifyPropertyChanged
	{
		public PersonalAccountDataContext()
		{
			_listLines = LoadLines();
			if (CUser.Avatar != null) _avatarUser = CUser.Avatar;
			if (CUser.IdLine != null)
				SelectedLine = ListLinesCombobox.Find(p => p.IdLine == CUser.IdLine);
		}

		private List<Line> _listLines;

		private List<Station> _listStations;
		public List<Station> ListStations
		{
			get { return _listStations; }
			set { _listStations = value; }
		}

		private List<LineComboBox> _listLinesCombobox;

		public List<LineComboBox> ListLinesCombobox
		{
			get
			{
				if (_listLines != null)
				{
					_listLinesCombobox = new List<LineComboBox>();
					foreach (Line line in _listLines)
						_listLinesCombobox.Add(new LineComboBox(line));
				}
				return _listLinesCombobox;
			}
		}

		private LineComboBox _selectedLine;

		public LineComboBox SelectedLine
		{
			get
			{
				return _selectedLine;
			}
			set
			{
				_selectedLine = value;
				ListStations = ListLinesCombobox.Find(p => p.IdLine == SelectedLine.IdLine).ListStations;
				OnPropertyChanged("ListStations");
			}
		}

		public User CUser => Manager.currentUser;

		private RelayCommand _saveChangesCommand;

		public RelayCommand SaveChangesCommand
		{
			get { return _saveChangesCommand ?? (_saveChangesCommand = new RelayCommand(SaveChanges)); }
		}

		public void SaveChanges()
		{
			UserProcedures.SaveChanges(CUser.IdUser, CUser.NameUser, CUser.Surname, CUser.Age, CUser.LoginUser, CUser.PasswordUser, CUser.IdStation,
					SelectedLine.IdLine, CUser.Avatar);
		}

		public List<Line> LoadLines()
		{
			try
			{
				var dataContext = new LineContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var res = dataContext.LoadLines().ToList();
				return res;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				return new List<Line>();
			}
		}

		private RelayCommand _addNewPhotoCommand;

		public RelayCommand AddNewPhotoCommand
		{
			get { return _addNewPhotoCommand ?? (_addNewPhotoCommand = new RelayCommand(AddNewPhoto)); }
		}

		private byte[] _avatarUser;
		public byte[] AvatarUser
		{
			get
			{ return _avatarUser; }
			set
			{
				_avatarUser = value;
				OnPropertyChanged(nameof(AvatarUser));
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
					AvatarUser = (byte[])converter.ConvertTo(image, typeof(byte[]));
					CUser.Avatar = AvatarUser;
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
