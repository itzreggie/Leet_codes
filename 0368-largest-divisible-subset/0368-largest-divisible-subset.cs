public class Solution {
    public IList<int> LargestDivisibleSubset(int[] nums) {
    Array.Sort(nums); // Sort the input array
    int n = nums.Length;
    int[] dp = new int[n]; // dp[i] stores the size of the largest subset ending with nums[i]
    int[] prev = new int[n]; // prev[i] stores the index of the previous element in the subset
    int maxIndex = 0; // Index of the largest element in the largest subset

    for (int i = 0; i < n; i++) {
        dp[i] = 1; // Each number alone is a valid subset
        prev[i] = -1; // Default value for no previous element
        for (int j = 0; j < i; j++) {
            if (nums[i] % nums[j] == 0 && dp[j] + 1 > dp[i]) {
                dp[i] = dp[j] + 1;
                prev[i] = j; // Link to the previous element
            }
        }
        if (dp[i] > dp[maxIndex]) {
            maxIndex = i;
        }
    }

    // Construct the result subset
    IList<int> result = new List<int>();
    while (maxIndex != -1) {
        result.Add(nums[maxIndex]);
        maxIndex = prev[maxIndex];
    }

    return result.Reverse().ToList(); // Return the subset in ascending order
}

}