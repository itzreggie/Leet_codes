public class Solution {
    public int FindLucky(int[] arr) {
        // 1) Count frequencies
        var freq = new Dictionary<int, int>();
        foreach (int x in arr) {
            if (!freq.ContainsKey(x))
                freq[x] = 0;
            freq[x]++;
        }

        // 2) Look for “lucky” numbers where freq[x] == x
        int ans = -1;
        foreach (var kvp in freq) {
            int num = kvp.Key, count = kvp.Value;
            if (num == count && num > ans) {
                ans = num;
            }
        }

        return ans;
    }
}
