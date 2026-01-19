public class Solution {
    public int MaxSideLength(int[][] mat, int threshold) {
        int m = mat.Length;
        int n = mat[0].Length;

        // Build prefix sum matrix
        int[,] prefix = new int[m + 1, n + 1];

        for (int i = 1; i <= m; i++) {
            for (int j = 1; j <= n; j++) {
                prefix[i, j] = mat[i - 1][j - 1]
                               + prefix[i - 1, j]
                               + prefix[i, j - 1]
                               - prefix[i - 1, j - 1];
            }
        }

        // Helper to compute sum of any square using prefix sums
        bool CanFindSquare(int size) {
            for (int i = size; i <= m; i++) {
                for (int j = size; j <= n; j++) {
                    int sum = prefix[i, j]
                              - prefix[i - size, j]
                              - prefix[i, j - size]
                              + prefix[i - size, j - size];

                    if (sum <= threshold)
                        return true;
                }
            }
            return false;
        }

        // Binary search for max side length
        int left = 0, right = Math.Min(m, n), ans = 0;

        while (left <= right) {
            int mid = (left + right) / 2;

            if (CanFindSquare(mid)) {
                ans = mid;
                left = mid + 1;
            } else {
                right = mid - 1;
            }
        }

        return ans;
    }
}
