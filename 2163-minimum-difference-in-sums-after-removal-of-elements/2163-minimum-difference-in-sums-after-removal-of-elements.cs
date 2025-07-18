using System;
using System.Collections.Generic;

public class Solution {
    public long MinimumDifference(int[] nums) {
        int n = nums.Length / 3;
        int len = nums.Length;

        long[] leftSum = new long[len];
        long[] rightSum = new long[len];

        // Max heap for left n smallest elements
        var maxHeap = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));
        long sum = 0;

        // Left to right: Keep n smallest elements for left part
        for (int i = 0; i < len; i++) {
            sum += nums[i];
            maxHeap.Enqueue(nums[i], nums[i]);
            if (maxHeap.Count > n) {
                sum -= maxHeap.Dequeue();
            }
            leftSum[i] = sum;
        }

        // Min heap for right n largest elements
        var minHeap = new PriorityQueue<int, int>();
        sum = 0;

        // Right to left: Keep n largest elements for right part
        for (int i = len - 1; i >= 0; i--) {
            sum += nums[i];
            minHeap.Enqueue(nums[i], nums[i]);
            if (minHeap.Count > n) {
                sum -= minHeap.Dequeue();
            }
            rightSum[i] = sum;
        }

        long result = long.MaxValue;

        // Compare left and right partitions
        for (int i = n - 1; i < 2 * n; i++) {
            long diff = leftSum[i] - rightSum[i + 1];
            result = Math.Min(result, diff);
        }

        return result;
    }
}
