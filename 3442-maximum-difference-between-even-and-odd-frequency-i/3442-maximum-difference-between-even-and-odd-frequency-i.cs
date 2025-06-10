

using System;

public class Solution {
    public int MaxDifference(string s) {
        // Array to hold the frequency of each lowercase letter.
        int[] freq = new int[26];
        foreach (char c in s) {
            freq[c - 'a']++;
        }
        
        // Initialize:
        // maxOdd: maximum frequency among letters with an odd count.
        // minEven: minimum frequency among letters with an even count.
        int maxOdd = -1;
        int minEven = int.MaxValue;
        
        for (int i = 0; i < 26; i++) {
            if (freq[i] > 0) {
                if (freq[i] % 2 == 1) { // odd frequency
                    maxOdd = Math.Max(maxOdd, freq[i]);
                } else {              // even frequency
                    minEven = Math.Min(minEven, freq[i]);
                }
            }
        }
        
        // If we did not find any odd frequency letter or even frequency letter,
        // a valid pair does not exist, so return -1.
        if (maxOdd == -1 || minEven == int.MaxValue) {
            return -1;
        }
        
        return maxOdd - minEven;
    }
}
