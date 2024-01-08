using Graph_algorithms.Algorithms;
using Graph_algorithms.Helpers;
using Graph_algorithms.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Graph_algorithms.ViewModels
{
	public class ViewModel : INotifyPropertyChanged
	{

		private void NotifyPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		public event PropertyChangedEventHandler? PropertyChanged;

		public string? EdgeFrom { get; set; }
		public string? EdgeTo { get; set; }
		public string? Weight { get; set; } = 0.ToString();
		public string? FordFrom { get; set; }
		public string? FordTo { get; set; }
		public string? Dijkstra_From { get; set; }
		public string? Dijkstra_To { get; set; }

		public int Milliseconds { get; set; } = 1000;

		private string? maxFlowPath;
		public string? MaxFlowPath
		{
			get { return maxFlowPath; }
			set { maxFlowPath = value; NotifyPropertyChanged(nameof(MaxFlowPath)); }
		}

		private ObservableCollection<int> queue_ = new();
		public ObservableCollection<int> Queue_
		{
			get { return queue_; }
			set { queue_ = value; NotifyPropertyChanged(nameof(Queue_)); }
		}


		private ObservableCollection<int> stack_ = new();
		public ObservableCollection<int> Stack_
		{
			get { return stack_; }
			set { stack_ = value; NotifyPropertyChanged(nameof(Stack_)); }
		}

		private ObservableCollection<int> stack_Renumber = new();
		public ObservableCollection<int> Stack_Renumber
		{
			get { return stack_Renumber; }
			set { stack_Renumber = value; NotifyPropertyChanged(nameof(Stack_Renumber)); }
		}

		private ObservableCollection<Vertex> vertices = new();
		public ObservableCollection<Vertex> Vertices
		{
			get { return vertices; }
			set { vertices = value; NotifyPropertyChanged(nameof(Vertices)); }
		}

		private List<Tuple<int, int>>? tuples;

		private ObservableCollection<Edge> edges = new();
		public ObservableCollection<Edge> Edges
		{
			get { return edges; }
			set { edges = value; NotifyPropertyChanged(nameof(Edges)); }
		}

		private RelayCommand? addVertex;
		public RelayCommand? AddVertex
		{
			get
			{
				return addVertex ?? new RelayCommand(obj =>
				{
					if (((TextBox)obj).Text != string.Empty)
					{
						Vertex newVertex = new(int.Parse(((TextBox)obj).Text));
						Vertices.Add(newVertex);
						((TextBox)obj).Text = string.Empty;
					}
				}
				);
			}
		}

		private RelayCommand? deleteVertex;
		public RelayCommand? DeleteVertex
		{
			get
			{
				return deleteVertex ?? new RelayCommand(obj =>
				{
					Vertex v = (Vertex)obj;
					if (v != null)
					{
						foreach (Edge e in v.Edges)
						{
							Vertex to = e.To;
							to.Edges.Remove(e);
						}
						Vertices.Remove(v);
					}

					var edges_for_remove = Edges.Where(x => x.From.Id == v.Id).ToList();
					foreach (var e in edges_for_remove)
					{
						Edges.Remove(e);
					}
				}
				);
			}
		}

		private RelayCommand? clearVertex;
		public RelayCommand? ClearVertex
		{
			get
			{
				return clearVertex ?? new RelayCommand(obj =>
				{
					Vertices.Clear();
					Edges.Clear();
				}
				);
			}
		}


		private RelayCommand? addEdge;
		public RelayCommand? AddEdge
		{
			get
			{
				return addEdge ?? new RelayCommand(obj =>
				{
					if (EdgeFrom == null || EdgeTo == null) return;

					if (!Vertices.Any(x => x.Id.ToString() == EdgeFrom) || !Vertices.Any(x => x.Id.ToString() == EdgeTo)) return;

					if (EdgeFrom == EdgeTo) return;

					Edge newEdge = new(FindByName(EdgeFrom), FindByName(EdgeTo), int.Parse(Weight));
					edges.Add(newEdge);
					newEdge.From.AddEdge(newEdge);

					EdgeFrom = null; EdgeTo = null;
					NotifyPropertyChanged(nameof(EdgeFrom));
					NotifyPropertyChanged(nameof(EdgeTo));
				}
				);
			}
		}


		private RelayCommand? deleteEdge;
		public RelayCommand? DeleteEdge
		{
			get
			{
				return deleteEdge ?? new RelayCommand(obj =>
				{
					Edge v1 = (Edge)obj;
					Edges.Remove(v1);
					foreach (var item in vertices)
					{
						item.RemoveEdge(v1);
					}

				}
				);
			}
		}

		private RelayCommand? clearEdge;
		public RelayCommand? ClearEdge
		{
			get
			{
				return clearEdge ?? new RelayCommand(obj =>
				{
					Edges.Clear();
				}
				);
			}
		}

		private RelayCommand dijkstra;
		public RelayCommand Dijkstra
		{
			get
			{
				return dijkstra ?? new RelayCommand(obj =>
				{
					if(Vertices.Count == 0 || Edges.Count == 0) return;
					if (Dijkstra_From == null || Dijkstra_To == null) return;

					DijkstraAlgorithm dijkstra = new(Vertices, Edges);
					string x = dijkstra.FindFastestRoute(Dijkstra_From, Dijkstra_To);
				}
				);
			}
		}

		private RelayCommand clearDijkstra;
		public RelayCommand ClearDijkstra
		{
			get
			{
				return clearDijkstra ?? new RelayCommand(obj =>
				{
					Dijkstra_From = null; Dijkstra_To = null;
					NotifyPropertyChanged(nameof(Dijkstra_From));
					NotifyPropertyChanged(nameof(Dijkstra_To));
					foreach (var item in vertices)
					{
						item.ColorVertex = "Orange";
						item.Weight = int.MaxValue;
					}
					foreach (var item in Edges)
					{
						item.ColorEdge = "Gray";
					}
				}
				);
			}
		}

		private RelayCommand? fordFulkersonAlgorithm;
		public RelayCommand? FordFulkersonAlgorithm
		{
			get
			{
				return fordFulkersonAlgorithm ?? new RelayCommand(obj =>
				{
					if (Vertices.Count == 0 || Edges.Count == 0) return;
					if (FordFrom == null || FordTo == null) return;

					FordFulkerson ford = new();
					tuples = new();
					foreach (var edge in Edges)
					{
						tuples.Add(new Tuple<int, int>(edge.Capacity, edge.Flow));
					}
					MaxFlowPath = ford.MaxFlow(FindByName(FordFrom), FindByName(FordTo)).ToString();
					foreach (var item in Edges)
					{
						item.Capacity -= item.Flow;
					}
				}
				);
			}
		}
		List<int>? temp;
		private RelayCommand? dfs;
		public RelayCommand? Dfs
		{
			get
			{
				return dfs ?? new RelayCommand(obj =>
				{
					if (Vertices.Count == 0 || Edges.Count == 0) return;

					temp = new List<int>();
					foreach (var v in vertices)
					{
						temp.Add(v.Id);
					}
					Deep_Breadth_Search deep = new(vertices, Milliseconds, Stack_, stack_Renumber);
					deep.Depth_First();
				}
				);
			}
		}

		private RelayCommand? bfs;
		public RelayCommand? Bfs
		{
			get
			{
				return bfs ?? new RelayCommand(obj =>
				{
					if (Vertices.Count == 0 || Edges.Count == 0) return;

					Queue_.Clear();
					Deep_Breadth_Search deep = new(vertices, Milliseconds, Queue_);
					deep.Breadth_First();
				}
				);
			}
		}

		private RelayCommand? dfs_сlear;
		public RelayCommand? Dfs_Clear
		{
			get
			{
				return dfs_сlear ?? new RelayCommand(obj =>
				{
					if (temp != null && temp.Count > 0)
					{
						for (int i = 0; i < vertices.Count; i++)
						{
							vertices[i].Id = temp[i];
						}
						Stack_.Clear();
						stack_Renumber.Clear();
						foreach (var item in Vertices) item.ColorVertex = "Orange";
					}
				}
				);
			}
		}

		private RelayCommand? prima;
		public RelayCommand? Prima
		{
			get
			{
				return prima ?? new RelayCommand(obj =>
				{
					if (Vertices.Count == 0 || Edges.Count == 0) return;

					Prim_sAlgorithm prim_S = new(Edges, Vertices);
					prim_S.MinimumSpanningTree();
				}
				);
			}
		}

		private RelayCommand? сlearFord;
		public RelayCommand? ClearFord
		{
			get
			{
				return сlearFord ?? new RelayCommand(obj =>
				{

					if (tuples?.Count > 0)
					{
						for (int i = 0; i < Edges.Count; i++)
						{
							Edges[i].Capacity = tuples[i].Item1;
							Edges[i].Flow = tuples[i].Item2;
						}
						MaxFlowPath = "";
						FordFrom = null; FordTo = null;
						NotifyPropertyChanged(nameof(FordFrom));
						NotifyPropertyChanged(nameof(FordTo));
					}
				}
				);
			}
		}

		private RelayCommand? сlearPrima;
		public RelayCommand? ClearPrima
		{
			get
			{
				return сlearPrima ?? new RelayCommand(obj =>
				{
					Queue_.Clear();
					foreach (var item in Edges) item.ColorEdge = "Gray";
					foreach (var item in Vertices) item.ColorVertex = "Orange";
				}
				);
			}
		}


		private RelayCommand? save;
		public RelayCommand? Save
		{
			get
			{
				return save ?? new RelayCommand(obj =>
				{
					string currentDirectory = Directory.GetCurrentDirectory();

					SaveFileDialog saveFileDialog = new()
					{
						InitialDirectory = Directory.Exists($@"{currentDirectory}\Saves\") ? $@"{currentDirectory}\Saves\" : $@"{currentDirectory}",
						Title = "Save File Dialog",
						DefaultExt = ".json",
						
					};
					bool? result = saveFileDialog.ShowDialog();

					if (result == true)
					{
						var vert_json = JsonSerializer.Serialize(Vertices.ToList(), new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.Preserve });
						File.WriteAllText($"{saveFileDialog.FileName}", vert_json);
					}
				}
				);
			}
		}

		private RelayCommand? readFile;
		public RelayCommand? ReadFile
		{
			get
			{
				return readFile ?? new RelayCommand(obj =>
				{
					string currentDirectory = Directory.GetCurrentDirectory();
					OpenFileDialog openFileDialog = new()
					{
						InitialDirectory = Directory.Exists($@"{currentDirectory}\Saves\") ? $@"{currentDirectory}\Saves\" : $@"{currentDirectory}"
					};

					if (openFileDialog.ShowDialog() == true)
					{
						using StreamReader r = new(openFileDialog.OpenFile());
						var vert = JsonSerializer.Deserialize<List<Vertex>>(r.ReadToEnd(), new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.Preserve });

						if (vert != null)
						{	
							foreach (var ver in vert)
							{
								Vertices.Add(ver);

								if (ver.Edges != null)
								{
									foreach (var edge in ver.Edges)
									{
										Edges.Add(edge);
									}
								}
							}
						}

					}
				});
			}
		}

		private Vertex FindByName(string? name) => vertices.FirstOrDefault(x => x.Id.ToString() == name);
	}
}
