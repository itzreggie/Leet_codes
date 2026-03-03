public class Solution {
    public IList<IList<string>> Partition(string s) {
        var result = new List<IList<string>>();
        Backtrack(0, s, new List<string>(), result);
        return result;
    }

    private void Backtrack(int start, string s, List<string> current, IList<IList<string>> result) {
        if (start == s.Length) {
            result.Add(new List<string>(current));
            return;
        }

        for (int end = start; end < s.Length; end++) {
            if (IsPalindrome(s, start, end)) {
                current.Add(s.Substring(start, end - start + 1));
                Backtrack(end + 1, s, current, result);
                current.RemoveAt(current.Count - 1);
            }
        }
    }

    private bool IsPalindrome(string s, int left, int right) {
        while (left < right) {
            if (s[left] != s[right]) return false;
            left++;
            right--;
        }
        return true;
    }
}
