public class Solution {
    public bool IsPowerOfFour(int n) {
        if (n <= 0) return false;

        // Check if n is a power of 2 AND only one bit is set at an even position
        return (n & (n - 1)) == 0 && (n & 0x55555555) != 0;
    }
}
