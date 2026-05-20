public class Solution {
    public int[] FindThePrefixCommonArray(int[] A, int[] B) {
        int n = A.Length;
        int[] freq = new int[n + 1];
        int[] result = new int[n];
        int common = 0;

        for (int i = 0; i < n; i++) {
            // Add A[i]
            freq[A[i]]++;
            if (freq[A[i]] == 2) common++;

            // Add B[i]
            freq[B[i]]++;
            if (freq[B[i]] == 2) common++;

            result[i] = common;
        }

        return result;
    }
}
