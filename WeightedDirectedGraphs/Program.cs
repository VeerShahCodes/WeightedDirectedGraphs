﻿namespace WeightedDirectedGraphs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Graph<int> graph = new Graph<int>();

            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);
            graph.AddVertex(6);
            graph.AddVertex(7);
            graph.AddVertex(8);

            graph.AddEdge(1, 2, 4.3f);
            graph.AddEdge(1, 3, 6.7f);
            graph.AddEdge(1, 4, 1.2f);
            graph.AddEdge(2, 5, 4.8f);
            graph.AddEdge(5, 1, 20f);
            graph.AddEdge(2, 8, 15f);
            graph.AddEdge(1, 8, 21f);
            graph.AddEdge(4, 7, 5f);
            graph.AddEdge(3, 6, 2.4f);

            List<Vertex<int>>? list = graph.PathFind(graph.Search(1), graph.Search(5));
            ;

        }
    }
}
