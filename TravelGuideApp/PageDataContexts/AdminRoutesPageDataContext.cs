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
	public class AdminRoutesPageDataContext : INotifyPropertyChanged
	{
		public AdminRoutesPageDataContext()
		{
			IsDelete = false;
		}

		private List<Route> _listRoutes;

		public List<Route> ListRoutes
		{
			get => _listRoutes;
			set
			{
				_listRoutes = value;
				OnPropertyChanged("ListRoutes");
			}
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

		private Route _selectedRoute;

		public Route SelectedRoute
		{
			get { return _selectedRoute ?? null; }
			set
			{
				if (value != null)
				{
					_selectedRoute = value;
					
					if (!IsDelete) OpenRoute();
					else DeleteRoute();
				}
			}
		}

		private string _searchExpression;

		public string SearchExpression
		{
			get { return _searchExpression ?? null; }
			set
			{
				_searchExpression = value;
				ListRoutes = LoadRoutes();
			}
		}

		public List<Route> LoadRoutes()
		{
			try
			{
				var dataContext = new RouteDataContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var result = dataContext.LoadRoutes(SearchExpression).ToList();
				return result;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				return new List<Route>();
			}
		}

		public void OpenRoute()
		{
			AdminRoutePage page = new AdminRoutePage
			{
				DataContext = new AdminRoutePageDataContext(SelectedRoute)
			};
			Manager.adminWindow.MainFrame.Navigate(page);
		}

		private RelayCommand _addRouteCommmand;

		public RelayCommand AddRouteCommand
		{
			get { return _addRouteCommmand ?? (_addRouteCommmand = new RelayCommand(AddRoute)); }
		}

		public void AddRoute()
		{
			AdminRoutePage page = new AdminRoutePage
			{
				DataContext = new AdminRoutePageDataContext(null)
			};
			Manager.adminWindow.MainFrame.Navigate(page);
		}

		private RelayCommand _deleteRouteCommmand;

		public RelayCommand DeleteRouteCommand => _deleteRouteCommmand ?? (_deleteRouteCommmand = new RelayCommand(DeleteRoute));

		public void DeleteRoute()
		{
			MessageBoxResult result = MessageBox.Show($"Вы точно хотите удалить {SelectedRoute.NameRoute}?", "Подтверждение", MessageBoxButton.YesNo);
			if (result == MessageBoxResult.Yes)
			{
				RouteProcedures.DeleteRoute((int)SelectedRoute.IdRoute);
				ListRoutes = LoadRoutes();
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
