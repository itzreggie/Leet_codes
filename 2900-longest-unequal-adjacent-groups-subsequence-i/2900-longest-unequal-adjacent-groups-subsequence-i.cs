


using System;
using System.Collections.Generic;

public class Solution {
    public IList<string> GetLongestSubsequence(string[] words, int[] groups) {
        List<string> result = new List<string>();

        // Start with the first element
        result.Add(words[0]);
        int prevGroup = groups[0];

        // Iterate through the rest of the array
        for (int i = 1; i < words.Length; i++) {
            if (groups[i] != prevGroup) {
                result.Add(words[i]);
                prevGroup = groups[i];
            }
        }

        return result;
    }
}
