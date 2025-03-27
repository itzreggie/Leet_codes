
public class Solution {
    public int MinimumIndex(IList<int> nums) {
        int n = nums.Count;

        // Step 1: Find the dominant element
        int dominant = nums[0];
        int count = 0;

        foreach (int num in nums) {
            if (num == dominant) {
                count++;
            } else if (--count == 0) {
                dominant = num;
                count = 1;
            }
        }

        // Step 2: Verify dominant element
        count = 0;
        foreach (int num in nums) {
            if (num == dominant) count++;
        }
        if (count * 2 <= n) return -1; // No dominant element

        // Step 3: Check for valid split
        int leftCount = 0;
        for (int i = 0; i < n - 1; i++) {
            if (nums[i] == dominant) leftCount++;

            int rightCount = count - leftCount;
            if (leftCount * 2 > i + 1 && rightCount * 2 > n - i - 1) {
                return i;
            }
        }

        return -1;
    }
}
