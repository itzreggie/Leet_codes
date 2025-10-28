
public class Solution {
    public int CountValidSelections(int[] nums) {
        int n = nums.Length;
        int validCount = 0;

        for (int i = 0; i < n; i++) {
            if (nums[i] == 0) {
                if (IsValid(nums, i, 1)) validCount++; // right
                if (IsValid(nums, i, -1)) validCount++; // left
            }
        }

        return validCount;
    }

    private bool IsValid(int[] original, int start, int direction) {
        int[] nums = (int[])original.Clone();
        int curr = start;

        while (curr >= 0 && curr < nums.Length) {
            if (nums[curr] == 0) {
                curr += direction;
            } else {
                nums[curr]--;
                direction *= -1;
                curr += direction;
            }
        }

        foreach (int num in nums) {
            if (num != 0) return false;
        }

        return true;
    }
}
