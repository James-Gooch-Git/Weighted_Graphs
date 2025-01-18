using System;
using System.Collections.Generic;

class WeightedGraph
{
    private int Nodes;
    private List<(int, int)>[] AdjacencyList;

    public WeightedGraph(int nodes)
    {
        Nodes = nodes;
        AdjacencyList = new List<(int, int)>[nodes];
        for (int i = 0; i < nodes; i++)
        {
            AdjacencyList[i] = new List<(int, int)>();
        }
    }

    public void AddEdge(int u, int v, int weight)
    {
        AdjacencyList[u].Add((v, weight));
        AdjacencyList[v].Add((u, weight)); // Assuming an undirected graph
    }

    public double CalculateInfluenceScore(int source)
    {
        int[] distance = new int[Nodes];
        bool[] visited = new bool[Nodes];
        PriorityQueue<int, int> minHeap = new PriorityQueue<int, int>();

        // Initialize distances
        for (int i = 0; i < Nodes; i++)
        {
            distance[i] = int.MaxValue;
        }
        distance[source] = 0;

        // Push the source into the priority queue
        minHeap.Enqueue(source, 0);

        while (minHeap.Count > 0)
        {
            int current = minHeap.Dequeue();
            if (visited[current]) continue;

            visited[current] = true;

            foreach (var (neighbor, weight) in AdjacencyList[current])
            {
                if (!visited[neighbor] && distance[current] + weight < distance[neighbor])
                {
                    distance[neighbor] = distance[current] + weight;
                    minHeap.Enqueue(neighbor, distance[neighbor]);
                }
            }
        }

        // Calculate total distance
        int totalDistance = 0;
        for (int i = 0; i < Nodes; i++)
        {
            if (i != source && distance[i] != int.MaxValue)
            {
                totalDistance += distance[i];
            }
        }

        // Calculate influence score
        return totalDistance > 0 ? (double)(Nodes - 1) / totalDistance : 0.0;
    }
}

// Example Usage
class Program
{
    static void Main()
    {
        //WeightedGraph graph = new WeightedGraph(5);
        //graph.AddEdge(0, 1, 1);
        //graph.AddEdge(0, 2, 4);
        //graph.AddEdge(1, 3, 2);
        //graph.AddEdge(3, 4, 1);
        WeightedGraph weightedGraph = new WeightedGraph(10); // 10 nodes for A, B, C, ..., J

        string[] nodes = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        // Add edges with weights (using index-based mapping: A=0, B=1, ...)
        weightedGraph.AddEdge(0, 1, 1); // A -- B
        weightedGraph.AddEdge(0, 2, 1); // A -- C
        weightedGraph.AddEdge(0, 4, 5); // A -- E
        weightedGraph.AddEdge(1, 2, 4); // B -- C
        weightedGraph.AddEdge(1, 4, 1); // B -- E
        weightedGraph.AddEdge(1, 6, 1); // B -- G
        weightedGraph.AddEdge(1, 7, 1); // B -- H
        weightedGraph.AddEdge(2, 3, 3); // C -- D
        weightedGraph.AddEdge(2, 4, 1); // C -- E
        weightedGraph.AddEdge(3, 4, 2); // D -- E
        weightedGraph.AddEdge(3, 5, 1); // D -- F
        weightedGraph.AddEdge(3, 6, 5); // D -- G
        weightedGraph.AddEdge(4, 6, 2); // E -- G
        weightedGraph.AddEdge(5, 6, 1); // F -- G
        weightedGraph.AddEdge(6, 7, 2); // G -- H
        weightedGraph.AddEdge(7, 8, 3); // H -- I
        weightedGraph.AddEdge(8, 9, 3); // I -- J


        // Calculate and print influence scores
        Console.WriteLine("Influence Scores for Weighted Graph:");
        for (int node = 0; node < nodes.Length; node++)
        {
            double score = weightedGraph.CalculateInfluenceScore(node);
            Console.WriteLine($"Node {node} {nodes[node]}: Influence Score = {score}");
        }
           


  

        //Measure for X Node
        double Xscore = weightedGraph.CalculateInfluenceScore(0);//0 is placeholder insert any other node numner
        Console.WriteLine("Influence Score for Node 3: " + Xscore);
    }
}
