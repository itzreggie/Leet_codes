public class Solution {
    public int[] FindRedundantConnection(int[][] edges) {
        int n = edges.Length;
        int[] parent = new int[n + 1];
        for (int i = 1; i <= n; i++) parent[i] = i;

        int Find(int x) {
            if (parent[x] != x) parent[x] = Find(parent[x]);
            return parent[x];
        }

        void Union(int x, int y) {
            int rootX = Find(x);
            int rootY = Find(y);
            if (rootX != rootY) parent[rootX] = rootY;
        }

        foreach (var edge in edges) {
            int u = edge[0], v = edge[1];
            if (Find(u) == Find(v)) return edge;
            Union(u, v);
        }

        return new int[0];
    }
}
