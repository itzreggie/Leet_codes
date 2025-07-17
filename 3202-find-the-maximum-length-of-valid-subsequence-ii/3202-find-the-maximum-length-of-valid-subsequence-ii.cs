public class Solution {
    public int MaximumLength(int[] nums, int k) {
        int n = nums.Length;
        int maxLen = 0;

        // dp[i]: Dictionary mapping modulo value to max subsequence length ending at i
        var dp = new Dictionary<int, int>[n];

        for (int i = 0; i < n; i++) {
            dp[i] = new Dictionary<int, int>();

            for (int j = 0; j < i; j++) {
                int mod = (nums[j] + nums[i]) % k;

                if (dp[j].ContainsKey(mod)) {
                    dp[i][mod] = Math.Max(dp[i].GetValueOrDefault(mod, 0), dp[j][mod] + 1);
                } else {
                    dp[i][mod] = Math.Max(dp[i].GetValueOrDefault(mod, 0), 2); // starting new pair
                }

                maxLen = Math.Max(maxLen, dp[i][mod]);
            }

            // Initialize single element as base
            foreach (var mod in dp[i].Keys) {
                maxLen = Math.Max(maxLen, dp[i][mod]);
            }
        }

        return Math.Max(maxLen, 1); // minimum subsequence has length 1
    }
}
