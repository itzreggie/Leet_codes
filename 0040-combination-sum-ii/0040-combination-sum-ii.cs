public class Solution {
    public IList<IList<int>> CombinationSum2(int[] candidates, int target) {
        Array.Sort(candidates);
        var results = new List<IList<int>>();
        Backtrack(candidates, target, 0, new List<int>(), results);
        return results;
    }

    private void Backtrack(int[] candidates, int target, int start, List<int> current, List<IList<int>> results) {
        if (target == 0) {
            results.Add(new List<int>(current));
            return;
        }

        for (int i = start; i < candidates.Length; i++) {
            // Skip duplicates at the same depth
            if (i > start && candidates[i] == candidates[i - 1])
                continue;

            // Stop early if number is too large
            if (candidates[i] > target)
                break;

            current.Add(candidates[i]);
            Backtrack(candidates, target - candidates[i], i + 1, current, results);
            current.RemoveAt(current.Count - 1);
        }
    }
}
