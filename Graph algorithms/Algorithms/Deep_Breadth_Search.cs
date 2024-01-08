using Graph_algorithms.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_algorithms.Algorithms
{
	public class Deep_Breadth_Search
	{
		/*Principle of the algorithm:

    1. Select a vertex of the graph and mark it as visited.
    2. Visit all neighboring vertices of this vertex that have not been marked as visited yet.
    3. For each visited vertex, repeat step 2.
        The algorithm terminates when all vertices in the graph have been visited.
       The algorithm can be implemented as a recursive function that will call itself to visit each neighboring vertex.*/

		private ObservableCollection<Vertex> vertices = new();
		private Stack<Vertex> stack_visited = new();
		private Stack<Vertex> stack_final = new();
		private Queue<Vertex> queue = new Queue<Vertex>();
		private ObservableCollection<int> stack_ = new();
		private ObservableCollection<int> stack_Renumber = new();
		private ObservableCollection<int> queue_ = new();
		private int milliseconds;

		public Deep_Breadth_Search(ObservableCollection<Vertex> vertice, int seconds, ObservableCollection<int> Stack_, ObservableCollection<int> stack_Renumber)
		{
			vertices = vertice;
			milliseconds = seconds;
			stack_ = Stack_;
			this.stack_Renumber = stack_Renumber;
		}
		public Deep_Breadth_Search(ObservableCollection<Vertex> vertice, int seconds, ObservableCollection<int> Queue_)
		{
			vertices = vertice;
			milliseconds = seconds;
			queue_ = Queue_;
		}
		public async void Depth_First()
		{
			Vertex currentVertex = vertices[0];
			stack_visited.Push(currentVertex);
			await Task.Delay(milliseconds);

			while (stack_visited.Count > 0)
			{
				currentVertex = stack_visited.Peek();
				if (currentVertex.ColorVertex == "Orange")
					VertColorChange(currentVertex, "Blue");
				BordColorChange(currentVertex, "Red");
				await Task.Delay(milliseconds);
				var list = Search(currentVertex);
				list.Reverse();
				if (list.Count == 0)
				{
					VertColorChange(currentVertex, "Green");
					stack_.Insert(0, currentVertex.Id);
					await Task.Delay(milliseconds);
					stack_final.Push(currentVertex);
					stack_visited.Pop();
				}
				else AddRange(list);

				BordColorChange(currentVertex, "Transparent");

			}
			ReNumber();
		}

		public async void Breadth_First()
		{
			Vertex currentVertex = vertices[0];
			queue.Enqueue(currentVertex);
			BordColorChange(currentVertex, "Red");
			await Task.Delay(milliseconds);
			VertColorChange(currentVertex, "Green");
			await Task.Delay(milliseconds);
			while (queue.Count > 0)
			{
				currentVertex = queue.Dequeue();
				queue_.Add(currentVertex.Id);
				BordColorChange(currentVertex, "Red");
				await Task.Delay(milliseconds);

				foreach (var vertex in Search(currentVertex))
				{
					queue.Enqueue(vertex);
					VertColorChange(vertex, "Blue");
					await Task.Delay(milliseconds);
				}
				VertColorChange(currentVertex, "Green");
				BordColorChange(currentVertex, "Transparent");
				await Task.Delay(milliseconds);
			}


		}

		private void ReNumber()
		{
			int i = 1;
			while (stack_final.Count > 0)
			{
				var vert = stack_final.Pop();
				vert.Id = i;
				stack_Renumber.Add(vert.Id);
				i++;
			}
		}

		private void VertColorChange(Vertex vert, string color) => vert.ColorVertex = color;
		private void BordColorChange(Vertex vert, string color) => vert.ColorVertexBorder = color;

		private List<Vertex> Search(Vertex current)
		{
			var list = new List<Vertex>();
			list.AddRange(current.Edges
				   .Where(x => x.From.Id == current.Id && (x.To.ColorVertex != "Blue" && x.To.ColorVertex != "Green"))
				   .Select(x => x.To)
				   .ToList());
			list.ForEach(x => x.ColorVertex = "Blue");
			return list;
		}

		private void AddRange(List<Vertex> list) => list.ForEach(x => stack_visited.Push(x));
	}
}
