using System;
using System.Collections.Generic;
using UnityEngine;


namespace DataStructures.Graphs
{
    public class Graph
    {
        public List<Vertex> AdjacencyList { get; set; }
        public Graph()
        {
            AdjacencyList = new List<Vertex>();
        }

        /// <summary>
        /// Adds a new vertex to the graph
        /// </summary>
        /// <param name="newVertex">Name of the new vertex</param>
        /// <returns>Returns the success of the operation</returns>
        public bool AddVertex(GameObject newVertex)
        {
            // We will keep the implementation simple and focus on the concepts
            // Ignore duplicate vertices.
            if (AdjacencyList.Find(v => v.Name == newVertex) != null) return true;

            // Add vertex to the graph
            AdjacencyList.Add(new Vertex(newVertex));
            return true;
        }

        /// <summary>
        /// Adds a new edge between two given vertices in the graph
        /// </summary>
        /// <param name="v1">Name of the first vertex</param>
        /// <param name="v2">Name of the second vertex</param>
        /// <returns>Returns the success of the operation</returns>
        public bool AddAnEdge(GameObject v1, GameObject v2)
        {
            // We will keep the implementation simple and focus on the concepts
            // Do not worry about handling invalid indexes or any other error cases.
            // We will assume all vertices are valid and already exist.

            // Add vertex v2 to the edges of vertex v1
            AdjacencyList.Find(v => v.Name == v1).Edges.Add(v2);

            // Add vertex v1 to the edges of vertex v2
            AdjacencyList.Find(v => v.Name == v2).Edges.Add(v1);

            return true;
        }

        /// <summary>
        /// Removes an edge between two given vertices in the graph
        /// </summary>
        /// <param name="v1">Name of the first vertex</param>
        /// <param name="v2">Name of the second vertex</param>
        /// <returns>Returns the success of the operation</returns>
        public bool RemoveAnEdge(GameObject v1, GameObject v2)
        {
            // We will keep the implementation simple and focus on the concepts
            // Do not worry about handling invalid indexes or any other error cases.
            // We will assume all vertices are valid and already exist.

            // Remove vertex v2 to the edges of vertex v1
            AdjacencyList.Find(v => v.Name == v1).Edges.Remove(v2);

            // Remove vertex v1 to the edges of vertex v2
            AdjacencyList.Find(v => v.Name == v2).Edges.Remove(v1);

            return true;
        }

        #region " DFS Traversal "

        /// <summary>
        /// Recursively traverse the graph and return an array of vertex names
        /// </summary>
        /// <param name="startVertex">Name for the starting vertex from where the traversal should start.</param>
        /// <returns>Returns array of GameObjects</returns>
        public List<GameObject> DFSRecursive(GameObject startVertex)
        {
            Vertex start = AdjacencyList.Find(v => v.Name == startVertex);
            if (start == null) return null;

            // List to keep track of the result
            List<GameObject> result = new List<GameObject>();

            // Lookup for keep track of visited nodes
            HashSet<GameObject> visited = new HashSet<GameObject>();

            DFSR(start, result, visited);
            return result;
        }

        private void DFSR(Vertex startVertex, List<GameObject> result, HashSet<GameObject> visited)
        {
            if (startVertex == null || visited.Contains(startVertex.Name)) return;

            //Add the vertex to the visited list
            result.Add(startVertex.Name);

            // Mark the vertex as visited
            visited.Add(startVertex.Name);

            // Traverse through the neighbors of the vertex
            foreach (var neighbor in startVertex.Edges)
            {
                // If the neighbor vertex is not visited already, perform DFS on the neighbor vertex
                if (!visited.Contains(neighbor))
                {
                    DFSR(AdjacencyList.Find(v => v.Name == neighbor), result, visited);
                }
            }
        }

        /// <summary>
        /// Iteratively traverse the graph and return an array of vertex names
        /// </summary>
        /// <param name="startVertex">Name for the starting vertex from where the traversal should start.</param>
        /// <returns>Returns array of GameObjects</returns>
        public List<GameObject> DFSIterative(GameObject startVertex)
        {
            Vertex start = AdjacencyList.Find(v => v.Name == startVertex);
            if (start == null) return null;

            List<GameObject> result = new List<GameObject>();
            HashSet<GameObject> visited = new HashSet<GameObject>();
            Stack<Vertex> stack = new Stack<Vertex>();
            stack.Push(start);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (visited.Contains(current.Name)) continue;
                result.Add(current.Name);
                visited.Add(current.Name);

                foreach (var neighbor in current.Edges)
                {
                    if (!visited.Contains(neighbor))
                    {
                        stack.Push(AdjacencyList.Find(v => v.Name == neighbor));
                    }
                }
            }
            return result;
        }

        #endregion

        /// <summary>
        /// Iteratively traverse the graph and return an array of vertex names
        /// </summary>
        /// <param name="startVertex">Name for the starting vertex from where the traversal should start.</param>
        /// <returns>Returns array of GameObjects</returns>
        public List<GameObject> BFSTraversal(GameObject startVertex)
        {
            Vertex start = AdjacencyList.Find(v => v.Name == startVertex);
            if (start == null) return null;

            List<GameObject> result = new List<GameObject>();
            HashSet<GameObject> visited = new HashSet<GameObject>();
            Queue<Vertex> queue = new Queue<Vertex>();
            queue.Enqueue(start);

            while(queue.Count > 0)
            {
                var current = queue.Dequeue();
                // If current vertex is already visited, move to the next vertex in the queue
                if(visited.Contains(current.Name)) continue;

                result.Add(current.Name);
                visited.Add(current.Name);

                foreach (var neighbor in current.Edges)
                {
                    if (!visited.Contains(neighbor))
                    {
                        queue.Enqueue(AdjacencyList.Find(v => v.Name == neighbor));
                    }
                }
            }

            return result;

        }
    }

    public class Vertex
    {
        /// <summary>
        /// Name of the vertex
        /// </summary>
        public GameObject Name { get; set; }

        /// <summary>
        /// All the edges connected to the given vertex
        /// </summary>
        public List<GameObject> Edges { get; set; }

        public Vertex(GameObject node)
        {
            this.Name = node;
            this.Edges = new List<GameObject>();
        }
    }
}