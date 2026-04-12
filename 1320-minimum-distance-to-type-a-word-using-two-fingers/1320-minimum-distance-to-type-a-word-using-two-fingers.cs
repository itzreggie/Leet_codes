public class Solution {
    // Keyboard coordinates for A–Z
    private readonly (int x, int y)[] pos = new (int, int)[26];

    public int MinimumDistance(string word) {
        // Build keyboard coordinates
        for (int i = 0; i < 26; i++) {
            pos[i] = (i / 6, i % 6);
        }

        // dp[index][finger1][finger2] = minimum cost
        var memo = new Dictionary<(int, int, int), int>();
        return DFS(0, -1, -1, word, memo);
    }

    private int DFS(int i, int f1, int f2, string word,
                    Dictionary<(int, int, int), int> memo) {

        if (i == word.Length) return 0;

        var key = (i, f1, f2);
        if (memo.ContainsKey(key)) return memo[key];

        int cur = word[i] - 'A';

        // Option 1: move finger 1
        int cost1 = (f1 == -1 ? 0 : Dist(f1, cur)) +
                    DFS(i + 1, cur, f2, word, memo);

        // Option 2: move finger 2
        int cost2 = (f2 == -1 ? 0 : Dist(f2, cur)) +
                    DFS(i + 1, f1, cur, word, memo);

        int best = Math.Min(cost1, cost2);
        memo[key] = best;
        return best;
    }

    private int Dist(int a, int b) {
        var (x1, y1) = pos[a];
        var (x2, y2) = pos[b];
        return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
    }
}
