public class Solution {
    public int LongestBalanced(string s) {
        int n = s.Length;
        int result = 0;

        for (int start = 0; start < n; start++) {
            int[] freq = new int[26];

            for (int end = start; end < n; end++) {
                int idx = s[end] - 'a';
                freq[idx]++;

                if (IsBalanced(freq)) {
                    int len = end - start + 1;
                    if (len > result) result = len;
                }
            }
        }

        return result;
    }

    private bool IsBalanced(int[] freq) {
        int minFreq = int.MaxValue;
        int maxFreq = 0;

        for (int i = 0; i < 26; i++) {
            if (freq[i] > 0) {
                if (freq[i] < minFreq) minFreq = freq[i];
                if (freq[i] > maxFreq) maxFreq = freq[i];
            }
        }

        if (maxFreq == 0) return false; // no characters
        return minFreq == maxFreq;
    }
}
