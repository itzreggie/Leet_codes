public class Solution {
    public string RemoveOccurrences(string s, string part) {
        while (s.Contains(part)) {
            int index = s.IndexOf(part);
            s = s.Remove(index, part.Length);
        }
        return s;
    }
}
