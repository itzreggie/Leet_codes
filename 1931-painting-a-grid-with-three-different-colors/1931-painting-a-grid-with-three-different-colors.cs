using System;
using System.Collections.Generic;

public class Solution {
    const int MOD = 1000000007;

    public int ColorTheGrid(int m, int n) {
        // Step 1: Generate all valid row colorings for a row of length m.
        List<int> validRows = GenerateValidRows(m);
        
        // Step 2: Precompute allowed transitions between rows.
        Dictionary<int, List<int>> transitions = GenerateTransitions(validRows, m);
        
        // Step 3: Set up DP.
        // dp[col][pattern] represents the count of ways to color up to column 'col' ending with row pattern 'pattern'.
        var dp = new Dictionary<int, Dictionary<int, long>>();
        dp[0] = new Dictionary<int, long>();
        // Initialize for column 0: every valid row pattern counts as 1 way.
        foreach (var row in validRows)
            dp[0][row] = 1;
        
        // Fill DP for columns 1 through n-1.
        for (int col = 1; col < n; col++) {
            dp[col] = new Dictionary<int, long>();
            foreach (var row in validRows) {
                long count = 0;
                // For every allowed previous row pattern that can transition into current pattern.
                foreach (var prevRow in transitions[row]) {
                    // It should be present in the dp for previous column.
                    if (dp[col - 1].ContainsKey(prevRow))
                        count = (count + dp[col - 1][prevRow]) % MOD;
                }
                dp[col][row] = count;
            }
        }
        
        // Sum the ways for all patterns in the last column.
        long result = 0;
        foreach (var row in validRows)
            result = (result + dp[n - 1][row]) % MOD;
        
        return (int)result;
    }
    
    // Generate all valid ways to color one row with length m,
    // ensuring no two adjacent cells have the same color.
    // Colors are encoded as a 2-bit value for each cell.
    private List<int> GenerateValidRows(int m) {
        var validRows = new List<int>();
        GenerateRowsHelper(m, 0, 0, validRows);
        return validRows;
    }
    
    private void GenerateRowsHelper(int m, int index, int rowMask, List<int> validRows) {
        if (index == m) {
            validRows.Add(rowMask);
            return;
        }
        // Color choices are 1, 2, 3 (we avoid 0 to simplify our bit operations).
        for (int color = 1; color <= 3; color++) {
            // Check adjacent cell in the same row (if index > 0).
            if (index == 0 || (((rowMask >> ((index - 1) * 2)) & 3) != color)) {
                GenerateRowsHelper(m, index + 1, rowMask | (color << (index * 2)), validRows);
            }
        }
    }
    
    // Check if two row patterns can be adjacent vertically.
    // They are valid if for every cell the colors differ.
    private bool IsValidTransition(int row1, int row2, int m) {
        for (int i = 0; i < m; i++) {
            int color1 = (row1 >> (i * 2)) & 3;
            int color2 = (row2 >> (i * 2)) & 3;
            if (color1 == color2)
                return false;
        }
        return true;
    }
    
    // For each valid row pattern, precompute which previous row patterns can transition into it.
    private Dictionary<int, List<int>> GenerateTransitions(List<int> validRows, int m) {
        var transitions = new Dictionary<int, List<int>>();
        foreach (var row in validRows) {
            transitions[row] = new List<int>();
            foreach (var prevRow in validRows) {
                if (IsValidTransition(row, prevRow, m)) {
                    transitions[row].Add(prevRow);
                }
            }
        }
        return transitions;
    }
}
