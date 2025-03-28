using System;
using System.Collections.Generic;

public class Solution {
    public int[] MaxPoints(int[][] grid, int[] queries) {
        int m = grid.Length, n = grid[0].Length;
        int total = m * n;
        // Build an array of cells: (value, row, col)
        var cells = new List<(int val, int r, int c)>();
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                cells.Add((grid[i][j], i, j));
            }
        }
        cells.Sort((a, b) => a.val.CompareTo(b.val));
        
        // Pair each query with its index then sort by query value.
        int k = queries.Length;
        var queryIdx = new (int q, int idx)[k];
        for (int i = 0; i < k; i++) {
            queryIdx[i] = (queries[i], i);
        }
        Array.Sort(queryIdx, (a, b) => a.q.CompareTo(b.q));
        
        // DSU (Union-Find) structure.
        var dsu = new DSU(total);
        // Active marker for each cell.
        bool[,] active = new bool[m, n];
        
        int[] ans = new int[k];
        int cellIndex = 0;
        // Directions: up, down, left, right
        int[] dr = new int[] { -1, 1, 0, 0 };
        int[] dc = new int[] { 0, 0, -1, 1 };
        
        // Process queries in sorted order.
        foreach (var (q, idx) in queryIdx) {
            // Activate all cells with value < current query q.
            while (cellIndex < cells.Count && cells[cellIndex].val < q) {
                var (val, r, c) = cells[cellIndex];
                active[r, c] = true;
                int id = r * n + c;
                // Check neighbors
                for (int d = 0; d < 4; d++) {
                    int nr = r + dr[d], nc = c + dc[d];
                    if (nr >= 0 && nr < m && nc >= 0 && nc < n && active[nr, nc]) {
                        int nid = nr * n + nc;
                        dsu.Union(id, nid);
                    }
                }
                cellIndex++;
            }
            // If the top left cell is active, we can get its component size.
            if (active[0, 0]) {
                int root = dsu.Find(0);
                ans[idx] = dsu.Size[root];
            } else {
                ans[idx] = 0;
            }
        }
        
        return ans;
    }
}

public class DSU {
    public int[] Parent;
    public int[] Size;
    
    public DSU(int n) {
        Parent = new int[n];
        Size = new int[n];
        for (int i = 0; i < n; i++) {
            Parent[i] = i;
            Size[i] = 1;
        }
    }
    
    public int Find(int x) {
        if (Parent[x] != x)
            Parent[x] = Find(Parent[x]);
        return Parent[x];
    }
    
    public void Union(int x, int y) {
        int rx = Find(x), ry = Find(y);
        if (rx == ry) return;
        // Union by size.
        if (Size[rx] < Size[ry]) {
            Parent[rx] = ry;
            Size[ry] += Size[rx];
        } else {
            Parent[ry] = rx;
            Size[rx] += Size[ry];
        }
    }
}
