public class Solution {
    public int MaximumLength(int[] nums) {
        int countEven = 0, countOdd = 0;

        // 1) Count evens/odds for same‐parity sums
        foreach (int num in nums) {
            if ((num & 1) == 0) countEven++;
            else               countOdd++;
        }
        int maxSameParity = Math.Max(countEven, countOdd);

        // 2) Compute longest alternating‐parity subsequence
        int altEven = 0, altOdd = 0;
        foreach (int num in nums) {
            if ((num & 1) == 0)      // current is even
                altEven = altOdd + 1;
            else                     // current is odd
                altOdd  = altEven + 1;
        }
        int maxAlt = Math.Max(altEven, altOdd);

        // 3) Best of both worlds
        return Math.Max(maxSameParity, maxAlt);
    }
}
