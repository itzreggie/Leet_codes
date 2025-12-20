public class Solution {
    public int MinDeletionSize(string[] strs) {
        int rows = strs.Length;
        int cols = strs[0].Length;
        int deleteCount = 0;

        // Check each column
        for (int c = 0; c < cols; c++) {
            for (int r = 1; r < rows; r++) {
                // If the column is not sorted lexicographically
                if (strs[r][c] < strs[r - 1][c]) {
                    deleteCount++;
                    break; // No need to check further in this column
                }
            }
        }

        return deleteCount;
    }
}
