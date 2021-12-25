using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelGuideApp.Classes;
using TravelGuideApp.Pages;

namespace TravelGuideApp.PageDataContexts
{
	public class AdminWindowDataContext
	{
		private RelayCommand _personalAccountCommand;

		public RelayCommand PersonalAccountCommand
		{
			get { return _personalAccountCommand ?? (_personalAccountCommand = new RelayCommand(PersonalAccount)); }
		}

		public void PersonalAccount()
		{
			Manager.adminWindow.MainFrame.Navigate(new PersonalAccount());
		}

		private RelayCommand _openRoutesAdminPageCommand;

		public RelayCommand OpenRoutesAdminPageCommand
		{
			get { return _openRoutesAdminPageCommand ?? (_openRoutesAdminPageCommand = new RelayCommand(OpenRoutesAdminPage)); }
		}

		public void OpenRoutesAdminPage()
		{
			Manager.adminWindow.MainFrame.Navigate(new AdminRoutesPage { DataContext = new AdminRoutesPageDataContext() });
		}

		private RelayCommand _openPlacesAdminPageCommand;

		public RelayCommand OpenPlacesAdminPageCommand
		{
			get { return _openPlacesAdminPageCommand ?? (_openPlacesAdminPageCommand = new RelayCommand(OpenPlacesAdminPage)); }
		}

		public void OpenPlacesAdminPage()
		{
			Manager.adminWindow.MainFrame.Navigate(new AdminPlacesPage { DataContext = new AdminPlacesPageDataContext() });
		}
	}
}
