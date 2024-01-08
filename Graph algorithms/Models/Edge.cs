using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_algorithms.Models
{
	public class Edge : INotifyPropertyChanged
	{

		public Vertex? From { get; set; }
		public Vertex? To { get; set; }
		public event PropertyChangedEventHandler? PropertyChanged;

		private void NotifyPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}


		public Edge(Vertex from, Vertex to, int capacity)
		{
			From = from;
			To = to;
			Capacity = capacity;
			Flow = 0;
			ColorEdge = "Gray";
		}
		public Edge() { }

		private int capacity;
		public int Capacity
		{
			get { return capacity; }
			set { capacity = value; NotifyPropertyChanged(nameof(Capacity)); }
		}
		private int flow;
		public int Flow
		{
			get { return flow; }
			set { flow = value; NotifyPropertyChanged(nameof(Flow)); }
		}

		private string? colorEdge;
		public string? ColorEdge
		{
			get { return colorEdge; }
			set { colorEdge = value; NotifyPropertyChanged(nameof(ColorEdge)); }
		}
	}
}
