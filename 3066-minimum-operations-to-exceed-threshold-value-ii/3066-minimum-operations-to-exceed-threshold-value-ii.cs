using System;
using System.Collections.Generic;

public class Solution {
    public int MinOperations(int[] nums, int target) {
        int operations = 0;
        // PriorityQueue to serve as a min-heap
        var minHeap = new SortedSet<(long value, int index)>();
        int index = 0;

        // Add all elements to the min-heap
        foreach (int num in nums) {
            minHeap.Add((num, index++));
        }

        while (minHeap.Count > 1 && minHeap.Min.value < target) {
            // Extract the two smallest numbers
            var first = minHeap.Min.value;
            minHeap.Remove(minHeap.Min);
            var second = minHeap.Min.value;
            minHeap.Remove(minHeap.Min);

            // Combine the two numbers and add back to the heap
            long newVal = Math.Min(first, second) * 2L + Math.Max(first, second);
            minHeap.Add((newVal, index++));
            operations++;
        }

        // Check the condition for the remaining elements
        return minHeap.Min.value >= target ? operations : -1;
    }
}
