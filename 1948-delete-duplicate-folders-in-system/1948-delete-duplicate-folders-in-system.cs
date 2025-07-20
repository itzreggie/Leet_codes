public class Solution {
    class TrieNode {
        public Dictionary<string, TrieNode> Children = new();
        public string Name = "";
        public bool Deleted = false;
    }

    Dictionary<string, List<TrieNode>> duplicates = new();

    public IList<IList<string>> DeleteDuplicateFolder(IList<IList<string>> paths) {
        var root = new TrieNode();

        // Build the trie
        foreach (var path in paths) {
            var node = root;
            foreach (var dir in path) {
                if (!node.Children.ContainsKey(dir)) {
                    node.Children[dir] = new TrieNode { Name = dir };
                }
                node = node.Children[dir];
            }
        }

        // Serialize each subtree to detect duplicates
        Serialize(root);

        // Mark duplicates
        foreach (var group in duplicates.Values) {
            if (group.Count > 1) {
                foreach (var node in group) {
                    node.Deleted = true;
                }
            }
        }

        // Collect results
        var result = new List<IList<string>>();
        DFS(root, new List<string>(), result);
        return result;
    }

    string Serialize(TrieNode node) {
        if (node.Children.Count == 0) return "";

        var sb = new StringBuilder();
        foreach (var kvp in node.Children.OrderBy(k => k.Key)) {
            var childSerial = Serialize(kvp.Value);
            sb.Append("(").Append(kvp.Key).Append(childSerial).Append(")");
        }

        var signature = sb.ToString();
        if (!duplicates.ContainsKey(signature)) {
            duplicates[signature] = new List<TrieNode>();
        }
        duplicates[signature].Add(node);

        return signature;
    }

    void DFS(TrieNode node, List<string> path, List<IList<string>> result) {
        foreach (var kvp in node.Children) {
            if (!kvp.Value.Deleted) {
                path.Add(kvp.Key);
                result.Add(new List<string>(path));
                DFS(kvp.Value, path, result);
                path.RemoveAt(path.Count - 1);
            }
        }
    }
}
