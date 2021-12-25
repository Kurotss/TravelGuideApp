using System;
using System.Configuration;
using System.Linq;
using System.Windows;
using TravelGuideApp.Classes;
using TravelGuideApp.DataContexts;

namespace TravelGuideApp.PageDataContexts
{
	public class LoginWindowDataContext
	{
		public LoginWindowDataContext(MainWindowDataContext parent)
		{
			_parent = parent;
		}

		private MainWindowDataContext _parent;

		private string _loginUser;

		public string LoginUser
		{
			get { return _loginUser; }
			set { _loginUser = value; }
		}

		private string _passwordUser;

		public string PasswordUser
		{
			get { return _passwordUser; }
			set { _passwordUser = value; }
		}

		private RelayCommand _loginCommand;

		public RelayCommand LoginCommand
		{
			get { return _loginCommand ?? (_loginCommand = new RelayCommand(Login)); }
		}

		public void Login()
		{
			try
			{
				var dataContext = new UserContext(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
				var result = dataContext.ExecLogin(LoginUser, PasswordUser).ToList();
				if (result.Count == 0) MessageBox.Show("Неверный логин или пароль");
				else
				{
					User user = result.FirstOrDefault();
					Manager.currentUser = user;
					_parent.IsLogin = true;
					if (user.IdRole == 1)
					{
						foreach (Window window in Application.Current.Windows)
						{
							if (window is LoginWindow)
							{
								window.Close();
								break;
							}
						}
					}
					else if (user.IdRole == 2)
					{
						AdminWindow adminWindow = new AdminWindow();
						adminWindow.Show();
						foreach (Window window in Application.Current.Windows)
						{
							if (!(window is AdminWindow))
							window.Close();
						}
					}
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}
	}
}
