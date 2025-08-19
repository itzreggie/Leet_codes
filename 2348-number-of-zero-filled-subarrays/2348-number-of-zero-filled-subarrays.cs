public class Solution {
    public long ZeroFilledSubarray(int[] nums) {
        long count = 0;
        int zeroStreak = 0;

        foreach (int num in nums) {
            if (num == 0) {
                zeroStreak++;
                count += zeroStreak;
            } else {
                zeroStreak = 0;
            }
        }

        return count;
    }
}
