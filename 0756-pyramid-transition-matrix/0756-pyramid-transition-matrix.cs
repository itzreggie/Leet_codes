public class Solution {
    public bool PyramidTransition(string bottom, IList<string> allowed) {
        // Map from pair "AB" -> list of possible tops ['C','D',...]
        var map = new Dictionary<string, List<char>>();

        foreach (var rule in allowed) {
            string key = rule.Substring(0, 2);
            char top = rule[2];

            if (!map.ContainsKey(key))
                map[key] = new List<char>();

            map[key].Add(top);
        }

        return CanBuild(bottom, map);
    }

    private bool CanBuild(string row, Dictionary<string, List<char>> map) {
        // If we reached the top
        if (row.Length == 1)
            return true;

        // Generate all possible next rows
        var allNextRows = new List<string>();
        BuildNextRows(row, 0, new StringBuilder(), allNextRows, map);

        // Try each possible next row
        foreach (var next in allNextRows) {
            if (CanBuild(next, map))
                return true;
        }

        return false;
    }

    private void BuildNextRows(string row, int index, StringBuilder current,
                               List<string> results, Dictionary<string, List<char>> map) {
        if (index == row.Length - 1) {
            results.Add(current.ToString());
            return;
        }

        string key = row.Substring(index, 2);
        if (!map.ContainsKey(key))
            return;

        foreach (char c in map[key]) {
            current.Append(c);
            BuildNextRows(row, index + 1, current, results, map);
            current.Length--; // backtrack
        }
    }
}
