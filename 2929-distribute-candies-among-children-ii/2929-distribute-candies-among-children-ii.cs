using System;

public class Solution {
    // Change the return type to long
    public long DistributeCandies(int n, int limit) {
        // If n is greater than the maximum possible candies that can be distributed, return 0.
        if (n > 3L * limit) return 0;

        long ans = 0;
        // Iterate m from 0 to 3 (3 children) for inclusion--exclusion.
        for (int m = 0; m <= 3; m++) {
            long reduction = m * ((long)limit + 1);
            // If n - reduction is negative, break out.
            if (n < reduction) break;
            
            // Calculate the number of solutions for: x1 + x2 + x3 = (n - reduction)
            // That is C((n - reduction) + 3 - 1, 3 - 1) = C(n - reduction + 2, 2)
            long ways = Combination(n - reduction + 2, 2);
            
            // Number of ways to choose m children out of 3
            long comb = Combination(3, m);
            
            if (m % 2 == 1)
                ans -= comb * ways;
            else
                ans += comb * ways;
        }
        return ans;
    }
    
    // This helper computes C(n, k) for k up to 2.
    private long Combination(long n, int k) {
        if(n < k) return 0;
        if(k == 0) return 1;
        if(k == 1) return n;
        if(k == 2) return n * (n - 1) / 2;
        
        long res = 1;
        for (int i = 1; i <= k; i++) {
            res = res * (n - i + 1) / i;
        }
        return res;
    }
    
   
}
