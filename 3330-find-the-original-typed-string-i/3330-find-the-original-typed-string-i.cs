

using System;

public class Solution {
    public int PossibleStringCount(string word) {
        int n = word.Length;
        if (n == 0) return 0;

        int result = 1;

        // Count how many "reducible" stretches there are
        for (int i = 0; i < n;) {
            int j = i + 1;
            while (j < n && word[j] == word[i]) j++;
            int length = j - i;

            if (length >= 2) {
                // Alice may have repeated this character by accident,
                // so we can remove one instance at most once in the whole word
                result += (length - 1);
            }

            i = j;
        }

        return result;
    }
}
