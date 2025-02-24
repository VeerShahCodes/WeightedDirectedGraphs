﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightedDirectedGraphs
{
    public class Graph<T>
    {
        private List<Vertex<T>> vertices;
        public IReadOnlyList<Vertex<T>> Vertices => vertices;
        public IReadOnlyList<Edge<T>> Edges
        {
            get
            {
                List<Edge<T>> edges = new List<Edge<T>>();
                for(int i = 0; i < vertices.Count; i++)
                {
                    for(int j = 0; j < vertices[i].NeighborCount; j++)
                    {
                        edges.Add(vertices[i].Neighbors[j]);

                    }
                }
                return edges;
            }
        }

        public int VertexCount => vertices.Count;

        public Graph()
        {
            vertices = new List<Vertex<T>>();
        }

        private void AddVertex(Vertex<T>? vertex)
        {
            if(vertex != null && vertex.NeighborCount == 0 && vertex.Owner != this)
            {
                vertices.Add(vertex);
                vertex.Owner = this;
            }
        }

        public void AddVertex(T val)
        {
            AddVertex(new Vertex<T>(val));
        }

        private bool RemoveVertex(Vertex<T>? vertex)
        {
            if(vertex == null || vertex.Owner != this) return false;

            for(int i = 0; i < Edges.Count; i++)
            {
                if (Edges[i].EndingPoint == vertex)
                {
                    Edges[i].StartingPoint.Neighbors.Remove(Edges[i]);
                }
            }
            vertices.Remove(vertex);
            return true;
        }

        public bool RemoveVertex(T val)
        {
            bool b = RemoveVertex(Search(val));
            return b;
        }

        private void AddEdge(Vertex<T>? a, Vertex<T>? b, float distance)
        {
            if(a != null && b != null && a.Owner == this && b.Owner == this)
            {
                Edge<T> edge = new Edge<T>(a, b, distance);
                if(!a.Neighbors.Contains(edge))
                {
                    a.Neighbors.Add(edge);
                }
            }
        }

        public void AddEdge(T val1, T val2, float distance)
        {
            AddEdge(Search(val1), Search(val2), distance);
        }

        

        private bool RemoveEdge(Vertex<T>? a, Vertex<T>? b)
        {
            if (a == null || b == null) return false;
            bool hasThatEdge = false;
            int index = 0;
            for(int i = 0; i < Edges.Count; i++)
            {
                if (Edges[i].StartingPoint == a && Edges[i].EndingPoint == b)
                {
                    hasThatEdge = true;
                    index = i;
                }
            }
            if (hasThatEdge == false) return false;

            a.Neighbors.Remove(Edges[index]);
            return true;
        }

        public bool RemoveEdge(T val1, T val2)
        {
            bool b = RemoveEdge(Search(val1), Search(val2));
            return b;
        }

        public Vertex<T>? Search(T? value)
        {
            if (value == null) return null;
            int z = -1;

            for(int i = 0; i < vertices.Count; i++)
            {
                if (value.Equals(vertices[i].Value))
                {
                    z = i;
                    break;
                }
            }

            if (z == -1) return null;
            else
            {
                return vertices[z];
            }

        }

        public Edge<T>? GetEdge(Vertex<T>? a, Vertex<T>? b)
        {
            if (a == null || b == null) return null;
            bool hasThatEdge = false;
            int index = 0;
            for (int i = 0; i < Edges.Count; i++)
            {
                if (Edges[i].StartingPoint == a && Edges[i].EndingPoint == b)
                {
                    hasThatEdge = true;
                    index = i;
                }
            }
            if (hasThatEdge == false) return null;

            return Edges[index];
        }

        public List<Vertex<T>>? PathFind(Vertex<T>? start, Vertex<T>? end)
        {
            if (start == null || end == null) return null;
            float pathCost = 0f;

            Dictionary<Vertex<T>, Vertex<T>?> previousVertex = new()
            {
                [start] = null
            };

            Queue<Vertex<T>> queue = new Queue<Vertex<T>>();
            queue.Enqueue(start);
            while(queue.Count > 0)
            {
                Vertex<T>? current = queue.Dequeue();
                if(current.Equals(end))
                {
                    List<Vertex<T>> path = new List<Vertex<T>>();
                    while (current != null)
                    {
                        path.Add(current);
                        if(GetEdge(previousVertex[current], current) != null)
                        {
                            pathCost += GetEdge(previousVertex[current], current).Distance;

                        }
                        current = previousVertex[current];
                    }
                    path.Reverse();
                    Console.WriteLine("Breadth First: " + pathCost);
                    return path;
                }

                for (int i = 0; i < current.NeighborCount; i++)
                {
                    if (!previousVertex.ContainsKey(current.Neighbors[i].EndingPoint))
                    {
                        queue.Enqueue(current.Neighbors[i].EndingPoint);
                        previousVertex[current.Neighbors[i].EndingPoint] = current;
                    }
                }
            }
            return null;
        }
    }
}
