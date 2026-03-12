public class Solution {
    public int MinOperations(string s) {
        int n = s.Length;
        int pattern0 = 0; // pattern starting with '0' → "010101..."
        int pattern1 = 0; // pattern starting with '1' → "101010..."

        for (int i = 0; i < n; i++) {
            char expected0 = (i % 2 == 0) ? '0' : '1';
            char expected1 = (i % 2 == 0) ? '1' : '0';

            if (s[i] != expected0) pattern0++;
            if (s[i] != expected1) pattern1++;
        }

        return Math.Min(pattern0, pattern1);
    }
}
