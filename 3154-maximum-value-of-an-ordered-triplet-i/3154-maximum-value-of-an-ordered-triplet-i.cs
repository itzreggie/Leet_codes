public class Solution {
    public long MaximumTripletValue(int[] nums) {
        int n = nums.Length;
        long maxValue = 0;

        for (int j = 1; j < n - 1; j++) {
            long maxLeft = long.MinValue; // Use long to store the maximum nums[i] - nums[j] for i < j
            for (int i = 0; i < j; i++) {
                maxLeft = Math.Max(maxLeft, (long)nums[i] - nums[j]);
            }
            
            // Calculate max triplet value for nums[k] with k > j
            for (int k = j + 1; k < n; k++) {
                long tripletValue = maxLeft * nums[k];
                maxValue = Math.Max(maxValue, tripletValue);
            }
        }

        return maxValue;
    }
}
