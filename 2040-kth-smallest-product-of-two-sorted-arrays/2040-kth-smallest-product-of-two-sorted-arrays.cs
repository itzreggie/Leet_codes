


public class Solution
{
    public long KthSmallestProduct(int[] nums1, int[] nums2, long k)
    {
        long left = -10000000000, right = 10000000000;

        while (left < right)
        {
            long mid = left + (right - left) / 2;
            if (CountLessOrEqual(nums1, nums2, mid) >= k)
                right = mid;
            else
                left = mid + 1;
        }

        return left;
    }

    private long CountLessOrEqual(int[] nums1, int[] nums2, long target)
    {
        long count = 0;
        foreach (int a in nums1)
        {
            if (a > 0)
            {
                int l = 0, r = nums2.Length;
                while (l < r)
                {
                    int m = (l + r) / 2;
                    if ((long)a * nums2[m] <= target)
                        l = m + 1;
                    else
                        r = m;
                }
                count += l;
            }
            else if (a < 0)
            {
                int l = 0, r = nums2.Length;
                while (l < r)
                {
                    int m = (l + r) / 2;
                    if ((long)a * nums2[m] <= target)
                        r = m;
                    else
                        l = m + 1;
                }
                count += nums2.Length - l;
            }
            else if (target >= 0)
            {
                count += nums2.Length;
            }
        }
        return count;
    }
}
