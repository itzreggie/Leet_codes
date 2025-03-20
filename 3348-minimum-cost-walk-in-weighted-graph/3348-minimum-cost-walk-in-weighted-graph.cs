using System;
using System.Collections.Generic;

public class Solution {
    public int[] MinimumCost(int n, int[][] edges, int[][] query) {
        // Build the undirected graph.
        List<(int to, int w)>[] graph = new List<(int, int)>[n];
        for (int i = 0; i < n; i++) {
            graph[i] = new List<(int, int)>();
        }
        int FULL = 0;  // FULL will be the bitwise OR of all edge weights.
        foreach (var edge in edges) {
            int u = edge[0], v = edge[1], w = edge[2];
            graph[u].Add((v, w));
            graph[v].Add((u, w));
            FULL |= w;
        }
        
        int qLen = query.Length;
        int[] ans = new int[qLen];
        for (int i = 0; i < qLen; i++) {
            int s = query[i][0], t = query[i][1];
            ans[i] = MultiStateSearch(s, t, n, graph, FULL);
        }
        return ans;
    }
    
    // Runs a multi-state Dijkstra-like search from s.
    // Each state is (node, cost) where cost is the cumulative AND
    // of edge weights taken so far. We allow cycles so that bits may be dropped.
    private int MultiStateSearch(int s, int t, int n, List<(int to, int w)>[] graph, int FULL) {
        // For each node, store a list of cost values (non-dominated states).
        List<int>[] visited = new List<int>[n];
        for (int i = 0; i < n; i++) {
            visited[i] = new List<int>();
        }
        // Use a priority queue ordered by cost (lower cost has higher priority).
        var pq = new PriorityQueue<(int node, int cost), int>();
        pq.Enqueue((s, FULL), FULL);
        visited[s].Add(FULL);
        
        while(pq.Count > 0) {
            var cur = pq.Dequeue();
            int u = cur.node, cost = cur.cost;
            foreach (var edge in graph[u]) {
                int v = edge.to;
                int newCost = cost & edge.w;
                
                // Dominance check: we only want to add (v, newCost)
                // if there’s no already stored state at v that is a submask of newCost.
                bool skip = false;
                List<int> toRemove = null;
                foreach(var c in visited[v]) {
                    // If an existing cost c is a submask of newCost, then newCost is not better.
                    if ((c | newCost) == newCost) {
                        skip = true;
                        break;
                    }
                    // Conversely, if newCost is a submask of c, then newCost is better.
                    if ((c | newCost) == c) {
                        if(toRemove == null) toRemove = new List<int>();
                        toRemove.Add(c);
                    }
                }
                if (skip) continue;
                if (toRemove != null) {
                    foreach(var rem in toRemove) {
                        visited[v].Remove(rem);
                    }
                }
                visited[v].Add(newCost);
                pq.Enqueue((v, newCost), newCost);
            }
        }
        if (visited[t].Count == 0) return -1;
        int best = int.MaxValue;
        foreach (int c in visited[t])
            best = Math.Min(best, c);
        return best;
    }
}
