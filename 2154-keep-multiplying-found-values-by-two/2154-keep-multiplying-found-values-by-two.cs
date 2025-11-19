public class Solution {
    public int FindFinalValue(int[] nums, int original) {
        HashSet<int> numSet = new HashSet<int>(nums);

        while (numSet.Contains(original)) {
            original *= 2;
        }

        return original;
    }
}
