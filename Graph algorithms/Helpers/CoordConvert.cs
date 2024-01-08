using Graph_algorithms.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Graph_algorithms.Helpers
{
	public class CoordConvert : IMultiValueConverter
	{

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			Vertex from = (Vertex)values[1];
			Vertex to = (Vertex)values[2];

			int x1 = from.VertexX;
			int y1 = from.VertexY;
			int x2 = to.VertexX;
			int y2 = to.VertexY;
			//найти середину линии

			int xm = (x1 + x2) / 2;
			int ym = (y1 + y2) / 2;

			//длина линии
			double L1 = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
			double L2 = L1 - 80;

			return parameter switch
			{
				"X1" => xm + (x1 - xm) * (L2 / L1),
				"Y1" => ym + (y1 - ym) * (L2 / L1),
				"X2" => xm + (x2 - xm) * (L2 / L1),
				"Y2" => ym + (y2 - ym) * (L2 / L1),
				_ => (object)0,
			};
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
