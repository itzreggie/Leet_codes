public class Solution {
    public bool HasAlternatingBits(int n) {
        int prev = n & 1;   // get the last bit
        n >>= 1;            // shift right

        while (n > 0) {
            int curr = n & 1;   // current bit
            if (curr == prev)   // if two adjacent bits are the same → false
                return false;

            prev = curr;        // update previous bit
            n >>= 1;            // move to next bit
        }

        return true;
    }
}
