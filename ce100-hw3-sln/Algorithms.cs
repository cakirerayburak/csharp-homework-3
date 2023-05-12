﻿using System;
using System.Collections;
using System.Linq;
/**
 * @author Eray Burak ÇAKIR - Süleyman Mert ALMALI
 */

namespace Algorithms
{

    /// <summary>
    /// Represents an edge in a graph with a starting node, ending node, and weight.
    /// </summary>
    public class Edge : IComparable<Edge>
    {
        /// <summary>
        /// The starting node of the edge.
        /// </summary>
        public int StartN { get; set; }

        /// <summary>
        /// The ending node of the edge.
        /// </summary>
        public int EndN { get; set; }

        /// <summary>
        /// The weight of the edge.
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Edge"/> class with the specified starting node, ending node, and weight.
        /// </summary>
        /// <param name="startN">The starting node of the edge.</param>
        /// <param name="endN">The ending node of the edge.</param>
        /// <param name="weight">The weight of the edge.</param>

        public Edge(int startN, int endN, int weight)
        {
            StartN = startN;
            EndN = endN;
            Weight = weight;
        }

        /// <summary>
        /// Compares this edge to another edge based on their weights.
        /// </summary>
        /// <param name="other">The other edge to compare to.</param>
        /// <returns>An integer that indicates the relative order of the edges.</returns>

        public int CompareTo(Edge? other)
        {
            return Weight.CompareTo(other?.Weight);
        }
    }


    /// <summary>
    /// Implements Kruskal's algorithm for finding the minimum spanning tree of a given graph represented as a set of edges.
    /// </summary>

     public class KruskalAlgorithm
    {
        /// <summary>
        /// Finds the minimum spanning tree of a graph given its number of vertices and a list of its edges.
        /// </summary>
        /// <param name="numberOfVertices">The number of vertices in the graph.</param>
        /// <param name="edges">The list of edges of the graph.</param>
        /// <returns>The list of edges that form the minimum spanning tree of the graph.</returns>
        /// <exception cref="ArgumentException">Thrown when the number of vertices is non-positive or the list of edges is null.</exception>


        public static List<Edge> Kruskal(int numberOfVertices, List<Edge> edges)
        {
            // Initial sort
            edges.Sort();

            // Set parents table
            var parent = Enumerable.Range(0, numberOfVertices).ToArray();

            // Spanning tree list
            var spanningTree = new List<Edge>();
            foreach (var edge in edges)
            {
                var startNodeRoot = FindRoot(edge.StartN, parent);
                var endNodeRoot = FindRoot(edge.EndN, parent);

                if (startNodeRoot != endNodeRoot)
                {
                    // Add edge to the spanning tree
                    spanningTree.Add(edge);

                    // Mark one root as parent of the other
                    parent[endNodeRoot] = startNodeRoot;
                }
            }

            // Return the spanning tree
            return spanningTree;
        }


        /// <summary>
        /// Finds the root node of the given node in the tree.
        /// Implements path compression optimization to speed up subsequent find operations.
        /// </summary>
        /// <param name="node">The node to find the root of.</param>
        /// <param name="parent">The array of parent nodes.</param>
        /// <returns>The root node of the tree containing the given node.</returns>

        private static int FindRoot(int node, int[] parent)
        {
            var root = node;
            var copy = (int[])parent.Clone(); // make a copy for parent

            while (root != copy[root])
            {
                copy[root] = copy[copy[root]]; // optimize path
                root = copy[root];
            }

            while (node != root)
            {
                var oldParent = copy[node];
                copy[node] = root;
                node = oldParent;
            }

            return root;
        }
    }
    


    /// <summary>
    /// Represents a weighted edge in a graph.
    /// </summary>

    public class BellmanFordAlgorithm
    {
        public class Edge
        {
            /// <summary>
            /// The starting node of the edge.
            /// </summary>
            
            public int Start { get; set; }

            /// <summary>
            /// The ending node of the edge.
            /// </summary>

            public int End { get; set; }

            /// <summary>
            /// The weight (cost) of the edge.
            /// </summary>

            public int Weight { get; set; }

        }


       
        /**

        @brief Computes the shortest path in a weighted directed graph using the Bellman-Ford algorithm.
        @param edges The list of edges in the graph, represented as tuples of the form (start, end, weight).
        @param startNode The index of the starting node in the graph.
        @param endNode The index of the ending node in the graph.
        @return A list of node indices representing the shortest path from the starting node to the ending node.
        @throw ArgumentException If the graph contains a negative-weight cycle.
        */


        public static List<int> ShortestPath(List<Edge> edges, int startNode, int endNode)
        {
            int n = edges.Count;
            int[] distances = new int[n];
            int[] previous = new int[n];

            for (int i = 0; i < n; i++)
            {
                distances[i] = int.MaxValue;
            }

            distances[startNode] = 0;

            for (int i = 1; i < n; i++)
            {
                bool hasChanged = false;

                foreach (Edge edge in edges)
                {
                    int u = edge.Start;
                    int v = edge.End;
                    int weight = edge.Weight;

                    if (distances[u] != int.MaxValue && distances[u] + weight < distances[v])
                    {
                        distances[v] = distances[u] + weight;
                        previous[v] = u;
                        hasChanged = true;
                    }
                }

                if (!hasChanged)
                {
                    break;
                }
            }

            // Check for negative-weight cycles
            foreach (Edge edge in edges)
            {
                int u = edge.Start;
                int v = edge.End;
                int weight = edge.Weight;

                if (distances[u] != int.MaxValue && distances[u] + weight < distances[v])
                {
                    throw new ArgumentException("The graph contains a negative-weight cycle.");
                }
            }

            // Build path list
            var path = new List<int> { endNode };
            int currentNode = endNode;
            while (currentNode != startNode)
            {
                currentNode = previous[currentNode];
                path.Insert(0, currentNode);
            }

            return path;
        }
    }



    public static class HuffmanAlgorithm
    {


        /// <summary>
        /// Represents a node in the Huffman tree for text data.
        /// </summary>



        public class Node_Txt
        {
            /// <summary>
            /// The symbol stored in this node.
            /// </summary>
            public char Symbol { get; set; }

            /// <summary>
            /// The frequency of the symbol in the input text.
            /// </summary>
            public int Frequency { get; set; }

            /// <summary>
            /// The left child node of this node.
            /// </summary>
            public Node_Txt Right { get; set; }

            /// <summary>
            /// The right child node of this node.
            /// </summary>
            public Node_Txt Left { get; set; }

            /// <summary>
            /// Traverses the Huffman tree to find the code for the given symbol.
            /// </summary>
            /// <param name="symbol">The symbol to find the code for.</param>
            /// <param name="data">The code data built so far.</param>
            /// <returns>The code for the given symbol, or null if the symbol is not found in the tree.</returns>
            public List<bool> Traverse(char symbol, List<bool> data)
            {

                if (Right == null && Left == null)
                {
                    if (symbol.Equals(this.Symbol))
                    {
                        return data;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    List<bool> left = null;
                    List<bool> right = null;

                    if (Left != null)
                    {
                        List<bool> leftPath = new List<bool>();
                        leftPath.AddRange(data);
                        leftPath.Add(false);

                        left = Left.Traverse(symbol, leftPath);
                    }

                    if (Right != null)
                    {
                        List<bool> rightPath = new List<bool>();
                        rightPath.AddRange(data);
                        rightPath.Add(true);
                        right = Right.Traverse(symbol, rightPath);
                    }

                    if (left != null)
                    {
                        return left;
                    }
                    else
                    {
                        return right;
                    }
                }
            }
        }

        public class HuffmanTree
        {
            private List<Node_Txt> nodes = new List<Node_Txt>();
            public Node_Txt Root { get; set; }
            public Dictionary<char, int> Frequencies = new Dictionary<char, int>();

            public void Build(string source)
            {
                for (int i = 0; i < source.Length; i++)
                {
                    if (!Frequencies.ContainsKey(source[i]))
                    {
                        Frequencies.Add(source[i], 0);
                    }

                    Frequencies[source[i]]++;
                }

                foreach (KeyValuePair<char, int> symbol in Frequencies)
                {
                    nodes.Add(new Node_Txt() { Symbol = symbol.Key, Frequency = symbol.Value });
                }

                while (nodes.Count > 1)
                {
                    List<Node_Txt> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<Node_Txt>();

                    if (orderedNodes.Count >= 2)
                    {

                        List<Node_Txt> taken = orderedNodes.Take(2).ToList<Node_Txt>();


                        Node_Txt parent = new Node_Txt()
                        {
                            Symbol = '*',
                            Frequency = taken[0].Frequency + taken[1].Frequency,
                            Left = taken[0],
                            Right = taken[1]
                        };

                        nodes.Remove(taken[0]);
                        nodes.Remove(taken[1]);
                        nodes.Add(parent);
                    }

                    this.Root = nodes.FirstOrDefault();

                }

            }

            public BitArray Encode(string source)
            {
                List<bool> encodedSource = new List<bool>();

                for (int i = 0; i < source.Length; i++)
                {
                    List<bool> encodedSymbol = this.Root.Traverse(source[i], new List<bool>());
                    encodedSource.AddRange(encodedSymbol);
                }

                BitArray bits = new BitArray(encodedSource.ToArray());

                return bits;
            }

            public string Decode(BitArray bits)
            {
                Node_Txt current = this.Root;
                string decoded = "";

                foreach (bool bit in bits)
                {
                    if (bit)
                    {
                        if (current.Right != null)
                        {
                            current = current.Right;
                        }
                    }
                    else
                    {
                        if (current.Left != null)
                        {
                            current = current.Left;
                        }
                    }

                    if (IsLeaf(current))
                    {
                        decoded += current.Symbol;
                        current = this.Root;
                    }
                }

                return decoded;
            }

            public bool IsLeaf(Node_Txt node)
            {
                return (node.Left == null && node.Right == null);
            }

        }

        public class Node_mp3
        {
            public byte Symbol { get; set; }
            public int Frequency { get; set; }
            public Node_mp3 Left { get; set; }
            public Node_mp3 Right { get; set; }

            public List<bool> Traverse_mp3(byte? symbol, List<bool> data)
            {
                // Leaf node
                if (Left == null && Right == null)
                {
                    if (Symbol == symbol)
                    {
                        return data;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    List<bool> left = null;
                    List<bool> right = null;

                    if (Left != null)
                    {
                        List<bool> leftPath = new List<bool>();
                        leftPath.AddRange(data);
                        leftPath.Add(false);

                        left = Left.Traverse_mp3(symbol, leftPath);
                    }

                    if (Right != null)
                    {
                        List<bool> rightPath = new List<bool>();
                        rightPath.AddRange(data);
                        rightPath.Add(true);

                        right = Right.Traverse_mp3(symbol, rightPath);
                    }

                    if (left != null)
                    {
                        return left;
                    }
                    else
                    {
                        return right;
                    }
                }
            }
        }

        public class HuffmanTree_mp3
        {
            private List<Node_mp3> nodes = new List<Node_mp3>();
            public Node_mp3 Root { get; set; }
            public Dictionary<byte, int> Frequencies = new Dictionary<byte, int>();

            public void Build(byte[] source)
            {
                for (int i = 0; i < source.Length; i++)
                {
                    if (!Frequencies.ContainsKey(source[i]))
                    {
                        Frequencies.Add(source[i], 0);
                    }

                    Frequencies[source[i]]++;
                }

                foreach (KeyValuePair<byte, int> symbol in Frequencies)
                {
                    nodes.Add(new Node_mp3() { Symbol = symbol.Key, Frequency = symbol.Value });
                }

                while (nodes.Count > 1)
                {
                    List<Node_mp3> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<Node_mp3>();

                    if (orderedNodes.Count >= 2)
                    {

                        List<Node_mp3> taken = orderedNodes.Take(2).ToList<Node_mp3>();


                        Node_mp3 parent = new Node_mp3()
                        {
                            Symbol = byte.MaxValue,
                            Frequency = taken[0].Frequency + taken[1].Frequency,
                            Left = taken[0],
                            Right = taken[1]
                        };

                        nodes.Remove(taken[0]);
                        nodes.Remove(taken[1]);
                        nodes.Add(parent);
                    }

                    this.Root = nodes.FirstOrDefault();
                }
            }

            public BitArray Encode(byte[] source)
            {
                List<bool> encodedSource = new List<bool>();

                for (int i = 0; i < source.Length; i++)
                {
                    List<bool> encodedSymbol = this.Root.Traverse_mp3(source[i], new List<bool>());
                    encodedSource.AddRange(encodedSymbol);
                }

                BitArray bits = new BitArray(encodedSource.ToArray());

                return bits;
            }

            public byte[] Decode(BitArray bits)
            {
                Node_mp3 current = this.Root;
                List<byte> decoded = new List<byte>();

                foreach (bool bit in bits)
                {
                    if (bit)
                    {
                        if (current.Right != null)
                        {
                            current = current.Right;
                        }
                    }
                    else
                    {
                        if (current.Left != null)
                        {
                            current = current.Left;
                        }
                    }

                    if (IsLeaf(current))
                    {
                        decoded.Add(current.Symbol);
                        current = this.Root;
                    }
                }

                return decoded.ToArray();
            }

            public bool IsLeaf(Node_mp3 node)
            {
                return (node.Left == null && node.Right == null);
            }
        }
        public static void WriteBitArray(BinaryWriter writer, BitArray bits)
        {
            byte[] bytes = new byte[(bits.Length + 7) / 8];
            bits.CopyTo(bytes, 0);
            writer.Write(bytes);
        }

        public static BitArray ReadBitArray(BinaryReader reader, long byteCount)
        {
            List<bool> bits = new List<bool>();
            byte[] bytes = reader.ReadBytes((int)byteCount);
            foreach (byte b in bytes)
            {
                for (int i = 0; i < 8; i++)
                {
                    bits.Add((b & (1 << i)) != 0);
                }
            }
            return new BitArray(bits.ToArray());
        }
    }



    public class Item
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<int> Dependencies { get; set; }

        public Item(string name, int id)
        {
            Name = name;
            Id = id;
            Dependencies = new List<int>();
        }
    }

    public class AssemblyGuide
    {
        private List<Item> steps;

        public AssemblyGuide()
        {
            steps = new List<Item>();
        }


        public void AddItem(List<Item> item)
        {
            steps.AddRange(item);
        }




        public ArrayList GetAssemblySteps()
        {
            List<Item> sortedItems = steps.OrderBy(item => TopologicalSort().IndexOf(item.Id)).ToList();
            ArrayList assemblySteps = new ArrayList();

            for (int i = 0; i < sortedItems.Count; i++)
            {
                Item item = sortedItems[i];
                string dependencies = string.Join(", ", item.Dependencies.Select(depId => steps.First(depItem => depItem.Id == depId).Name));
                assemblySteps.Add($"Step {i + 1}: Attach {item.Name}");
            }

            return assemblySteps;
        }


        private void DFS(int id, HashSet<int> visited, Stack<int> stack)
        {
            visited.Add(id);

            foreach (int dependency in steps[id - 1].Dependencies)
            {
                if (!visited.Contains(dependency))
                {
                    DFS(dependency, visited, stack);
                }
            }

            stack.Push(id);
        }

        private List<int> TopologicalSort()
        {
            HashSet<int> visited = new HashSet<int>();
            Stack<int> stack = new Stack<int>();

            foreach (Item item in steps)
            {
                if (!visited.Contains(item.Id))
                {
                    DFS(item.Id, visited, stack);
                }
            }

            return stack.Reverse().ToList();
        }



        

    }
}





