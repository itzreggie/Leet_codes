public class Solution {
    public int CountCompleteSubarrays(int[] nums) {
        int n = nums.Length;
        
        // Determine the number of distinct elements in the entire array.
        var distinctSet = new HashSet<int>(nums);
        int totalDistinct = distinctSet.Count;
        
        long result = 0;
        var freq = new Dictionary<int, int>();
        int left = 0;
        
        // Slide over the array with right pointer.
        for (int right = 0; right < n; right++) {
            int val = nums[right];
            if (!freq.ContainsKey(val))
                freq[val] = 0;
            freq[val]++;
            
            // Try to shrink the window if possible while keeping it "complete".
            while (freq.Count == totalDistinct && freq[nums[left]] > 1) {
                freq[nums[left]]--;
                left++;
            }
            
            // If the window [left, right] is complete (contains all distinct elements),
            // then every subarray ending at 'right' and starting at any index i ∈ [0, left]
            // is also complete. That adds (left + 1) complete subarrays.
            if (freq.Count == totalDistinct) {
                result += (left + 1);
            }
        }
        
        return (int)result;
    }
}
