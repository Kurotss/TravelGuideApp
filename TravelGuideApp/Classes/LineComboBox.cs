using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TravelGuideApp.Classes
{
	public class LineComboBox
	{
		public LineComboBox(Line item)
		{
			_item = item;
		}

		public Line _item;

		public int IdLine => _item.IdLine;

		public string NameLine => _item.NameLine;

		public SolidColorBrush ColorLine => (SolidColorBrush)new BrushConverter().ConvertFrom(_item.Color);

		public List<Station> ListStations => _item.ListStations;
	}
}
