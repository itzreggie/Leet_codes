using System;
using System.Collections.Generic;

public class Solution 
{
    public int[] MaxTargetCounts(int[][] edges1, int[][] edges2, int k) 
    {
        // Build tree1 and tree2 as undirected graphs.
        int n = edges1.Length + 1; // Tree1 has n nodes labeled 0 .. n-1.
        int m = edges2.Length + 1; // Tree2 has m nodes labeled 0 .. m-1.
        
        List<int>[] tree1 = BuildGraph(n, edges1);
        List<int>[] tree2 = BuildGraph(m, edges2);
        
        // For each node in tree1, count the number of nodes at distance <= k.
        int[] countTree1 = new int[n];
        for (int i = 0; i < n; i++)
        {
            countTree1[i] = BfsCount(tree1, i, k);
        }
        
        // For tree2, the “extra” reachable nodes via the bridge come
        // from nodes that are within distance <= (k - 1) from the chosen connector.
        // If k == 0 then no tree2 node is reachable (the bridge would add 1 edge).
        int bestTree2 = 0;
        if (k - 1 >= 0)
        {
            for (int i = 0; i < m; i++)
            {
                int cnt = BfsCount(tree2, i, k - 1);
                bestTree2 = Math.Max(bestTree2, cnt);
            }
        }
        // Otherwise, if k == 0 bestTree2 stays 0.
        
        // The answer for each node in tree1 is simply:
        // (number of tree1 nodes within distance k) + (best tree2 ball size)
        int[] answer = new int[n];
        for (int i = 0; i < n; i++)
        {
            answer[i] = countTree1[i] + bestTree2;
        }
        
        return answer;
    }
    
    // Helper to build an undirected graph given number of nodes and edge list.
    private List<int>[] BuildGraph(int nodeCount, int[][] edges)
    {
        List<int>[] graph = new List<int>[nodeCount];
        for (int i = 0; i < nodeCount; i++)
            graph[i] = new List<int>();
        
        foreach(var edge in edges)
        {
            int u = edge[0], v = edge[1];
            graph[u].Add(v);
            graph[v].Add(u);
        }
        return graph;
    }
    
    // Performs BFS starting from 'start' and counts number of nodes within 'radius' edges.
    private int BfsCount(List<int>[] graph, int start, int radius)
    {
        int n = graph.Length;
        bool[] visited = new bool[n];
        Queue<(int node, int dist)> queue = new Queue<(int, int)>();
        queue.Enqueue((start, 0));
        int count = 0;
        
        while(queue.Count > 0)
        {
            var (node, dist) = queue.Dequeue();
            if(dist > radius)
                continue;
            
            if(visited[node])
                continue;
            
            visited[node] = true;
            count++;
            // Expand neighbors if we're not at the maximum distance yet.
            if(dist < radius)
            {
                foreach (int nei in graph[node])
                {
                    if (!visited[nei])
                        queue.Enqueue((nei, dist + 1));
                }
            }
        }
        return count;
    }
}
