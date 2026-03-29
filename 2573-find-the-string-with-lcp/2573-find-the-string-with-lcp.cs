public class Solution
{
    public string FindTheString(int[][] lcp)
    {
        int n = lcp.Length;
        if (n == 0) return "";

        // 1) Basic checks: symmetry, diagonal, and recurrence
        for (int i = 0; i < n; i++)
        {
            if (lcp[i][i] != n - i) return "";
            for (int j = 0; j < n; j++)
            {
                if (lcp[i][j] != lcp[j][i]) return "";
                if (lcp[i][j] < 0 || lcp[i][j] > n - Math.Max(i, j)) return "";
            }
        }

        // Recurrence: if lcp[i][j] > 0, then lcp[i+1][j+1] must be lcp[i][j] - 1 (when in bounds)
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                int v = lcp[i][j];
                if (v == 0) continue;
                if (i == n - 1 || j == n - 1)
                {
                    if (v != 1) return "";
                }
                else
                {
                    if (lcp[i + 1][j + 1] != v - 1) return "";
                }
            }
        }

        // 2) DSU for positions that must be equal
        var dsu = new DSU(n);
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (lcp[i][j] > 0)
                {
                    dsu.Union(i, j);
                }
            }
        }

        // 3) Assign characters to components, lexicographically smallest
        char[] res = new char[n];
        var compToChar = new Dictionary<int, char>();
        char nextChar = 'a';

        for (int i = 0; i < n; i++)
        {
            int root = dsu.Find(i);
            if (!compToChar.ContainsKey(root))
            {
                if (nextChar > 'z') return "";
                compToChar[root] = nextChar++;
            }
            res[i] = compToChar[root];
        }

        // 4) Ensure that lcp[i][j] == 0 implies different characters at i and j
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (lcp[i][j] == 0 && res[i] == res[j]) return "";
            }
        }

        // 5) Verify by recomputing lcp from the constructed string
        int[][] check = new int[n][];
        for (int i = 0; i < n; i++)
            check[i] = new int[n];

        for (int i = n - 1; i >= 0; i--)
        {
            for (int j = n - 1; j >= 0; j--)
            {
                if (res[i] == res[j])
                {
                    if (i + 1 < n && j + 1 < n)
                        check[i][j] = check[i + 1][j + 1] + 1;
                    else
                        check[i][j] = 1;
                }
                else
                {
                    check[i][j] = 0;
                }
            }
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (check[i][j] != lcp[i][j]) return "";
            }
        }

        return new string(res);
    }

    private class DSU
    {
        private int[] parent;
        private int[] rank;

        public DSU(int n)
        {
            parent = new int[n];
            rank = new int[n];
            for (int i = 0; i < n; i++)
                parent[i] = i;
        }

        public int Find(int x)
        {
            if (parent[x] != x)
                parent[x] = Find(parent[x]);
            return parent[x];
        }

        public void Union(int x, int y)
        {
            int rx = Find(x);
            int ry = Find(y);
            if (rx == ry) return;

            if (rank[rx] < rank[ry])
                parent[rx] = ry;
            else if (rank[rx] > rank[ry])
                parent[ry] = rx;
            else
            {
                parent[ry] = rx;
                rank[rx]++;
            }
        }
    }
}
