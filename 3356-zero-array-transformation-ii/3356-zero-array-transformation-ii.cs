public class Solution {
    public int MinZeroArray(int[] nums, int[][] queries) {
        int n = nums.Length;
        // If nums is already a zero array, answer is 0.
        bool allZero = true;
        for (int i = 0; i < n; i++) {
            if (nums[i] != 0) { allZero = false; break; }
        }
        if (allZero) return 0;
        
        int m = queries.Length;
        // Use binary search over k (number of queries used)
        int lo = 0, hi = m + 1;
        while (lo < hi) {
            int mid = lo + (hi - lo) / 2;
            if (IsFeasible(nums, queries, mid))
                hi = mid;
            else
                lo = mid + 1;
        }
        return lo <= m ? lo : -1;
    }
    
    // Check if using the first k queries we can reduce nums to zero.
    private bool IsFeasible(int[] nums, int[][] queries, int k) {
        int n = nums.Length;
        int[] diff = new int[n + 1];
        // Build the difference array from the first k queries.
        for (int i = 0; i < k; i++) {
            int l = queries[i][0], r = queries[i][1], val = queries[i][2];
            diff[l] += val;
            if (r + 1 < n) diff[r + 1] -= val;
        }
        // Compute the available decrement for each index.
        int runningSum = 0;
        for (int j = 0; j < n; j++) {
            runningSum += diff[j];
            if (runningSum < nums[j]) return false;
        }
        return true;
    }
}
