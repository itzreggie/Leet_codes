using System;

public class Solution {
    public int Candy(int[] ratings) {
        int n = ratings.Length;
        if (n == 0) return 0;

        int[] candies = new int[n];
        Array.Fill(candies, 1); // Every child gets at least one candy

        // Left-to-right pass: Ensure higher-rated child gets more than left neighbor
        for (int i = 1; i < n; i++) {
            if (ratings[i] > ratings[i - 1]) {
                candies[i] = candies[i - 1] + 1;
            }
        }

        // Right-to-left pass: Ensure higher-rated child gets more than right neighbor
        for (int i = n - 2; i >= 0; i--) {
            if (ratings[i] > ratings[i + 1]) {
                candies[i] = Math.Max(candies[i], candies[i + 1] + 1);
            }
        }

        // Sum up the candies
        int totalCandies = 0;
        foreach (int c in candies) {
            totalCandies += c;
        }
        return totalCandies;
    }

   
}
