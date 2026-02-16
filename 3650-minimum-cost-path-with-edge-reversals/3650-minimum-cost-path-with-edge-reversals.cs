using System;
using System.Collections.Generic;

public class Solution {
    public int MinCost(int n, int[][] edges) {
        // Build adjacency list with both normal and "reversed" edges
        List<(int to, long w)>[] graph = new List<(int, long)>[n];
        for (int i = 0; i < n; i++) {
            graph[i] = new List<(int, long)>();
        }

        foreach (var e in edges) {
            int u = e[0], v = e[1], w = e[2];

            // Normal edge: u -> v with cost w
            graph[u].Add((v, w));

            // Reversed edge: v -> u with cost 2 * w
            graph[v].Add((u, 2L * w));
        }

        // Dijkstra from 0 to n - 1
        long[] dist = new long[n];
        Array.Fill(dist, long.MaxValue);
        dist[0] = 0;

        var pq = new PriorityQueue<(int node, long cost), long>();
        pq.Enqueue((0, 0), 0);

        while (pq.Count > 0) {
            var (node, cost) = pq.Dequeue();
            if (cost > dist[node]) continue;
            if (node == n - 1) break;

            foreach (var (to, w) in graph[node]) {
                long newCost = cost + w;
                if (newCost < dist[to]) {
                    dist[to] = newCost;
                    pq.Enqueue((to, newCost), newCost);
                }
            }
        }

        return dist[n - 1] == long.MaxValue ? -1 : (int)dist[n - 1];
    }
}
