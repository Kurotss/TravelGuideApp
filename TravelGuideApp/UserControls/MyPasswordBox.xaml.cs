﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TravelGuideApp.UserControls
{
	/// <summary>
	/// Логика взаимодействия для MyPasswordBox.xaml
	/// </summary>
	public partial class MyPasswordBox : UserControl
	{

		// Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PasswordProperty =
			DependencyProperty.Register("Password", typeof(string), typeof(MyPasswordBox), new PropertyMetadata(string.Empty));

		public string Password
		{
			get { return (string)GetValue(PasswordProperty); }
			set { SetValue(PasswordProperty, value); }
		}


		public MyPasswordBox()
		{
			InitializeComponent();
		}

		private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
		{
			Password = txtPassword.Password;
		}
	}
}
