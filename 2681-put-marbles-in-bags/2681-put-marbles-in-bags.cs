public class Solution {
    public long PutMarbles(int[] weights, int k) {
        int n = weights.Length;
        // With no cut, the cost is fixed (weights[0] + weights[n-1]), and any scoring differences cancel it.
        // So the answer is solely determined by which (k - 1) bonus cuts are chosen.
        // The bonus for making a cut between index i and i+1 is weights[i] + weights[i+1].

        // If there's no cut to make, return 0.
        if(k == 1) return 0L;
        
        int m = n - 1;  // number of possible cut positions
        long[] bonuses = new long[m];
        for (int i = 0; i < m; i++) {
            bonuses[i] = (long)weights[i] + weights[i + 1];
        }
        
        Array.Sort(bonuses); // Sort in ascending order.
        
        int cuts = k - 1;
        long minBonus = 0, maxBonus = 0;
        // Sum the smallest (k-1) bonuses for the minimal total cost.
        for (int i = 0; i < cuts; i++) {
            minBonus += bonuses[i];
        }
        // Sum the largest (k-1) bonuses for the maximal total cost.
        // Note: bonuses has m = n - 1 elements; the largest `cuts` elements are at indices m - cuts to m - 1.
        for (int i = 0; i < cuts; i++) {
            maxBonus += bonuses[m - cuts + i];
        }
        
        return maxBonus - minBonus;
    }
}
