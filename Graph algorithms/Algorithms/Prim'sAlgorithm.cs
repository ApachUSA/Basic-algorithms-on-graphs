using Graph_algorithms.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_algorithms.Algorithms
{
	public class Prim_sAlgorithm
	{
		private ObservableCollection<Edge> _edges = new();
		private ObservableCollection<Vertex> vertices = new();

		public Prim_sAlgorithm(ObservableCollection<Edge> edge, ObservableCollection<Vertex> vertice)
		{
			_edges = edge;
			vertices = vertice;
			vertices[0].ColorVertex = "Green";

		}

		public void MinimumSpanningTree()
		{

			Edge currentedge;
			foreach (var vertix in vertices)
			{
				if (vertices.Any(x => x.ColorVertex == "Orange"))
				{
					currentedge = _edges
						.Where(x => (x.To.ColorVertex == "Green" && x.From.ColorVertex != "Green")
						|| (x.From.ColorVertex == "Green" && x.To.ColorVertex != "Green"))
						.OrderBy(x => x.Capacity)
						.First();
					if (currentedge.From.ColorVertex == "Green")
					{
						currentedge.To.ColorVertex = "Green";
						currentedge.ColorEdge = "Green";
					}
					if (currentedge.To.ColorVertex == "Green")
					{
						currentedge.From.ColorVertex = "Green";
						currentedge.ColorEdge = "Green";
					}
				}
			}


		}
	}
}
