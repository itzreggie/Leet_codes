using System;
using System.Collections.Generic;
using System.Linq;

public class Solution {
    public long MinCost(int[] basket1, int[] basket2) {
        var count = new Dictionary<int, int>();
        int n = basket1.Length;
        int minValue = int.MaxValue;

        // Count frequency differences
        for (int i = 0; i < n; i++) {
            count[basket1[i]] = count.GetValueOrDefault(basket1[i], 0) + 1;
            count[basket2[i]] = count.GetValueOrDefault(basket2[i], 0) - 1;
            minValue = Math.Min(minValue, Math.Min(basket1[i], basket2[i]));
        }

        var excess = new List<int>();

        foreach (var kvp in count) {
            if (kvp.Value % 2 != 0) {
                // If odd difference, it's impossible
                return -1;
            }
            for (int i = 0; i < Math.Abs(kvp.Value) / 2; i++) {
                excess.Add(kvp.Key);
            }
        }

        excess.Sort();

        long cost = 0;
        for (int i = 0; i < excess.Count / 2; i++) {
            cost += Math.Min(excess[i], 2 * minValue);
        }

        return cost;
    }
}
