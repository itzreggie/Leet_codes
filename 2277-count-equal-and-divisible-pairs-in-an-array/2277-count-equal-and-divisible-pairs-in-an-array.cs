public class Solution {
    public int CountPairs(int[] nums, int k) {
        // map value → (gcd(i,k) → count of indices i seen so far with that gcd)
        var map = new Dictionary<int, Dictionary<int, int>>();
        int result = 0;

        for (int j = 0; j < nums.Length; j++) {
            int v = nums[j];
            if (!map.TryGetValue(v, out var gcdCounts)) {
                gcdCounts = new Dictionary<int, int>();
                map[v] = gcdCounts;
            }

            // for each previous index i with the same value, check if (i*j) % k == 0
            foreach (var pair in gcdCounts) {
                int g = pair.Key;
                int cnt = pair.Value;
                // we know gcd(i,k)=g, so i is a multiple of g and k/g is integer:
                // (i*j) % k == 0  ⇔  j % (k/g) == 0
                if (j % (k / g) == 0) {
                    result += cnt;
                }
            }

            // record this index j by its gcd(j, k)
            int gj = Gcd(j, k);
            if (!gcdCounts.ContainsKey(gj))
                gcdCounts[gj] = 0;
            gcdCounts[gj]++;
        }

        return result;
    }

    private int Gcd(int a, int b) {
        return b == 0 ? a : Gcd(b, a % b);
    }
}
