using System;
using System.Collections.Generic;

public class Solution {
    public int LargestIsland(int[][] grid) {
        int n = grid.Length;
        int[] dirs = { 0, 1, 0, -1, 0 };
        int islandId = 2;
        Dictionary<int, int> islandSizes = new Dictionary<int, int>();
        int maxIslandSize = 0;

        // Assign unique IDs to islands and calculate their sizes
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                if (grid[i][j] == 1) {
                    int size = Dfs(grid, i, j, islandId, dirs);
                    islandSizes[islandId] = size;
                    maxIslandSize = Math.Max(maxIslandSize, size);
                    islandId++;
                }
            }
        }

        // Calculate the maximum island size after flipping a 0 to 1
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                if (grid[i][j] == 0) {
                    HashSet<int> neighborIds = new HashSet<int>();
                    for (int d = 0; d < 4; d++) {
                        int x = i + dirs[d];
                        int y = j + dirs[d + 1];
                        if (x >= 0 && x < n && y >= 0 && y < n && grid[x][y] > 1) {
                            neighborIds.Add(grid[x][y]);
                        }
                    }
                    int newSize = 1;
                    foreach (int id in neighborIds) {
                        newSize += islandSizes[id];
                    }
                    maxIslandSize = Math.Max(maxIslandSize, newSize);
                }
            }
        }

        return maxIslandSize;
    }

    private int Dfs(int[][] grid, int i, int j, int islandId, int[] dirs) {
        int n = grid.Length;
        if (i < 0 || i >= n || j < 0 || j >= n || grid[i][j] != 1) {
            return 0;
        }
        grid[i][j] = islandId;
        int size = 1;
        for (int d = 0; d < 4; d++) {
            size += Dfs(grid, i + dirs[d], j + dirs[d + 1], islandId, dirs);
        }
        return size;
    }
}