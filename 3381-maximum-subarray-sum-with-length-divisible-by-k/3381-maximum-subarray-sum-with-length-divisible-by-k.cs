public class Solution {
    public long MaxSubarraySum(int[] nums, int k) {
        int n = nums.Length;
        long[] prefix = new long[n + 1];
        for (int i = 0; i < n; i++) {
            prefix[i + 1] = prefix[i] + nums[i];
        }

        long[] minPrefix = new long[k];
        for (int i = 0; i < k; i++) minPrefix[i] = long.MaxValue;
        minPrefix[0] = 0; // base case

        long result = long.MinValue;

        for (int i = 1; i <= n; i++) {
            int mod = i % k;
            if (minPrefix[mod] != long.MaxValue) {
                result = Math.Max(result, prefix[i] - minPrefix[mod]);
            }
            minPrefix[mod] = Math.Min(minPrefix[mod], prefix[i]);
        }

        return result;
    }
}
