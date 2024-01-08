using Graph_algorithms.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_algorithms.Algorithms
{
	public class DijkstraAlgorithm
	{
		private ObservableCollection<Vertex> vertices;
		private ObservableCollection<Edge> edges;
		private List<Vertex> tempVertexList;

		public DijkstraAlgorithm(ObservableCollection<Vertex> vert, ObservableCollection<Edge> edge)
		{
			vertices = vert;
			tempVertexList = vert.ToList();
			edges = edge;
		}

		public string FindFastestRoute(string start, string end)
		{
			FindByName(start).Weight = 0;
			do
			{
				var vertex = tempVertexList.OrderBy(x => x.Weight).FirstOrDefault();

				if (vertex != null)
				{
					foreach (var x in vertex.Edges)
					{
						if (tempVertexList.Contains(x.To))
						{
							if (x.To.Weight > vertex.Weight + x.Capacity)
							{
								x.To.Weight = vertex.Weight + x.Capacity;
								x.To.Previous = vertex;
							}
						}
					}
					tempVertexList.Remove(vertex);
				}

			} while (tempVertexList.Count() != 0);

			return GetPath(start, end);
		}

		private string GetPath(string start, string end)
		{
			var startVertex = FindByName(start);
			var endVertex = FindByName(end);
			var path = endVertex.Id.ToString() + " ";
			while (startVertex != endVertex)
			{
				endVertex = endVertex.Previous;
				path = endVertex.Id + " " + path;
			}

			var x = path.Split(' ').ToArray();
			for (int i = 0; i < x.Length - 1; i++)
			{
				FindByName(x[i].ToString()).ColorVertex = "Green";
				foreach (var item in edges)
				{
					if (item.From.Id.ToString() == x[i].ToString() && item.To.Id.ToString() == x[i + 1].ToString()
						|| item.To.Id.ToString() == x[i].ToString() && item.From.Id.ToString() == x[i + 1].ToString())
						item.ColorEdge = "Green";
				}
			}

			return path;
		}

		private Vertex FindByName(string name) => vertices.FirstOrDefault(x => x.Id.ToString() == name);
	}
}

