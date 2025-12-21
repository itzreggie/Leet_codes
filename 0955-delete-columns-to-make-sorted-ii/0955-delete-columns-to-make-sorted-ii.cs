public class Solution {
    public int MinDeletionSize(string[] strs) {
        int n = strs.Length;
        int m = strs[0].Length;
        bool[] sorted = new bool[n - 1]; // track which adjacent pairs are already sorted
        int deletions = 0;

        for (int col = 0; col < m; col++) {
            bool needDelete = false;

            // Check if this column causes disorder
            for (int row = 0; row < n - 1; row++) {
                if (!sorted[row] && strs[row][col] > strs[row + 1][col]) {
                    needDelete = true;
                    break;
                }
            }

            if (needDelete) {
                // Delete this column
                deletions++;
            } else {
                // Mark pairs that are now sorted thanks to this column
                for (int row = 0; row < n - 1; row++) {
                    if (!sorted[row] && strs[row][col] < strs[row + 1][col]) {
                        sorted[row] = true;
                    }
                }
            }
        }

        return deletions;
    }
}
