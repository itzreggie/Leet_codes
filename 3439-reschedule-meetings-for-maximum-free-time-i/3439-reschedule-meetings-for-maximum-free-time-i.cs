using System;
using System.Linq;

public class Solution {
    private int n, k, T;
    private int[] start, end, dur, pref;

    public int MaxFreeTime(int eventTime, int k, int[] startTime, int[] endTime) {
        // 1) Initialize and sort events by startTime:
        n = startTime.Length;
        T = eventTime;
        this.k = k;
        start = new int[n];
        end   = new int[n];
        var idx = Enumerable.Range(0, n)
                            .OrderBy(i => startTime[i])
                            .ToArray();
        for (int i = 0; i < n; i++) {
            start[i] = startTime[idx[i]];
            end[i]   = endTime  [idx[i]];
        }

        // 2) Precompute durations and prefix sums
        dur  = new int[n];
        pref = new int[n + 1];
        for (int i = 0; i < n; i++) {
            dur[i] = end[i] - start[i];
            pref[i+1] = pref[i] + dur[i];
        }

        // 3) Binary search for the largest L
        int lo = 0, hi = T, ans = 0;
        while (lo <= hi) {
            int mid = lo + ((hi - lo) >> 1);
            if (CanAchieve(mid)) {
                ans = mid;
                lo = mid + 1;
            } else {
                hi = mid - 1;
            }
        }
        return ans;
    }

    // Check if we can get a free gap >= L with <= k shifts
    private bool CanAchieve(int L) {
        // Helper to test shifting up to k from the LEFT side of a gap
        bool TryLeft(int m) {
            // gap before first (m == -1)
            if (m == -1) {
                // shift first t events right: new gap = start[t] - pref[t]
                // want start[t] - pref[t] >= L  ==>  pref[t] <= start[t] - L
                for (int t = 0; t <= k && t <= n; t++) {
                    int rhs = (t < n ? start[t] : T) - L;
                    if (pref[t] <= rhs) 
                        return true;
                }
                return false;
            }
            // gap between m and m+1:
            // shift up to tL of [0..m] right: new end of block = baseEnd + sumDur
            // require start[m+1] - (baseEnd + sumDur) >= L
            //  => sumDur <= start[m+1] - baseEnd - L
            int maxL = Math.Min(k, m+1);
            for (int tL = 0; tL <= maxL; tL++) {
                int baseEnd = (m - tL >= 0 ? end[m - tL] : 0);
                int sumDur  = pref[m+1] - pref[m+1 - tL];
                if (sumDur <= start[m+1] - baseEnd - L)
                    return true;
            }
            return false;
        }

        // Helper to test shifting up to k from the RIGHT side of a gap
        bool TryRight(int m) {
            // gap after last (m == n-1)
            if (m == n-1) {
                int maxR = Math.Min(k, n);
                for (int t = 0; t <= maxR; t++) {
                    int baseEnd = (n-1-t >= 0 ? end[n-1-t] : 0);
                    int sumDur  = pref[n] - pref[n - t];
                    if (sumDur <= T - baseEnd - L)
                        return true;
                }
                return false;
            }
            // gap between m and m+1:
            // shift up to tR of [m+1..n-1] left: new start = bound - sumDur
            // require (bound - sumDur) - end[m] >= L
            //  => sumDur <= bound - end[m] - L
            int maxR = Math.Min(k, n - (m+1));
            for (int tR = 0; tR <= maxR; tR++) {
                int bound   = (m+1+tR < n ? start[m+1+tR] : T);
                int sumDur  = pref[m+1+tR] - pref[m+1];
                if (sumDur <= bound - end[m] - L)
                    return true;
            }
            return false;
        }

        // 4) Test the “before first” gap
        if (TryLeft(-1) || TryRight(n-1))
            return true;

        // 5) Test each interior gap
        for (int m = 0; m < n-1; m++) {
            if (TryLeft(m) || TryRight(m))
                return true;
        }

        return false;
    }
}
