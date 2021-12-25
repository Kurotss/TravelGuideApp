using System.ComponentModel;
using TravelGuideApp.Classes;
using TravelGuideApp.Pages;

namespace TravelGuideApp.PageDataContexts
{
	public class MainWindowDataContext : INotifyPropertyChanged
	{
		public MainWindowDataContext()
		{
		}

		private bool _isLogin = false;

		public bool IsLogin
		{
			get { return _isLogin; }
			set
			{
				_isLogin = value;
				OnPropertyChanged("IsLogin");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private RelayCommand _personalAccountCommand;

		public RelayCommand PersonalAccountCommand
		{
			get { return _personalAccountCommand ?? (_personalAccountCommand = new RelayCommand(PersonalAccount)); }
		}

		public void PersonalAccount()
		{
			Manager.window.MainFrame.Navigate(new PersonalAccount());
		}

		private RelayCommand _openLogWindowCommand;

		public RelayCommand OpenLogWindowCommand
		{
			get { return _openLogWindowCommand ?? (_openLogWindowCommand = new RelayCommand(OpenLogWindow)); }
		}

		public void OpenLogWindow()
		{
			LoginWindow window = new LoginWindow
			{
				DataContext = new LoginWindowDataContext(this)
			};
			window.Show();
		}

		private RelayCommand _openPlacesPageCommand;

		public RelayCommand OpenPlacesPageCommand
		{
			get { return _openPlacesPageCommand ?? (_openPlacesPageCommand = new RelayCommand(OpenPlacesPage)); }
		}

		public void OpenPlacesPage()
		{
			Manager.window.MainFrame.Navigate(new PlacesPage { DataContext = new PlacesPageDataContext(this) });
		}

		private RelayCommand _openRoutesPageCommand;

		public RelayCommand OpenRoutesPageCommand
		{
			get { return _openRoutesPageCommand ?? (_openRoutesPageCommand = new RelayCommand(OpenRoutesPage)); }
		}

		public void OpenRoutesPage()
		{
			Manager.window.MainFrame.Navigate(new RoutesPage { DataContext = new RoutesPageDataContext(this) });
		}

		private RelayCommand _exitCommand;

		public RelayCommand ExitCommand
		{
			get { return _exitCommand ?? (_exitCommand = new RelayCommand(Exit)); }
		}

		public void Exit()
		{
			IsLogin = false;
			Manager.currentUser = null;
		}
	}
}
