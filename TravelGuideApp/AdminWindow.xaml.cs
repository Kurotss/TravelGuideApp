using System.Windows;
using TravelGuideApp.Classes;

namespace TravelGuideApp
{
	/// <summary>
	/// Логика взаимодействия для AdminWindow.xaml
	/// </summary>
	public partial class AdminWindow : Window
	{
		public AdminWindow()
		{
			InitializeComponent();
			Manager.adminWindow = this;
		}
	}
}
