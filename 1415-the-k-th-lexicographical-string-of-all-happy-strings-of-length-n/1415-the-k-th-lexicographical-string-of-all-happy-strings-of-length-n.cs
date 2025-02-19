public class Solution {
    public string GetHappyString(int n, int k) {
        var result = new List<string>();
        GenerateHappyStrings(n, "", result);
        return result.Count >= k ? result[k - 1] : "";
    }

    private void GenerateHappyStrings(int n, string current, List<string> result) {
        if (current.Length == n) {
            result.Add(current);
            return;
        }

        foreach (var ch in new char[] { 'a', 'b', 'c' }) {
            if (current.Length == 0 || current[current.Length - 1] != ch) {
                GenerateHappyStrings(n, current + ch, result);
            }
        }
    }
}
