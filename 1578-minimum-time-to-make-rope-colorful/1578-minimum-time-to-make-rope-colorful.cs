public class Solution {
    public int MinCost(string colors, int[] neededTime) {
        int totalTime = 0;

        for (int i = 1; i < colors.Length; i++) {
            if (colors[i] == colors[i - 1]) {
                // Add the smaller time to total
                totalTime += Math.Min(neededTime[i], neededTime[i - 1]);
                // Keep the larger time for future comparison
                neededTime[i] = Math.Max(neededTime[i], neededTime[i - 1]);
            }
        }

        return totalTime;
    }
}
