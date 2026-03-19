public class Solution {
    public int[] GetBiggestThree(int[][] grid) {
        int m = grid.Length;
        int n = grid[0].Length;

        HashSet<int> sums = new HashSet<int>();

        for (int r = 0; r < m; r++) {
            for (int c = 0; c < n; c++) {

                // radius 0 rhombus (just the cell)
                sums.Add(grid[r][c]);

                int k = 1;
                while (true) {
                    // Check bounds for radius k
                    if (r - k < 0 || r + k >= m || c - k < 0 || c + k >= n)
                        break;

                    int total = 0;

                    // Top → Right
                    for (int i = 0; i < k; i++)
                        total += grid[r - k + i][c + i];

                    // Right → Bottom
                    for (int i = 0; i < k; i++)
                        total += grid[r + i][c + k - i];

                    // Bottom → Left
                    for (int i = 0; i < k; i++)
                        total += grid[r + k - i][c - i];

                    // Left → Top
                    for (int i = 0; i < k; i++)
                        total += grid[r - i][c - k + i];

                    sums.Add(total);
                    k++;
                }
            }
        }

        return sums.OrderByDescending(x => x).Take(3).ToArray();
    }
}
