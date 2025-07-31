public class Solution {
    public int MaximumGain(string s, int x, int y) {
        long score1, score2;
        string rem;

        if (x < y) {
            score1 = RemoveScore(s, 'b', 'a', y, out rem);
            score2 = RemoveScore(rem, 'a', 'b', x, out _);
        } else {
            score1 = RemoveScore(s, 'a', 'b', x, out rem);
            score2 = RemoveScore(rem, 'b', 'a', y, out _);
        }

        return (int)(score1 + score2);
    }

    private long RemoveScore(string str, char first, char second, int score, out string remainder) {
        var sb = new System.Text.StringBuilder();
        long total = 0;
        foreach (char c in str) {
            sb.Append(c);
            int len = sb.Length;
            if (len >= 2 && sb[len - 2] == first && sb[len - 1] == second) {
                sb.Length = len - 2;
                total += score;
            }
        }
        remainder = sb.ToString();
        return total;
    }
}
