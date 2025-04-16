public class Solution {
    public long CountGood(int[] nums, int k) {
    long count = 0; // Use long to collect large counts
    int left = 0;
    long pairs = 0;
    var frequency = new Dictionary<int, int>();

    for (int right = 0; right < nums.Length; right++) {
        if (!frequency.ContainsKey(nums[right])) {
            frequency[nums[right]] = 0;
        }
        // Add the number of existing occurrences to the pair count.
        pairs += frequency[nums[right]];
        frequency[nums[right]]++;

        // Shrink the window while we have at least k pairs
        while (pairs >= k) {
            // All subarrays from left to end with current right are good.
            count += nums.Length - right;
            // Remove the element at left from the window.
            pairs -= frequency[nums[left]] - 1;
            frequency[nums[left]]--;
            left++;
        }
    }
    return count;
}



}