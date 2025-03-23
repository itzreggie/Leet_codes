public class Solution {
    public int CountPaths(int n, int[][] roads) {
        const int MOD = 1_000_000_007;

        // Step 1: Build adjacency list
        var adj = new List<(int, int)>[n];
        for (int i = 0; i < n; i++) {
            adj[i] = new List<(int, int)>();
        }
        foreach (var road in roads) {
            adj[road[0]].Add((road[1], road[2]));
            adj[road[1]].Add((road[0], road[2]));
        }

        // Step 2: Initialize distances and ways
        var dist = new long[n];
        var ways = new long[n];
        Array.Fill(dist, long.MaxValue);
        dist[0] = 0;
        ways[0] = 1;

        // Step 3: Priority Queue for Dijkstra's Algorithm
        var pq = new SortedSet<(long, int)> { (0, 0) };

        while (pq.Count > 0) {
            var (currentDist, currentNode) = pq.Min;
            pq.Remove(pq.Min);

            foreach (var (neighbor, travelTime) in adj[currentNode]) {
                long newDist = currentDist + travelTime;

                if (newDist < dist[neighbor]) {
                    pq.Remove((dist[neighbor], neighbor)); // Remove outdated distance
                    dist[neighbor] = newDist;
                    ways[neighbor] = ways[currentNode];
                    pq.Add((newDist, neighbor));
                } else if (newDist == dist[neighbor]) {
                    ways[neighbor] = (ways[neighbor] + ways[currentNode]) % MOD;
                }
            }
        }

        return (int)(ways[n - 1] % MOD);
    }
}
