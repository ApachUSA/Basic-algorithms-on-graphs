# Graph algorithms (Dijkstra, Prim's, Ford–Fulkerson, Depth-first search, Breadth-first search)

## Introduction
During my university studies there were several lab works related to graph algorithms. I decided to combine them into one application, as they have the same foundation.
This work is a little more than a year old, I would probably write a much better one now, but I'll leave it as it is. Maybe someone will find it useful as a good example of implementing graphs and algorithms.

## Technologies
![Static Badge](https://img.shields.io/badge/WPF-badge?style=for-the-badge&logo=WPF&color=%23292929)

- The system is implemented using **WPF** and **MVVM** pattern.
- Moving vertices is implemented using Microsoft.Xaml.Behaviors.Wpf


## Functions

### General
- The function of saving the graph and restoring works. ⚠️**When saving, specify only the file name (.json will be set automatically)**.
- Only oriented graphs are used in the application
- ⚠️ After using the algorithm, don't forget to clean the result with the eraser next to it.
### Dijkstra
Dijkstra's algorithm is an algorithm used to determine the shortest path from the initial node to all other nodes. The shortest path from node A to node B is marked in green.
### Depth-first search
Depth-first search (DFS) is one of the graph traversal methods. Depth-first search strategy, as the name suggests, is to go "deep" into the graph as far as possible. Visited but still unused vertices accumulate in the stack, so the later a vertex is visited, the sooner it will be used. 
- Only graph traversal is implemented, not search
- In the field you need to enter the time in milliseconds for animation
- The result is displayed in colors and additionally the stack is displayed
- At the end, topological sorting is applied and displayed next to it (vertex numbers change too).
### Breadth-first search
BFS (Breadth-First Search) is a graph search algorithm that explores all vertices in one level before moving to the next level. Breadth-first search, roughly speaking, is based on replacing the stack with a queue. After this modification, the earlier a vertex is visited (placed in the queue), the earlier it is used (removed from the queue).
- Only graph traversal is implemented, not search
- In the field you need to enter the time in milliseconds for animation
- The result is displayed in colors and additionally the queue is displayed
### Prim's
Prima Algorithm is an algorithm for constructing the minimum spanning tree of a weighted connected undirected graph.
*As it was said before, the system uses oriented graphs, so the algorithm checks not only the edge you created (1 -> 2), but also the reverse edge (2 -> 1)*.
- The final minimum spanning tree is displayed in green color.
### Ford–Fulkerson
The Ford-Falkerson algorithm is a greedy approach to calculate the maximum possible flow in a network or graph.
- The upper value of the edge is responsible for the capacity and the lower value for the flow
- The maximum flow is displayed at the bottom of the algorithm window
  
## Screenshots
<div align="center">
  <img src="/VapeShop.Domain/Screenshots/Login.png"/>
  <p>Login</p>
  <br/>
</div>