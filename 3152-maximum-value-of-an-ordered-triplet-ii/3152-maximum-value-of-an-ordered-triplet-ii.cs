public class Solution {
    public long MaximumTripletValue(int[] nums) {
        int n = nums.Length;
        if(n < 3) return 0;
        
        // Build prefix maximum: prefixMax[j] is max{nums[0..j]}
        long[] prefixMax = new long[n];
        prefixMax[0] = nums[0];
        for (int i = 1; i < n; i++) {
            prefixMax[i] = Math.Max(prefixMax[i - 1], nums[i]);
        }
        
        // Build suffix maximum: suffixMax[j] is max{nums[j..n-1]}
        long[] suffixMax = new long[n];
        suffixMax[n - 1] = nums[n - 1];
        for (int i = n - 2; i >= 0; i--) {
            suffixMax[i] = Math.Max(suffixMax[i + 1], nums[i]);
        }
        
        long ans = 0;
        // Iterate over possible middle indices j
        for (int j = 1; j <= n - 2; j++) {
            long diff = prefixMax[j - 1] - nums[j];
            long candidate = diff * suffixMax[j + 1];
            ans = Math.Max(ans, candidate);
        }
        
        // Return 0 if the maximum value is negative
        return ans < 0 ? 0 : ans;
    }
}
