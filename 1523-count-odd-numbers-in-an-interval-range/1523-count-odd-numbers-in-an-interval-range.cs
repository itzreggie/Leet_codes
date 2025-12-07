public class Solution {
    public int CountOdds(int low, int high) {
        // Total numbers in the range
        int total = high - low + 1;

        // If both low and high are even, odds = total / 2
        // Otherwise, odds = total / 2 + 1
        return (total / 2) + ((low % 2 == 1 || high % 2 == 1) ? 1 : 0);
    }
}
