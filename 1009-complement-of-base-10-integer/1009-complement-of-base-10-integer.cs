public class Solution {
    public int BitwiseComplement(int n) {
        if (n == 0) return 1;

        int mask = 0;
        int temp = n;

        // Build a mask of all 1's the same length as n's binary form
        while (temp > 0) {
            mask = (mask << 1) | 1;
            temp >>= 1;
        }

        // XOR flips the bits
        return n ^ mask;
    }
}
