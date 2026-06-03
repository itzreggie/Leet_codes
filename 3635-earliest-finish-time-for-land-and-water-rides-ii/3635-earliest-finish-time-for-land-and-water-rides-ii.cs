using System;

public class Solution {
    public int EarliestFinishTime(int[] landStartTime, int[] landDuration,
                                  int[] waterStartTime, int[] waterDuration) 
    {
        long ans = long.MaxValue;

        // Land -> Water
        ans = Math.Min(ans, ComputeOrder(landStartTime, landDuration,
                                         waterStartTime, waterDuration));

        // Water -> Land
        ans = Math.Min(ans, ComputeOrder(waterStartTime, waterDuration,
                                         landStartTime, landDuration));

        return (int)ans;
    }

    // first: rides done first (start1, dur1)
    // second: rides done second (start2, dur2)
    private long ComputeOrder(int[] start1, int[] dur1,
                              int[] start2, int[] dur2)
    {
        int n1 = start1.Length;
        int n2 = start2.Length;

        // Pack second rides into array of (start, dur)
        var second = new (int s, int d)[n2];
        for (int i = 0; i < n2; i++) {
            second[i] = (start2[i], dur2[i]);
        }

        // Sort second rides by start time
        Array.Sort(second, (a, b) => a.s.CompareTo(b.s));

        // Build prefix min of duration (for rides with start < t)
        long[] prefixMinDur = new long[n2];
        long curMin = long.MaxValue;
        for (int i = 0; i < n2; i++) {
            curMin = Math.Min(curMin, (long)second[i].d);
            prefixMinDur[i] = curMin;
        }

        // Build suffix min of (start + duration) (for rides with start >= t)
        long[] suffixMinFinish = new long[n2];
        curMin = long.MaxValue;
        for (int i = n2 - 1; i >= 0; i--) {
            long finish = (long)second[i].s + second[i].d;
            curMin = Math.Min(curMin, finish);
            suffixMinFinish[i] = curMin;
        }

        long best = long.MaxValue;

        for (int i = 0; i < n1; i++) {
            long firstEnd = (long)start1[i] + dur1[i];

            // Binary search: first index in second with start >= firstEnd
            int idx = LowerBoundSecond(second, (int)firstEnd);

            long candidate = long.MaxValue;

            // Case 1: use a ride with start >= firstEnd
            if (idx < n2) {
                candidate = Math.Min(candidate, suffixMinFinish[idx]);
            }

            // Case 2: use a ride with start < firstEnd
            if (idx > 0) {
                long minDur = prefixMinDur[idx - 1];
                candidate = Math.Min(candidate, firstEnd + minDur);
            }

            best = Math.Min(best, candidate);
        }

        return best;
    }

    // Lower bound: first index with second[i].s >= value
    private int LowerBoundSecond((int s, int d)[] arr, int value) {
        int lo = 0, hi = arr.Length;
        while (lo < hi) {
            int mid = lo + (hi - lo) / 2;
            if (arr[mid].s >= value) hi = mid;
            else lo = mid + 1;
        }
        return lo;
    }
}
