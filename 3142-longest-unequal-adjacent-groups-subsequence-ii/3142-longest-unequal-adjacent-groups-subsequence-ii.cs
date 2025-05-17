
public class Solution {
    public IList<string> GetWordsInLongestSubsequence(string[] words, int[] groups) {
        int n = words.Length;
        // dp[i] is the length of the longest valid subsequence ending at index i
        int[] dp = new int[n];
        // parent[i] is the previous index in the subsequence used to form dp[i]
        int[] parent = new int[n];
        
        for (int i = 0; i < n; i++) {
            dp[i] = 1;    // each index alone is a valid subsequence of length 1
            parent[i] = -1;
        }
        
        // Try every possible ordered pair (j, i) with j < i
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < i; j++) {
                // Must have alternating groups.
                if (groups[j] == groups[i]) continue;
                // Their corresponding words must have equal lengths.
                if (words[j].Length != words[i].Length) continue;
                // And their Hamming distance should be exactly 1.
                if (HammingDistance(words[j], words[i]) != 1) continue;
                
                // If we can extend a valid subsequence ending at j by using i
                if (dp[j] + 1 > dp[i]) {
                    dp[i] = dp[j] + 1;
                    parent[i] = j;
                }
            }
        }
        
        // Find the index with the maximum dp value.
        int maxIdx = 0, maxLen = dp[0];
        for (int i = 1; i < n; i++) {
            if (dp[i] > maxLen) {
                maxLen = dp[i];
                maxIdx = i;
            }
        }
        
        // Reconstruct the subsequence of indices in reverse order.
        List<int> indices = new List<int>();
        int idx = maxIdx;
        while (idx != -1) {
            indices.Add(idx);
            idx = parent[idx];
        }
        // Reverse to get the correct order.
        indices.Reverse();
        
        // Build the result list of words.
        List<string> result = new List<string>();
        foreach (int i in indices) {
            result.Add(words[i]);
        }
        return result;
    }
    
    // Helper method to compute the Hamming distance between two strings of equal length.
    private int HammingDistance(string a, string b) {
        int count = 0;
        for (int i = 0; i < a.Length; i++) {
            if (a[i] != b[i]) count++;
        }
        return count;
    }
}
