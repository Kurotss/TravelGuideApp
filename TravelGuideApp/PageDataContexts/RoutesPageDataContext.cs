using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Windows;
using TravelGuideApp.Classes;
using TravelGuideApp.DataContexts;
using TravelGuideApp.Pages;

namespace TravelGuideApp.PageDataContexts
{
	public class RoutesPageDataContext : INotifyPropertyChanged
	{
		public RoutesPageDataContext(MainWindowDataContext parent)
		{
			_parent = parent;
		}

		private readonly MainWindowDataContext _parent;

		public MainWindowDataContext Parent => _parent;

		private List<Route> _listRoutes;

		public List<Route> ListRoutes
		{
			get { return _listRoutes; }
			set { LoadRoutes(); }
		}

		private Route _selectedRoute;

		public Route SelectedRoute
		{
			get { return _selectedRoute ?? null; }
			set
			{
				_selectedRoute = value;
				OpenRoute();
			}
		}

		private string _searchExpression;

		public string SearchExpression
		{
			get { return _searchExpression ?? null; }
			set
			{
				_searchExpression = value;
				LoadRoutes();
				OnPropertyChanged("ListRoutes");
			}
		}

		public void LoadRoutes()
		{
			try
			{
				var dataContext = new RouteDataContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				_listRoutes = dataContext.LoadRoutes(SearchExpression).ToList();
			}
			catch(Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		public void OpenRoute()
		{
			RoutePage page = new RoutePage
			{
				DataContext = new RoutePageDataContext(SelectedRoute, Parent)
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
