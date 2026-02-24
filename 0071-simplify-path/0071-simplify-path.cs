public class Solution {
    public string SimplifyPath(string path) {
        var parts = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        var stack = new Stack<string>();

        foreach (var part in parts)
        {
            if (part == ".")
            {
                // Current directory → ignore
                continue;
            }
            else if (part == "..")
            {
                // Go up one directory if possible
                if (stack.Count > 0)
                    stack.Pop();
            }
            else
            {
                // Normal directory name (including "..." or "....")
                stack.Push(part);
            }
        }

        // Build canonical path
        var result = "/" + string.Join("/", stack.Reverse());

        return result;
    }
}
