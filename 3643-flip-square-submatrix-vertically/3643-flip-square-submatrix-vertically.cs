public class Solution {
    public int[][] ReverseSubmatrix(int[][] grid, int x, int y, int k) 
    {
        // Reverse the rows inside the k x k submatrix
        int top = x;
        int bottom = x + k - 1;

        while (top < bottom)
        {
            // Swap entire rows inside the submatrix boundaries
            for (int col = 0; col < k; col++)
            {
                int temp = grid[top][y + col];
                grid[top][y + col] = grid[bottom][y + col];
                grid[bottom][y + col] = temp;
            }

            top++;
            bottom--;
        }

        return grid;
    }
}
