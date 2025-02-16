using System;
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

        public void AddVertex(Vertex<T>? vertex)
        {
            if(vertex != null && vertex.NeighborCount == 0 && vertex.Owner != this)
            {
                vertices.Add(vertex);
                vertex.Owner = this;
            }
        }

        public bool RemoveVertex(Vertex<T>? vertex)
        {
            if(vertex == null || vertex.Owner != this) return false;

            for(int i = 0; i < vertices.Count; i++)
            {
                
            }
            return true;
        }

        public void AddEdge(Vertex<T>? a, Vertex<T>? b, float distance)
        {
            if(a != null && b != null && a.Owner != this && b.Owner != this)
            {
                Edge<T> edge = new Edge<T>(a, b, distance);
                if(!a.Neighbors.Contains(edge))
                {
                    a.Neighbors.Add(edge);
                }
            }
        }

        public bool RemoveEdge(Edge<T> edge)
        {
            if(edge.StartingPoint == null  ||  edge.EndingPoint == null || !Edges.Contains(edge)) return false;
            edge.SelfDestruct();
            return true;
        }
    }
}
