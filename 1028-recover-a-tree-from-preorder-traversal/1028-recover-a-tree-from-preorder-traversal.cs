public class Solution {
    public TreeNode RecoverFromPreorder(string traversal) {
        Stack<TreeNode> stack = new Stack<TreeNode>();
        int index = 0;
        int n = traversal.Length;
        while (index < n) {
            int depth = 0;
            while (index < n && traversal[index] == '-') {
                depth++;
                index++;
            }
            int val = 0;
            while (index < n && char.IsDigit(traversal[index])) {
                val = val * 10 + (traversal[index] - '0');
                index++;
            }
            TreeNode node = new TreeNode(val);
            if (depth == stack.Count) {
                if (stack.Count > 0)
                    stack.Peek().left = node;
            } else {
                while (depth != stack.Count)
                    stack.Pop();
                if (stack.Count > 0)
                    stack.Peek().right = node;
            }
            stack.Push(node);
        }
        while (stack.Count > 1)
            stack.Pop();
        return stack.Peek();
    }
}
