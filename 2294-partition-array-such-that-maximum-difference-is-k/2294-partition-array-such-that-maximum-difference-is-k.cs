


using System;
using System.Collections.Generic;

public class Solution
{
    public int PartitionArray(int[] nums, int k)
    {
        Array.Sort(nums);
        int count = 0;
        int start = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            // Start a new group if the current number exceeds the allowed range from the start
            if (nums[i] - nums[start] > k)
            {
                count++;
                start = i;
            }
        }

        // Account for the last group
        return count + 1;
    }
}
