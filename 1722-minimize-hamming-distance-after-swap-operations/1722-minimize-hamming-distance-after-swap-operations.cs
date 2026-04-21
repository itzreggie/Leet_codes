public class Solution {
    public int MinimumHammingDistance(int[] source, int[] target, int[][] allowedSwaps) {
        int n = source.Length;
        UnionFind uf = new UnionFind(n);

        // Build connected components using allowed swaps
        foreach (var swap in allowedSwaps) {
            uf.Union(swap[0], swap[1]);
        }

        // Group indices by their root parent
        Dictionary<int, List<int>> groups = new Dictionary<int, List<int>>();
        for (int i = 0; i < n; i++) {
            int root = uf.Find(i);
            if (!groups.ContainsKey(root))
                groups[root] = new List<int>();
            groups[root].Add(i);
        }

        int hamming = 0;

        // For each connected component, count mismatches
        foreach (var group in groups.Values) {
            Dictionary<int, int> freq = new Dictionary<int, int>();

            // Count frequency of source values in this component
            foreach (int idx in group) {
                if (!freq.ContainsKey(source[idx]))
                    freq[source[idx]] = 0;
                freq[source[idx]]++;
            }

            // Match target values; reduce frequency when matched
            foreach (int idx in group) {
                if (freq.ContainsKey(target[idx]) && freq[target[idx]] > 0) {
                    freq[target[idx]]--;
                } else {
                    hamming++;
                }
            }
        }

        return hamming;
    }

    // ---- Union-Find Implementation ----
    public class UnionFind {
        int[] parent;
        int[] rank;

        public UnionFind(int n) {
            parent = new int[n];
            rank = new int[n];
            for (int i = 0; i < n; i++)
                parent[i] = i;
        }

        public int Find(int x) {
            if (parent[x] != x)
                parent[x] = Find(parent[x]);
            return parent[x];
        }

        public void Union(int a, int b) {
            int pa = Find(a);
            int pb = Find(b);
            if (pa == pb) return;

            if (rank[pa] < rank[pb]) {
                parent[pa] = pb;
            } else if (rank[pa] > rank[pb]) {
                parent[pb] = pa;
            } else {
                parent[pb] = pa;
                rank[pa]++;
            }
        }
    }
}
