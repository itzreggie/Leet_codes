public class Solution {
    public int LenLongestFibSubseq(int[] arr) {
        int n = arr.Length;
        int maxLength = 0;
        var index = new Dictionary<int, int>();
        for (int i = 0; i < n; i++) {
            index[arr[i]] = i;
        }

        var dp = new Dictionary<(int, int), int>();
        
        for (int k = 0; k < n; k++) {
            for (int j = 0; j < k; j++) {
                int iVal = arr[k] - arr[j];
                if (iVal < arr[j] && index.ContainsKey(iVal)) {
                    int i = index[iVal];
                    dp[(j, k)] = dp.GetValueOrDefault((i, j), 2) + 1;
                    maxLength = Math.Max(maxLength, dp[(j, k)]);
                }
            }
        }
        
        return maxLength >= 3 ? maxLength : 0;
    }
}
