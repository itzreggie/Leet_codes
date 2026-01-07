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
    private const long MOD = 1000000007;
    private long totalSum = 0;
    private long maxProduct = 0;

    public int MaxProduct(TreeNode root) {
        // 1. Compute total sum of the tree
        totalSum = GetSum(root);

        // 2. Compute max product by checking every subtree
        ComputeMax(root);

        return (int)(maxProduct % MOD);
    }

    // First DFS: compute total sum
    private long GetSum(TreeNode node) {
        if (node == null) return 0;
        return node.val + GetSum(node.left) + GetSum(node.right);
    }

    // Second DFS: compute subtree sums and update max product
    private long ComputeMax(TreeNode node) {
        if (node == null) return 0;

        long left = ComputeMax(node.left);
        long right = ComputeMax(node.right);

        long subSum = node.val + left + right;

        long product = subSum * (totalSum - subSum);
        if (product > maxProduct)
            maxProduct = product;

        return subSum;
    }
}
