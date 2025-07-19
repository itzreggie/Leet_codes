public class Solution {
    public IList<string> RemoveSubfolders(string[] folder) {
        Array.Sort(folder, StringComparer.Ordinal); // Sort paths lexicographically
        var result = new List<string>();

        foreach (string path in folder) {
            if (result.Count == 0 || !path.StartsWith(result[result.Count - 1] + "/")) {
                result.Add(path);
            }
        }

        return result;
    }
}
