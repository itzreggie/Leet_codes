using System;
using System.Collections.Generic;
using System.Linq;

public class Solution
{
    public int[][] DivideArray(int[] nums, int k)
    {
        Array.Sort(nums);
        var result = new List<IList<int>>();
        int n = nums.Length;

        for (int i = 0; i < n; i += 3)
        {
            int min = nums[i];
            int max = nums[i + 2];

            if (max - min > k)
            {
                return new int[0][];
            }

            result.Add(new List<int> { nums[i], nums[i + 1], nums[i + 2] });
        }

        return result.Select(group => group.ToArray()).ToArray();
    }
}
