public class Solution {
    public int SumRootToLeaf(TreeNode root) {
        return Dfs(root, 0);
    }

    private int Dfs(TreeNode node, int current) {
        if (node == null)
            return 0;

        // Shift left (multiply by 2) and add current bit
        current = (current << 1) | node.val;

        // If it's a leaf, return the accumulated value
        if (node.left == null && node.right == null)
            return current;

        // Otherwise continue down both sides
        return Dfs(node.left, current) + Dfs(node.right, current);
    }
}
