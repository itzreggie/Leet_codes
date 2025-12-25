


using System;

public class Solution {
    public long MaximumHappinessSum(int[] happiness, int k) {
        if (happiness == null || happiness.Length == 0 || k <= 0) return 0;
        Array.Sort(happiness);           // ascending
        Array.Reverse(happiness);        // now descending

        int n = happiness.Length;
        k = Math.Min(k, n);
        long sum = 0;
        for (int i = 0; i < k; i++) {
            int val = happiness[i] - i;          // decremented i times
            if (val > 0) sum += val;
        }
        return sum;
    }
}
