public class Solution {
    public int MinFlips(string s) {
        int n = s.Length;
        string ss = s + s;

        // Build alternating patterns of length 2n
        char[] alt1 = new char[2 * n];
        char[] alt2 = new char[2 * n];

        for (int i = 0; i < 2 * n; i++) {
            alt1[i] = (i % 2 == 0) ? '0' : '1';
            alt2[i] = (i % 2 == 0) ? '1' : '0';
        }

        int res = int.MaxValue;
        int diff1 = 0, diff2 = 0;
        int left = 0;

        // Sliding window
        for (int right = 0; right < 2 * n; right++) {
            if (ss[right] != alt1[right]) diff1++;
            if (ss[right] != alt2[right]) diff2++;

            // Keep window size = n
            if (right - left + 1 > n) {
                if (ss[left] != alt1[left]) diff1--;
                if (ss[left] != alt2[left]) diff2--;
                left++;
            }

            // When window is exactly size n, update result
            if (right - left + 1 == n) {
                res = Math.Min(res, Math.Min(diff1, diff2));
            }
        }

        return res;
    }
}
