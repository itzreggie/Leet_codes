public class Solution {
    public string FindDifferentBinaryString(string[] nums) {
        HashSet<string> numSet = new HashSet<string>(nums);
        int n = nums.Length;
        for (int i = 0; i < (1 << n); i++) {
            string candidate = Convert.ToString(i, 2).PadLeft(n, '0');
            if (!numSet.Contains(candidate)) {
                return candidate;
            }
        }
        return "";
    }
}
