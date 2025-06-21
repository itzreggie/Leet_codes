

using System;

public class Solution {
    public int MinimumDeletions(string word, int k) {
        // Count the frequency of each lowercase letter.
        int[] freq = new int[26];
        foreach (char c in word) {
            // Assuming input is lowercase. If necessary, convert by: char lower = char.ToLower(c)
            freq[c - 'a']++;
        }
        
        int totalLength = word.Length;
        int maxFreq = 0;
        // Determine the maximum frequency among all letters.
        for (int i = 0; i < 26; i++) {
            maxFreq = Math.Max(maxFreq, freq[i]);
        }
        
        int minDeletion = int.MaxValue;
        // Try every candidate lower bound L from 0 to maxFreq.
        // L represents the minimum frequency threshold that every kept letter must meet.
        for (int L = 0; L <= maxFreq; L++) {
            int deletions = 0;
            for (int i = 0; i < 26; i++) {
                int f = freq[i];
                if (f < L) {
                    // If a letter's frequency is less than L, we cannot raise it—
                    // so we must delete all occurrences of that letter.
                    deletions += f;
                } else {
                    // Otherwise, we can keep at most L+k occurrences.
                    // Any excess occurrences beyond that must be deleted.
                    deletions += f - Math.Min(f, L + k);
                }
            }
            minDeletion = Math.Min(minDeletion, deletions);
        }
        
        return minDeletion;
    }
    
   
}
