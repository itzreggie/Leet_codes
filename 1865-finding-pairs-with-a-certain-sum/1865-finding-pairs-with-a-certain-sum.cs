using System;
using System.Collections.Generic;

public class FindSumPairs {
    private int[] nums1;
    private int[] nums2;
    private Dictionary<int, int> freqNums2;

    public FindSumPairs(int[] nums1, int[] nums2) {
        this.nums1 = nums1;
        this.nums2 = nums2;
        freqNums2 = new Dictionary<int, int>();

        foreach (int num in nums2) {
            if (!freqNums2.ContainsKey(num))
                freqNums2[num] = 0;
            freqNums2[num]++;
        }
    }

    public void Add(int index, int val) {
        int oldVal = nums2[index];
        int newVal = oldVal + val;

        // Update nums2
        nums2[index] = newVal;

        // Update frequency map
        freqNums2[oldVal]--;
        if (freqNums2[oldVal] == 0) {
            freqNums2.Remove(oldVal);
        }

        if (!freqNums2.ContainsKey(newVal)) {
            freqNums2[newVal] = 0;
        }
        freqNums2[newVal]++;
    }

    public int Count(int tot) {
        int count = 0;
        foreach (int num in nums1) {
            int complement = tot - num;
            if (freqNums2.ContainsKey(complement)) {
                count += freqNums2[complement];
            }
        }
        return count;
    }
}
