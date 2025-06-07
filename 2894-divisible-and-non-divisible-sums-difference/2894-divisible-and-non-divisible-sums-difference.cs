public class Solution {
    public int DifferenceOfSums(int n, int m) {
        // Compute total sum from 1 to n.
        long total = (long)n * (n + 1) / 2;
        
        // Calculate number of multiples of m in [1, n]
        long k = n / m;
        
        // Sum of multiples of m in [1, n]: m, 2*m, ..., k*m.
        long sumDivisible = m * k * (k + 1) / 2;
        
        // Result: sum of numbers not divisible by m minus sum of those divisible by m.
        long result = total - 2 * sumDivisible;
        
        // Explicitly cast to int as required.
        return (int)result;
    }
}
