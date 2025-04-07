public class Solution {
    public bool CanPartition(int[] nums) {
    int sum = nums.Sum(); // Get the sum of the array
    
    // If the total sum is odd, it cannot be partitioned into two equal subsets
    if (sum % 2 != 0) return false;
    
    int target = sum / 2;
    int n = nums.Length;
    
    // Dynamic programming array to check if subset sums are possible
    bool[] dp = new bool[target + 1];
    dp[0] = true; // Base case: sum 0 is always achievable with no elements
    
    foreach (int num in nums) {
        for (int j = target; j >= num; j--) {
            dp[j] = dp[j] || dp[j - num];
        }
    }
    
    return dp[target];
}

}

