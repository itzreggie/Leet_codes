public class Solution {
    public int LongestSubsequence(string s, int k) {
        int n = s.Length;
        // Count all zeros upfront—they never change the numeric value
        int zeroCount = 0;
        foreach (char c in s)
            if (c == '0')
                zeroCount++;

        // We'll greedily pick as many '1's (from least significant bit inward)
        // as we can without exceeding k
        long value = 0;
        long bitWeight = 1;   // represents 2^0, then 2^1, 2^2, ...
        int oneCount = 0;

        // Scan from the end (LSB) toward the front
        for (int i = n - 1; i >= 0; i--) {
            if (s[i] == '1') {
                // Can we add this '1' at the current bitWeight?
                if (bitWeight <= k && value + bitWeight <= k) {
                    value += bitWeight;
                    oneCount++;
                }
            }
            bitWeight <<= 1;            // next more significant bit
            if (bitWeight > k) break;   // beyond this, any '1' would overflow
        }

        // Total length = all zeros + the picked ones
        return zeroCount + oneCount;
    }
}
