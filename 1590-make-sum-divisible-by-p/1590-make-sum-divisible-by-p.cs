public class Solution {
    public int MinSubarray(int[] nums, int p) {
        long total = 0;
        foreach (int x in nums) total += x;

        int need = (int)(total % p);
        if (need == 0) return 0;   // already divisible

        Dictionary<int, int> map = new();
        map[0] = -1;  // prefix sum before array starts

        long prefix = 0;
        int res = nums.Length;

        for (int i = 0; i < nums.Length; i++) {
            prefix = (prefix + nums[i]) % p;
            int target = (int)((prefix - need + p) % p);

            if (map.ContainsKey(target)) {
                res = Math.Min(res, i - map[target]);
            }

            map[(int)prefix] = i;
        }

        return res == nums.Length ? -1 : res;
    }
}
