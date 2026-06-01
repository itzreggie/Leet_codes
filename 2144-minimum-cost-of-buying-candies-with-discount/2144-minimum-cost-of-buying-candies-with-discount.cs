public class Solution {
    public int MinimumCost(int[] cost) {
        Array.Sort(cost);
        Array.Reverse(cost);

        int total = 0;

        for (int i = 0; i < cost.Length; i++) {
            // Every third candy is free
            if ((i + 1) % 3 != 0) {
                total += cost[i];
            }
        }

        return total;
    }
}
