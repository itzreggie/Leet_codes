public class Solution {
    public int MaxArea(int[] height) {
        int i = 0, j = height.Length - 1;
        int maxArea = 0;
        
        while (i < j) {
            int h = Math.Min(height[i], height[j]);
            int currentArea = (j - i) * h;
            maxArea = Math.Max(maxArea, currentArea);
            
            // Move the pointer pointing to the shorter line inward.
            if (height[i] < height[j]) {
                i++;
            } else {
                j--;
            }
        }
        
        return maxArea;
    }
}
