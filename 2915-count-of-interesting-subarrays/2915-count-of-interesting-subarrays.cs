public class Solution {
    public long CountInterestingSubarrays(IList<int> nums, int modulo, int k) {
        // Dictionary to store frequencies of prefix sums modulo 'modulo'
        var freq = new Dictionary<int, long>();
        freq[0] = 1;  // One empty prefix (sum 0)

        int prefix = 0;
        long result = 0;
        
        foreach (int num in nums) {
            // Create an indicator: 1 if num % modulo equals k; otherwise 0.
            int indicator = (num % modulo == k) ? 1 : 0;
            prefix += indicator;
            
            // Current prefix sum modulo's value.
            int currMod = prefix % modulo;
            
            // We need to find previous prefixes such that:
            // (currMod - previous) mod modulo == k
            // Rearranging, previous value (modulo modulo) should equal (currMod - k + modulo) % modulo.
            int target = (currMod - k + modulo) % modulo;
            
            if (freq.ContainsKey(target)) {
                result += freq[target];
            }
            
            if (!freq.ContainsKey(currMod)) {
                freq[currMod] = 0;
            }
            freq[currMod]++;
        }
        
        return result;
    }
}
