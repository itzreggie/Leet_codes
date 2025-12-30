public class Solution {
    public int MinimumBoxes(int[] apple, int[] capacity) {
        int totalApples = apple.Sum();

        // Sort capacities descending so we use the largest boxes first
        Array.Sort(capacity);
        Array.Reverse(capacity);

        int used = 0;
        int currentCapacity = 0;

        foreach (int cap in capacity) {
            currentCapacity += cap;
            used++;

            if (currentCapacity >= totalApples)
                return used;
        }

        return used; // In case all boxes are needed
    }
}
