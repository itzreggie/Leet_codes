public class Solution {
    public int CountPermutations(int[] complexity) {
        const int MOD = 1000000007;
        long result = 1;

        for (int i = 1; i < complexity.Length; i++) {
            // If any element is less than or equal to the root complexity,
            // then no valid permutation exists
            if (complexity[i] <= complexity[0]) {
                return 0;
            }

            // Multiply result by current index and apply modulo
            result = (result * i) % MOD;
        }

        return (int)result;
    }
}
