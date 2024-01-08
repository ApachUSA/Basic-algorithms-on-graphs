using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_algorithms.Models
{
	public class Vertex : INotifyPropertyChanged
	{
		public List<Edge>? Edges { get; set; }
		public Vertex? Previous { get; set; }
		public event PropertyChangedEventHandler? PropertyChanged;

		private void NotifyPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}


		public Vertex() { }

		public Vertex(int id)
		{
			Id = id;
			Edges = new List<Edge>();
			VertexX = 10;
			VertexY = 10;
			ColorVertex = "Orange";
			Weight = int.MaxValue;
		}

		private int id;
		public int Id
		{
			get { return id; }
			set { id = value; NotifyPropertyChanged(nameof(Id)); }
		}

		private int vertexX;
		public int VertexX
		{
			get { return vertexX; }
			set { vertexX = value + 20; NotifyPropertyChanged(nameof(VertexX)); }
		}

		private int vertexY;
		public int VertexY
		{
			get { return vertexY; }
			set { vertexY = value + 20; NotifyPropertyChanged(nameof(VertexY)); }
		}

		private string? colorVertexBorder;
		public string? ColorVertexBorder
		{
			get { return colorVertexBorder; }
			set { colorVertexBorder = value; NotifyPropertyChanged(nameof(ColorVertexBorder)); }
		}

		private string? colorVertex;
		public string? ColorVertex
		{
			get { return colorVertex; }
			set { colorVertex = value; NotifyPropertyChanged(nameof(ColorVertex)); }
		}

		private int weight;
		public int Weight
		{
			get { return weight; }
			set { weight = value; NotifyPropertyChanged(nameof(Weight)); }
		}

		public void AddEdge(Edge e)
		{
			Edges?.Add(e);
		}

		public void RemoveEdge(Edge e)
		{
			Edges?.Remove(e);
		}
	}
}
