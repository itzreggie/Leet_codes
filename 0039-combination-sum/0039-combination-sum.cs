


using System;
using System.Collections.Generic;

public class Solution {
    public IList<IList<int>> CombinationSum(int[] candidates, int target) {
        var result = new List<IList<int>>();
        var combination = new List<int>();
        Backtrack(candidates, target, 0, combination, result);
        return result;
    }

    private void Backtrack(int[] candidates, int target, int start, List<int> combination, IList<IList<int>> result) {
        if (target == 0) {
            // Found a valid combination
            result.Add(new List<int>(combination));
            return;
        }

        for (int i = start; i < candidates.Length; i++) {
            if (candidates[i] <= target) {
                // Choose the candidate
                combination.Add(candidates[i]);
                // Recurse with reduced target (same i because unlimited use allowed)
                Backtrack(candidates, target - candidates[i], i, combination, result);
                // Undo the choice (backtrack)
                combination.RemoveAt(combination.Count - 1);
            }
        }
    }
}
