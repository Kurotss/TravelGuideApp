using System.Configuration;
using System.Linq;
using System.Windows;
using TravelGuideApp.Classes;
using TravelGuideApp.DataContexts;
using TravelGuideApp.PageDataContexts;
using TravelGuideApp.Pages;

namespace TravelGuideApp
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			Manager.window = this;
		}
	}
}
