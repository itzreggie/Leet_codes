using System;
using System.Collections.Generic;
using System.Linq;

public class Solution {
    public int MaxFreeTime(int eventTime, int[] startTime, int[] endTime) {
        int n = startTime.Length;
        if (n == 0) return eventTime;

        // 1) Sort meetings by start time
        var meetings = new List<(int s, int e)>(n);
        for (int i = 0; i < n; i++)
            meetings.Add((startTime[i], endTime[i]));
        meetings.Sort((a, b) => a.s.CompareTo(b.s));

        // 2) Build the original gaps array of length n+1
        //    gap[0] = time before the first meeting
        //    gap[i] = idle between meetings i-1 and i
        //    gap[n] = time after the last meeting
        int[] gaps = new int[n + 1];
        gaps[0] = meetings[0].s;
        for (int i = 1; i < n; i++)
            gaps[i] = meetings[i].s - meetings[i - 1].e;
        gaps[n] = eventTime - meetings[n - 1].e;

        // 3) Build a multiset of those gaps: value -> count
        var ms = new SortedList<int, int>();
        Action<int, int> changeCount = (val, delta) => {
            if (delta > 0) {
                if (ms.ContainsKey(val)) ms[val] += delta;
                else ms[val] = delta;
            } else {
                // delta is negative
                int cnt = ms[val] + delta;
                if (cnt > 0) ms[val] = cnt;
                else ms.Remove(val);
            }
        };
        foreach (var g in gaps)
            changeCount(g, +1);

        // 4) Helper to get (largest, secondLargest) from ms
        (int largest, int second) GetTopTwo() {
            var keys = ms.Keys;
            int sz = keys.Count;
            int largest = keys[sz - 1];
            int cnt = ms[largest];
            if (cnt >= 2) {
                // the same gap value appears twice
                return (largest, largest);
            }
            // otherwise second‐largest is the previous key
            int second = (sz >= 2 ? keys[sz - 2] : 0);
            return (largest, second);
        }

        // 5) Initialize answer with the best SINGLE gap (no removal)
        var (initMax, initSecond) = GetTopTwo();
        int answer = initMax;

        // 6) Try removing each meeting m
        //    Which merges gaps at index m and m+1
        for (int m = 0; m < n; m++) {
            // compute duration of meeting m
            int dur = meetings[m].e - meetings[m].s;

            // remove the two adjacent gaps gaps[m], gaps[m+1]
            changeCount(gaps[m], -1);
            changeCount(gaps[m + 1], -1);

            // add the merged gap
            int merged = gaps[m] + gaps[m + 1] + dur;
            changeCount(merged, +1);

            // query top two
            var (g1, g2) = GetTopTwo();

            // after reinserting the meeting optimally, max free = max(g2, g1 - dur)
            int cand = Math.Max(g2, g1 - dur);
            if (cand > answer) answer = cand;

            // revert changes: remove merged, add back original two
            changeCount(merged, -1);
            changeCount(gaps[m], +1);
            changeCount(gaps[m + 1], +1);
        }

        return answer;
    }
}
