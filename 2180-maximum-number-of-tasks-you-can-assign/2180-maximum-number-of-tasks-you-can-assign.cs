public class Solution {
    public int MaxTaskAssign(int[] tasks, int[] workers, int pills, int strength) {
        Array.Sort(tasks);
        Array.Sort(workers);
        
        int lo = 0, hi = Math.Min(tasks.Length, workers.Length);
        while (lo < hi) {
            int mid = (lo + hi + 1) / 2;
            if (CanComplete(tasks, workers, pills, strength, mid))
                lo = mid;
            else
                hi = mid - 1;
        }
        return lo;
    }
    
    // Check if it is possible to complete 'x' tasks
    private bool CanComplete(int[] tasks, int[] workers, int pills, int strength, int x) {
        int n = workers.Length;
        // We'll use the x easiest tasks (tasks[0..x-1], since tasks is sorted in ascending order)
        // and use the strongest x workers (workers[n-x .. n-1]).
        // Build a multiset (worker strength -> frequency) for these workers.
        var available = new SortedList<int, int>();
        for (int i = n - x; i < n; i++) {
            int w = workers[i];
            if (available.ContainsKey(w))
                available[w]++;
            else
                available[w] = 1;
        }
        
        int pillLeft = pills;
        // Process tasks in descending order (i.e. from hardest among our x candidate tasks to easiest)
        for (int i = x - 1; i >= 0; i--) {
            int req = tasks[i];
            // First try to assign a worker who can complete this task without a pill.
            if (available.Count > 0) {
                // The worker with the highest strength is at the last index.
                int maxWorker = available.Keys[available.Count - 1];
                if (maxWorker >= req) {
                    RemoveFromMultiset(available, maxWorker);
                    continue;
                }
            }
            // Otherwise, if we have a pill available, we can boost a worker.
            if (pillLeft > 0) {
                // A worker boosted by a pill has strength (worker + strength) so we need:
                // worker + strength >= req  --> worker >= req - strength.
                int needed = req - strength;
                if (needed < 0) needed = 0;
                // In the multiset, find the smallest worker with strength >= needed.
                int idx = LowerBound(available, needed);
                if (idx == -1) return false;
                int candidate = available.Keys[idx];
                // For safety, (candidate + strength) should be >= req.
                if (candidate + strength < req) return false;
                RemoveFromMultiset(available, candidate);
                pillLeft--;
            } else {
                return false;
            }
        }
        return true;
    }
    
    // Removes one occurrence of 'key' from the multiset.
    private void RemoveFromMultiset(SortedList<int, int> multiset, int key) {
        if (multiset[key] == 1)
            multiset.Remove(key);
        else
            multiset[key]--;
    }
    
    // Returns the index of the smallest key in 'sl' which is >= target.
    // If no such key exists, returns -1.
    private int LowerBound(SortedList<int, int> sl, int target) {
        int lo = 0, hi = sl.Count - 1;
        int ans = -1;
        while (lo <= hi) {
            int mid = lo + (hi - lo) / 2;
            if (sl.Keys[mid] >= target) {
                ans = mid;
                hi = mid - 1;
            } else {
                lo = mid + 1;
            }
        }
        return ans;
    }
}
