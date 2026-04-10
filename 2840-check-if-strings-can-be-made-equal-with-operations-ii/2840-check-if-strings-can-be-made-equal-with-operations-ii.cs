public class Solution {
    public bool CheckStrings(string s1, string s2) {
        int n = s1.Length;

        int[] even1 = new int[26];
        int[] odd1 = new int[26];
        int[] even2 = new int[26];
        int[] odd2 = new int[26];

        for (int i = 0; i < n; i++) {
            if (i % 2 == 0) {
                even1[s1[i] - 'a']++;
                even2[s2[i] - 'a']++;
            } else {
                odd1[s1[i] - 'a']++;
                odd2[s2[i] - 'a']++;
            }
        }

        for (int c = 0; c < 26; c++) {
            if (even1[c] != even2[c]) return false;
            if (odd1[c] != odd2[c]) return false;
        }

        return true;
    }
}
