public class Solution {
    public int StrStr(string haystack, string needle) {
        // Edge case: if needle is empty, return 0
        if (string.IsNullOrEmpty(needle)) {
            return 0;
        }

        // Use built-in IndexOf for simplicity
        int index = haystack.IndexOf(needle);
        return index; // returns -1 if not found
    }
}
