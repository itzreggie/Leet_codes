using System;
using System.Collections.Generic;
using System.Linq;

public class Solution {
    public int MaxEvents(int[][] events) {
        // Sort events by start day
        Array.Sort(events, (a, b) => a[0] != b[0] ? a[0] - b[0] : a[1] - b[1]);
        
        // Min‐heap of (endDay, uniqueId)
        var pq = new SortedSet<(int end, int id)>(
            Comparer<(int end, int id)>.Create((x, y) =>
                x.end != y.end ? x.end - y.end : x.id - y.id
            )
        );
        
        int i = 0, n = events.Length;
        int day = 0, attended = 0;
        
        // Process until all events are consumed or heap is empty
        while (i < n || pq.Count > 0) {
            // If no ongoing event, jump day to next event start
            if (pq.Count == 0) {
                day = events[i][0];
            }
            
            // Push all events that start on or before 'day'
            while (i < n && events[i][0] <= day) {
                pq.Add((events[i][1], i));
                i++;
            }
            
            // Remove events that have already ended
            while (pq.Count > 0 && pq.Min.end < day) {
                pq.Remove(pq.Min);
            }
            
            // Attend the event that ends earliest
            if (pq.Count > 0) {
                pq.Remove(pq.Min);
                attended++;
                day++;
            }
        }
        
        return attended;
    }
}
