public class Solution {
    public int NumberOfSpecialChars(string word) {
        int[] firstUpper = new int[26];
        int[] lastLower = new int[26];

        Array.Fill(firstUpper, int.MaxValue);
        Array.Fill(lastLower, -1);

        // Record positions
        for (int i = 0; i < word.Length; i++) {
            char c = word[i];
            if (char.IsLower(c)) {
                lastLower[c - 'a'] = i;
            } else {
                firstUpper[c - 'A'] = Math.Min(firstUpper[c - 'A'], i);
            }
        }

        int count = 0;

        // Check special condition
        for (int i = 0; i < 26; i++) {
            if (lastLower[i] != -1 && firstUpper[i] != int.MaxValue) {
                if (lastLower[i] < firstUpper[i]) {
                    count++;
                }
            }
        }

        return count;
    }
}
