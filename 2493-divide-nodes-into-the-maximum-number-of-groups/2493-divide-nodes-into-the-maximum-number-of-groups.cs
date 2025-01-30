using System;
using System.Collections.Generic;

public class Solution {
    public int MagnificentSets(int n, int[][] edges) {
        // Graph represented as an adjacency list
        List<int>[] graph = new List<int>[n + 1];
        for (int i = 1; i <= n; i++) {
            graph[i] = new List<int>();
        }
        // Building the graph
        foreach (var edge in edges) {
            int from = edge[0], to = edge[1];
            graph[from].Add(to);
            graph[to].Add(from);
        }

        List<int> nodes = new List<int>();
        bool[] visited = new bool[n + 1]; // Array to keep track of visited nodes
        int totalCount = 0; // To store the sum of magnificent sets found

        // Depth First Search (DFS) to traverse components of the graph
        Action<int> depthFirstSearch = null;
        depthFirstSearch = (int node) => {
            nodes.Add(node);
            visited[node] = true;
            foreach (int neighbor in graph[node]) {
                if (!visited[neighbor]) {
                    depthFirstSearch(neighbor);
                }
            }
        };

        // Breadth First Search (BFS) to find the largest distance in a single component
        Func<int, int> breadthFirstSearch = (int startNode) => {
            int localCount = 1;
            int[] distances = new int[n + 1];
            for (int i = 1; i <= n; i++) {
                distances[i] = int.MaxValue;
            }
            distances[startNode] = 1;
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(startNode);

            while (queue.Count > 0) {
                int currentNode = queue.Dequeue();
                foreach (int neighbor in graph[currentNode]) {
                    if (distances[neighbor] == int.MaxValue) {
                        localCount = distances[neighbor] = distances[currentNode] + 1;
                        queue.Enqueue(neighbor);
                    }
                }
            }

            // Update distances for all nodes to ensure they are reachable
            foreach (int node in nodes) {
                if (distances[node] == int.MaxValue) {
                    distances[node] = ++localCount;
                }
            }

            // Check if there is a violation in magnificent set property
            foreach (int node in nodes) {
                foreach (int neighbor in graph[node]) {
                    if (Math.Abs(distances[node] - distances[neighbor]) != 1) {
                        return -1;
                    }
                }
            }
            return localCount;
        };

        // Loop over all nodes to explore every component of the graph
        for (int i = 1; i <= n; i++) {
            if (!visited[i]) {
                depthFirstSearch(i);
                int maxDistance = -1;
                foreach (int node in nodes) {
                    maxDistance = Math.Max(maxDistance, breadthFirstSearch(node));
                }
                if (maxDistance == -1) return -1; // If impossible to find a magnificent set
                totalCount += maxDistance; // Add the maximum distance found in this component
                nodes.Clear();
            }
        }
        return totalCount;
    }
}