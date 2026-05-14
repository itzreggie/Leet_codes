public class Solution {
    public bool IsGood(int[] nums) {
        int n = nums.Max();

        // Length must be exactly n + 1
        if (nums.Length != n + 1)
            return false;

        int[] freq = new int[n + 1];

        foreach (int x in nums) {
            if (x < 1 || x > n) return false;
            freq[x]++;
        }

        // Check 1..n-1 appear exactly once
        for (int i = 1; i < n; i++) {
            if (freq[i] != 1)
                return false;
        }

        // Check n appears exactly twice
        return freq[n] == 2;
    }
}
