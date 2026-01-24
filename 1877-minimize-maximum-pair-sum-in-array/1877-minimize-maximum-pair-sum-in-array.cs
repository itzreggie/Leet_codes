public class Solution {
    public int MinPairSum(int[] nums) {
        Array.Sort(nums);
        int left = 0;
        int right = nums.Length - 1;
        int maxPairSum = 0;

        while (left < right) {
            int pairSum = nums[left] + nums[right];
            if (pairSum > maxPairSum)
                maxPairSum = pairSum;

            left++;
            right--;
        }

        return maxPairSum;
    }
}
