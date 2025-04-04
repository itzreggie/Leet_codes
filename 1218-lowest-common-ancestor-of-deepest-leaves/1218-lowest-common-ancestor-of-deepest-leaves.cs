public class Solution {
    public TreeNode LcaDeepestLeaves(TreeNode root) {
        return Helper(root).Item1;
    }

    private (TreeNode, int) Helper(TreeNode node) {
        if (node == null) return (null, 0);

        var left = Helper(node.left);
        var right = Helper(node.right);

        if (left.Item2 == right.Item2) {
            return (node, left.Item2 + 1);
        } else if (left.Item2 > right.Item2) {
            return (left.Item1, left.Item2 + 1);
        } else {
            return (right.Item1, right.Item2 + 1);
        }
    }
}
