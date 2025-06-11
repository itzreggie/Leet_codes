using System;
using System.Collections.Generic;

public class Solution {
    public int MaxDifference(string s, int k) {
        int ans = int.MinValue;

        foreach (var pair in GetPermutations()) {
            char a = pair.Item1;
            char b = pair.Item2;

            // minDiff[parityA][parityB] = min(a - b) seen so far for that parity
            int[,] minDiff = new int[2, 2] { 
                { int.MaxValue / 2, int.MaxValue / 2 }, 
                { int.MaxValue / 2, int.MaxValue / 2 } 
            };

            List<int> prefixA = new List<int> { 0 };
            List<int> prefixB = new List<int> { 0 };

            int l = 0;

            for (int r = 0; r < s.Length; r++) {
                prefixA.Add(prefixA[^1] + (s[r] == a ? 1 : 0));
                prefixB.Add(prefixB[^1] + (s[r] == b ? 1 : 0));

                while (r - l + 1 >= k &&
                       prefixA[l] < prefixA[^1] &&
                       prefixB[l] < prefixB[^1]) {

                    int pa = prefixA[l] % 2;
                    int pb = prefixB[l] % 2;
                    minDiff[pa, pb] = Math.Min(minDiff[pa, pb], prefixA[l] - prefixB[l]);
                    l++;
                }

                int parityA = prefixA[^1] % 2;
                int parityB = prefixB[^1] % 2;
                int requiredA = 1 - parityA;

                int candidate = prefixA[^1] - prefixB[^1] - minDiff[requiredA, parityB];
                ans = Math.Max(ans, candidate);
            }
        }

        return ans;
    }

    private List<Tuple<char, char>> GetPermutations() {
        var result = new List<Tuple<char, char>>();
        string chars = "01234";
        foreach (char a in chars) {
            foreach (char b in chars) {
                if (a != b)
                    result.Add(Tuple.Create(a, b));
            }
        }
        return result;
    }
}
