using System;
using System.Collections.Generic;

public class Solution {
    public bool JudgePoint24(int[] cards) {
        List<double> nums = new List<double>();
        foreach (int card in cards) {
            nums.Add((double)card);
        }
        return Backtrack(nums);
    }

    private bool Backtrack(List<double> nums) {
        if (nums.Count == 1) {
            return Math.Abs(nums[0] - 24) < 1e-6;
        }

        for (int i = 0; i < nums.Count; i++) {
            for (int j = 0; j < nums.Count; j++) {
                if (i == j) continue;

                List<double> next = new List<double>();
                for (int k = 0; k < nums.Count; k++) {
                    if (k != i && k != j) {
                        next.Add(nums[k]);
                    }
                }

                foreach (double result in Compute(nums[i], nums[j])) {
                    next.Add(result);
                    if (Backtrack(next)) return true;
                    next.RemoveAt(next.Count - 1);
                }
            }
        }

        return false;
    }

    private List<double> Compute(double a, double b) {
        List<double> results = new List<double>();
        results.Add(a + b);
        results.Add(a - b);
        results.Add(b - a);
        results.Add(a * b);
        if (Math.Abs(b) > 1e-6) results.Add(a / b);
        if (Math.Abs(a) > 1e-6) results.Add(b / a);
        return results;
    }
}
