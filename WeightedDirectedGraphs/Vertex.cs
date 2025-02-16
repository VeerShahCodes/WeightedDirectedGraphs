﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightedDirectedGraphs
{
    public class Vertex<T>
    {
        public T Value { get; set; }
        public List<Edge<T>> Neighbors { get; set; }

        internal Graph<T> Owner { get; set; }

        public int NeighborCount => Neighbors.Count;

        public Vertex(T value)
        {
            Value = value;
            Neighbors = new List<Edge<T>>();
            Owner = new Graph<T>();
        }

    }
}
