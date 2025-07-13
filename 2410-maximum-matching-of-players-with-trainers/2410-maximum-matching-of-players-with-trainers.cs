using System;

public class Solution {
    public int MatchPlayersAndTrainers(int[] players, int[] trainers) {
        // Sort both arrays so we can do a one‐pass greedy match
        Array.Sort(players);
        Array.Sort(trainers);
        
        int i = 0, j = 0, matches = 0;
        int n = players.Length, m = trainers.Length;
        
        // Two pointers: try to match the i-th player with the j-th trainer
        while (i < n && j < m) {
            if (players[i] <= trainers[j]) {
                // Found a match
                matches++;
                i++;
                j++;
            } else {
                // This trainer can't train player i; try a stronger trainer
                j++;
            }
        }
        
        return matches;
    }
}
