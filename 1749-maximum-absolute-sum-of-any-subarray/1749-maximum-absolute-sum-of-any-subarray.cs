public class Solution {
    public int MaxAbsoluteSum(int[] nums) {
        int maxSum = 0, minSum = 0, currentMax = 0, currentMin = 0;
        
        foreach (int num in nums) {
            currentMax = Math.Max(currentMax + num, num);
            maxSum = Math.Max(maxSum, currentMax);
            
            currentMin = Math.Min(currentMin + num, num);
            minSum = Math.Min(minSum, currentMin);
        }
        
        return Math.Max(Math.Abs(maxSum), Math.Abs(minSum));
    }
}
