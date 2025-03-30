public class Solution {
    public IList<int> PartitionLabels(string s) {
    var lastIndex = new int[26]; // Store the last index of each character
    for (int i = 0; i < s.Length; i++) {
        lastIndex[s[i] - 'a'] = i; // Update last occurrence of each character
    }

    var result = new List<int>();
    int start = 0, end = 0;

    for (int i = 0; i < s.Length; i++) {
        end = Math.Max(end, lastIndex[s[i] - 'a']); // Expand the end to include all characters
        if (i == end) { // Partition is complete
            result.Add(end - start + 1); // Add partition size
            start = i + 1; // Update the start for the next partition
        }
    }

    return result;
}

}