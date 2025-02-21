public class FindElements {
    private HashSet<int> recoveredValues;

    public FindElements(TreeNode root) {
        recoveredValues = new HashSet<int>();
        Recover(root, 0);
    }

    private void Recover(TreeNode node, int val) {
        if (node == null) return;
        node.val = val;
        recoveredValues.Add(val);
        Recover(node.left, 2 * val + 1);
        Recover(node.right, 2 * val + 2);
    }

    public bool Find(int target) {
        return recoveredValues.Contains(target);
    }
}
