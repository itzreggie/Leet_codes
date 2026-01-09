/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */

public class Solution {
    public TreeNode SubtreeWithAllDeepest(TreeNode root) {
        return DFS(root).node;
    }

    private (TreeNode node, int depth) DFS(TreeNode root) {
        if (root == null)
            return (null, 0);

        var left = DFS(root.left);
        var right = DFS(root.right);

        if (left.depth > right.depth)
            return (left.node, left.depth + 1);

        if (right.depth > left.depth)
            return (right.node, right.depth + 1);

        // depths equal → this node is the LCA of deepest nodes
        return (root, left.depth + 1);
    }
}
