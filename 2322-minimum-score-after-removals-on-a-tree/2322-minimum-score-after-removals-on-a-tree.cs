public class Solution {
    int[] xor; // stores XOR of subtree rooted at each node
    Dictionary<int, List<int>> tree = new();
    int totalXor = 0;
    int minScore = int.MaxValue;

    public int MinimumScore(int[] nums, int[][] edges) {
        int n = nums.Length;

        // Build tree
        foreach (var edge in edges) {
            tree.TryAdd(edge[0], new List<int>());
            tree.TryAdd(edge[1], new List<int>());
            tree[edge[0]].Add(edge[1]);
            tree[edge[1]].Add(edge[0]);
        }

        xor = new int[n];
        var parent = new int[n];
        Array.Fill(parent, -1);

        // First DFS to get XOR value of each subtree
        DFS(0, -1, nums, parent);

        // Check all pairs of nodes for potential second-edge cut
        for (int i = 1; i < n; ++i) {
            for (int j = i + 1; j < n; ++j) {
                int[] scores = Evaluate(i, j, parent);
                Array.Sort(scores);
                minScore = Math.Min(minScore, scores[2] - scores[0]);
            }
        }

        return minScore;
    }

    private int DFS(int node, int par, int[] nums, int[] parent) {
        xor[node] = nums[node];
        parent[node] = par;

        foreach (int child in tree[node]) {
            if (child == par) continue;
            xor[node] ^= DFS(child, node, nums, parent);
        }

        return xor[node];
    }

    // Evaluates score after cutting edges leading to nodes i and j
    private int[] Evaluate(int u, int v, int[] parent) {
        if (IsAncestor(u, v, parent)) {
            return new int[] {
                xor[v],
                xor[u] ^ xor[v],
                xor[0] ^ xor[u]
            };
        } else if (IsAncestor(v, u, parent)) {
            return new int[] {
                xor[u],
                xor[v] ^ xor[u],
                xor[0] ^ xor[v]
            };
        } else {
            return new int[] {
                xor[u],
                xor[v],
                xor[0] ^ xor[u] ^ xor[v]
            };
        }
    }

    private bool IsAncestor(int anc, int desc, int[] parent) {
        while (desc != -1) {
            if (desc == anc) return true;
            desc = parent[desc];
        }
        return false;
    }
}
