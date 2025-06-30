public class Solution {
    public int FindLHS(int[] nums) {
        var freq = new Dictionary<int, int>();
        foreach (int num in nums) {
            if (!freq.ContainsKey(num))
                freq[num] = 0;
            freq[num]++;
        }

        int maxLen = 0;
        foreach (var kvp in freq) {
            int key = kvp.Key;
            if (freq.ContainsKey(key + 1)) {
                int len = freq[key] + freq[key + 1];
                maxLen = Math.Max(maxLen, len);
            }
        }

        return maxLen;
    }
}
