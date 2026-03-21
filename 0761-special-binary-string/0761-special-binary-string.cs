public class Solution {
    public string MakeLargestSpecial(string s) {
        List<string> parts = new List<string>();
        int count = 0;
        int start = 0;

        for (int i = 0; i < s.Length; i++) {
            if (s[i] == '1') count++;
            else count--;

            // When count == 0, we found a full special substring
            if (count == 0) {
                // Recursively process the inside part
                string inner = s.Substring(start + 1, i - start - 1);
                string processed = MakeLargestSpecial(inner);

                // Wrap it back into a special string
                parts.Add("1" + processed + "0");

                start = i + 1;
            }
        }

        // Sort descending to get lexicographically largest
        parts.Sort((a, b) => string.Compare(b, a));

        return string.Concat(parts);
    }
}
