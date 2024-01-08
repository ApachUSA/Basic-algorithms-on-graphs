using Graph_algorithms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_algorithms.Algorithms
{
	public class FordFulkerson
	{
		public int MaxFlow(Vertex source, Vertex sink)
		{
			int totalFlow = 0;

			while (true)
			{
				List<Edge> path = FindPath(source, sink);
				if (path == null)
					break;

				int pathFlow = int.MaxValue;
				foreach (Edge edge in path)
				{
					pathFlow = Math.Min(pathFlow, edge.Capacity - edge.Flow);
				}

				foreach (Edge edge in path)
				{
					edge.Flow += pathFlow;
				}

				totalFlow += pathFlow;
			}

			return totalFlow;
		}

		//search for a path in the graph between two given vertices using the breadth-first search (BFS) algorithm.
		private List<Edge> FindPath(Vertex source, Vertex sink)
		{
			Dictionary<Vertex, Edge> path = new Dictionary<Vertex, Edge>();
			Queue<Vertex> queue = new Queue<Vertex>();
			queue.Enqueue(source);

			while (queue.Count > 0)
			{
				Vertex current = queue.Dequeue();

				foreach (Edge edge in current.Edges)
				{
					if (edge.Capacity - edge.Flow > 0 && !path.ContainsKey(edge.To))
					{
						path[edge.To] = edge;
						if (edge.To == sink)
							break;

						queue.Enqueue(edge.To);
					}
				}
			}

			if (!path.ContainsKey(sink))
			{
				return null;
			}


			List<Edge> result = new();
			Vertex v = sink;
			while (v != source)
			{
				Edge e = path[v];
				result.Add(e);
				v = e.From;
			}

			result.Reverse();
			return result;
		}
	}
}
