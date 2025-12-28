public class Solution {
    public int CountNegatives(int[][] grid) {
        int m = grid.Length;
        int n = grid[0].Length;

        int row = 0;
        int col = n - 1;
        int count = 0;

        while (row < m && col >= 0) {
            if (grid[row][col] < 0) {
                // All elements below this one in the same column are also negative
                count += (m - row);
                col--; // move left
            } else {
                row++; // move down
            }
        }

        return count;
    }
}
