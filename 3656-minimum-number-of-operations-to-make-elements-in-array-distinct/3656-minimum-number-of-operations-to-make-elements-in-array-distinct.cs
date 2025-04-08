public class Solution {
    public int MinimumOperations(int[] nums) {
        // Check if the array is already distinct
        if (nums.Distinct().Count() == nums.Length) {
            return 0;
        }

        // Initialize the operation counter
        int operations = 0;

        // Use a HashSet to track distinct elements during processing
        HashSet<int> seen = new HashSet<int>();
        Queue<int> queue = new Queue<int>(nums);

        while (queue.Count > 0) {
            // Simulate the removal of up to 3 elements
            for (int i = 0; i < 3 && queue.Count > 0; i++) {
                int current = queue.Dequeue();
                seen.Add(current);
            }
            operations++;

            // Check if the remaining elements in the queue are distinct
            if (queue.Distinct().Count() == queue.Count) {
                break;
            }
        }

        return operations;
    }
}
