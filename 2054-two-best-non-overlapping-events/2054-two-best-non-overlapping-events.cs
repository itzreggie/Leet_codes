public class Solution {
    public int MaxTwoEvents(int[][] events) {
        // Sort events by start time
        Array.Sort(events, (a, b) => a[0].CompareTo(b[0]));

        // PriorityQueue: (endTime, value)
        var pq = new PriorityQueue<(int end, int val), int>();
        int maxVal = 0;
        int result = 0;

        foreach (var e in events) {
            int start = e[0], end = e[1], val = e[2];

            // Pop all events that end before this start
            while (pq.Count > 0 && pq.Peek().end < start) {
                var prev = pq.Dequeue();
                maxVal = Math.Max(maxVal, prev.val);
            }

            // Option 1: take this event alone
            result = Math.Max(result, val);

            // Option 2: combine with best previous non-overlapping
            result = Math.Max(result, val + maxVal);

            // Push current event into PQ
            pq.Enqueue((end, val), end);
        }

        return result;
    }
}
