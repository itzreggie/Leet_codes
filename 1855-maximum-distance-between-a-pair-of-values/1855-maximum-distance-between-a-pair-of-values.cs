public class Solution {
    public int MaxDistance(int[] nums1, int[] nums2) {
        int i = 0, j = 0;
        int n = nums1.Length, m = nums2.Length;
        int maxDist = 0;

        while (i < n && j < m) {
            if (nums1[i] <= nums2[j]) {
                // valid pair
                maxDist = Math.Max(maxDist, j - i);
                j++; // try to increase distance
            } else {
                i++; // nums1[i] too large, move i forward
            }
        }

        return maxDist;
    }
}
