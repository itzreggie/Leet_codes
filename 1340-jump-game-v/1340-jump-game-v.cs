public class Solution {
    public int MaxJumps(int[] arr, int d) {
        int n = arr.Length;
        int[] dp = new int[n];
        Array.Fill(dp, -1);

        int result = 1;
        for (int i = 0; i < n; i++) {
            result = Math.Max(result, DFS(i, arr, d, dp));
        }

        return result;
    }

    private int DFS(int i, int[] arr, int d, int[] dp) {
        if (dp[i] != -1) return dp[i];

        int n = arr.Length;
        int best = 1;

        // Jump right
        for (int x = 1; x <= d && i + x < n; x++) {
            if (arr[i + x] >= arr[i]) break; // must be strictly lower
            best = Math.Max(best, 1 + DFS(i + x, arr, d, dp));
        }

        // Jump left
        for (int x = 1; x <= d && i - x >= 0; x++) {
            if (arr[i - x] >= arr[i]) break; // must be strictly lower
            best = Math.Max(best, 1 + DFS(i - x, arr, d, dp));
        }

        dp[i] = best;
        return best;
    }
}

