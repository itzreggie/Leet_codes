public class Solution {
    public IList<IList<int>> PermuteUnique(int[] nums) {
        Array.Sort(nums);
        var result = new List<IList<int>>();
        var used = new bool[nums.Length];
        var current = new List<int>();

        void Backtrack() {
            if (current.Count == nums.Length) {
                result.Add(new List<int>(current));
                return;
            }

            for (int i = 0; i < nums.Length; i++) {
                if (used[i]) continue;

                // Skip duplicates: only use the first unused duplicate
                if (i > 0 && nums[i] == nums[i - 1] && !used[i - 1])
                    continue;

                used[i] = true;
                current.Add(nums[i]);

                Backtrack();

                used[i] = false;
                current.RemoveAt(current.Count - 1);
            }
        }

        Backtrack();
        return result;
    }
}
