

using System;

public class Solution {
    private int[] parent = new int[26]; // Tracks connected components of equivalent chars

    public string SmallestEquivalentString(string s1, string s2, string baseStr) {
        // Initialize Union-Find for lowercase letters
        for (int i = 0; i < 26; i++) {
            parent[i] = i;
        }

        // Merge equivalent character sets using Union-Find
        for (int i = 0; i < s1.Length; i++) {
            Union(s1[i] - 'a', s2[i] - 'a');
        }

        // Convert baseStr using the smallest equivalent characters
        char[] result = baseStr.ToCharArray();
        for (int i = 0; i < result.Length; i++) {
            result[i] = (char)('a' + Find(result[i] - 'a'));
        }

        return new string(result);
    }

    private int Find(int x) {
        if (parent[x] != x) {
            parent[x] = Find(parent[x]); // Path compression
        }
        return parent[x];
    }

    private void Union(int x, int y) {
        int rootX = Find(x);
        int rootY = Find(y);
        
        if (rootX != rootY) {
            // Always attach the smaller lexicographical root to the larger one
            if (rootX < rootY) {
                parent[rootY] = rootX;
            } else {
                parent[rootX] = rootY;
            }
        }
    }
}
