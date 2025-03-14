public class Solution {
    public int MaximumCandies(int[] candies, long k) {
        int maxCandy = 0;
        foreach (int candy in candies) {
            maxCandy = Math.Max(maxCandy, candy);
        }
        
        int left = 1;
        int right = maxCandy;
        int ans = 0;
        
        while (left <= right) {
            int mid = left + (right - left) / 2;
            long count = 0;
            foreach (int candy in candies) {
                count += candy / mid;
                if (count >= k) break;
            }
            if (count >= k) {
                ans = mid;
                left = mid + 1;
            } else {
                right = mid - 1;
            }
        }
        
        return ans;
    }
}
