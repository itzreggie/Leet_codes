public class Solution {
    public int LongestSubarray(int[] nums) {
        // 1. 找到最大值 k
        int k = int.MinValue;
        foreach (var x in nums) {
            if (x > k) k = x;
        }
        
        // 2. 扫描数组，统计等于 k 的最长连续段
        int maxLen = 0, currLen = 0;
        foreach (var x in nums) {
            if (x == k) {
                currLen++;
                if (currLen > maxLen) {
                    maxLen = currLen;
                }
            } else {
                currLen = 0;
            }
        }
        
        return maxLen;
    }
}

