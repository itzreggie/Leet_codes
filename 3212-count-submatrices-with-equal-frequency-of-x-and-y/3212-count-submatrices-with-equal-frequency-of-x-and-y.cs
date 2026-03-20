public class Solution {
    public int NumberOfSubmatrices(char[][] grid) {
        int m = grid.Length;
        int n = grid[0].Length;

        int[,] prefixX = new int[m + 1, n + 1];
        int[,] prefixY = new int[m + 1, n + 1];

        // Build prefix sums
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                prefixX[i + 1, j + 1] = prefixX[i + 1, j] + prefixX[i, j + 1] - prefixX[i, j];
                prefixY[i + 1, j + 1] = prefixY[i + 1, j] + prefixY[i, j + 1] - prefixY[i, j];

                if (grid[i][j] == 'X') prefixX[i + 1, j + 1]++;
                if (grid[i][j] == 'Y') prefixY[i + 1, j + 1]++;
            }
        }

        int count = 0;

        // Only submatrices (0,0) → (r,c)
        for (int r = 0; r < m; r++) {
            for (int c = 0; c < n; c++) {

                int xCount = prefixX[r + 1, c + 1];
                int yCount = prefixY[r + 1, c + 1];

                if (xCount > 0 && xCount == yCount)
                    count++;
            }
        }

        return count;
    }
}
