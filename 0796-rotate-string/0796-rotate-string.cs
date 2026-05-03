public class Solution {
    public bool RotateString(string s, string goal) {
        if (s.Length != goal.Length)
            return false;

        string doubled = s + s;
        return doubled.Contains(goal);
    }
}
