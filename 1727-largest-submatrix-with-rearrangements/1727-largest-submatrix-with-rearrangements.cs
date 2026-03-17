public class Solution {
    public int LargestSubmatrix(int[][] matrix) {
        int m = matrix.Length;
        int n = matrix[0].Length;

        // Step 1: Build heights array (like histogram heights)
        int[][] heights = new int[m][];
        heights[0] = matrix[0];

        for (int i = 1; i < m; i++) {
            heights[i] = new int[n];
            for (int j = 0; j < n; j++) {
                if (matrix[i][j] == 1)
                    heights[i][j] = heights[i - 1][j] + 1;
                else
                    heights[i][j] = 0;
            }
        }

        int maxArea = 0;

        // Step 2: For each row, sort heights descending (simulate column rearrangement)
        for (int i = 0; i < m; i++) {
            Array.Sort(heights[i]);
            Array.Reverse(heights[i]); // largest heights first

            // Step 3: Compute max area using sorted heights
            for (int j = 0; j < n; j++) {
                int height = heights[i][j];
                int width = j + 1; // because sorted descending
                maxArea = Math.Max(maxArea, height * width);
            }
        }

        return maxArea;
    }
}
