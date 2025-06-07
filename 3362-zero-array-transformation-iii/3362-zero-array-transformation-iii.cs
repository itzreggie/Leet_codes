
using System;
using System.Collections.Generic;

public class Solution {
    public int MaxRemoval(int[] nums, int[][] queries) {
        int n = nums.Length;
        int q = queries.Length;
        
        // ---- STEP 1: Global Feasibility Check ----
        // Compute, for each index, the total potential coverage if you use all queries.
        int[] avail = new int[n+1];
        foreach (var qr in queries) {
            int l = qr[0], r = qr[1];
            avail[l] += 1;
            if (r + 1 < n) {
                avail[r + 1] -= 1;
            }
        }
        int current = 0;
        for (int i = 0; i < n; i++) {
            current += avail[i];
            if (current < nums[i]) 
                return -1; // Even all queries together are not enough.
        }
        
        // ---- STEP 2: Greedy selection of intervals (queries) to cover demands ----
        // Represent each query as an interval.
        var intervals = new Query[q];
        for (int i = 0; i < q; i++) {
            intervals[i] = new Query(queries[i][0], queries[i][1]);
        }
        // Sort intervals by left endpoint ascending.
        Array.Sort(intervals, (a, b) => a.L.CompareTo(b.L));
        
        // We'll use a PriorityQueue to choose among intervals with L <= current index.
        // We want the one with maximum R, so we push with priority = -R.
        PriorityQueue<Query, int> pq = new PriorityQueue<Query, int>();
        int pointer = 0;  // to iterate over sorted intervals
        
        // diff array for applying the updates (each chosen query gives +1 from index i to its R).
        int[] diff = new int[n+1];  // all initially 0
        int usedCount = 0;  // count how many queries we choose
        current = 0;  // current coverage (prefix sum of diff)
        
        // Process positions 0..n-1.
        for (int i = 0; i < n; i++) {
            // Add updates from previously chosen queries.
            if (i > 0) diff[i] += diff[i - 1];
            current = diff[i];
            
            // While current coverage is less than required at i, choose extra queries.
            while (current < nums[i]) {
                // Add all intervals that become available at index i.
                while (pointer < q && intervals[pointer].L <= i) {
                    // Enqueue with priority -R so that the one with largest R comes first.
                    pq.Enqueue(intervals[pointer], -intervals[pointer].R);
                    pointer++;
                }
                // If no available interval can help, it is impossible.
                if (pq.Count == 0) 
                    return -1;
                
                // Choose the interval that extends farthest.
                Query chosen = pq.Dequeue();
                // This chosen query will add +1 to every index in [i, chosen.R].
                diff[i] += 1;
                if (chosen.R + 1 < diff.Length)
                    diff[chosen.R + 1] -= 1;
                usedCount++;
                current++;  // since index i gets immediate +1 from this update.
            }
        }
        
        // ---- STEP 3: Answer computation ----
        // We needed usedCount queries to cover the entire array;
        // hence, we can remove (total queries - usedCount).
        return q - usedCount;
    }
    
    // Simple class to represent an interval (query).
    public class Query {
        public int L;
        public int R;
        public Query(int l, int r) {
            L = l;
            R = r;
        }
    }
}
