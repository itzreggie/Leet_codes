public class Solution {
    public long MinimumTotalDistance(IList<int> robot, IList<IList<int>> factory) {
        robot = robot.OrderBy(x => x).ToList();
        var fac = factory.OrderBy(x => x[0]).ToList();

        // Expand factories into a flat list of positions
        List<int> slots = new List<int>();
        foreach (var f in fac) {
            int pos = f[0];
            int limit = f[1];
            for (int i = 0; i < limit; i++) {
                slots.Add(pos);
            }
        }

        int n = robot.Count;
        int m = slots.Count;

        // dp[i][j] = min cost to repair first i robots using first j slots
        long[,] dp = new long[n + 1, m + 1];

        // Initialize dp with large values
        for (int i = 0; i <= n; i++)
            for (int j = 0; j <= m; j++)
                dp[i, j] = long.MaxValue / 4;

        // Base case: 0 robots cost 0
        for (int j = 0; j <= m; j++)
            dp[0, j] = 0;

        // Fill DP
        for (int i = 1; i <= n; i++) {
            for (int j = 1; j <= m; j++) {

                // Option 1: skip this factory slot
                dp[i, j] = dp[i, j - 1];

                // Option 2: match robot i-1 to slot j-1
                long cost = Math.Abs(robot[i - 1] - slots[j - 1]);
                dp[i, j] = Math.Min(dp[i, j], dp[i - 1, j - 1] + cost);
            }
        }

        return dp[n, m];
    }
}
