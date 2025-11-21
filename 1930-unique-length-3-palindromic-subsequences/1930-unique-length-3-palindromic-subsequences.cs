

public class Solution {
    public int CountPalindromicSubsequence(string s) {
        int n = s.Length;
        var result = new HashSet<string>();

        // For each character, try to form palindromes of length 3
        for (char c = 'a'; c <= 'z'; c++) {
            int first = s.IndexOf(c);
            int last = s.LastIndexOf(c);

            // We need at least two occurrences of c to form c_x_c
            if (first != -1 && last != -1 && first < last) {
                // Check all characters between first and last
                for (int i = first + 1; i < last; i++) {
                    string palindrome = $"{c}{s[i]}{c}";
                    result.Add(palindrome);
                }
            }
        }

        return result.Count;
    }
}
