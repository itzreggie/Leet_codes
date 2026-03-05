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
    public IList<TreeNode> GenerateTrees(int n) {
        if (n == 0) return new List<TreeNode>();
        return BuildTrees(1, n);
    }

    private IList<TreeNode> BuildTrees(int start, int end) {
        List<TreeNode> result = new List<TreeNode>();

        // Base case: empty tree
        if (start > end) {
            result.Add(null);
            return result;
        }

        // Try each number as the root
        for (int rootVal = start; rootVal <= end; rootVal++) {
            // All possible left subtrees
            var leftTrees = BuildTrees(start, rootVal - 1);

            // All possible right subtrees
            var rightTrees = BuildTrees(rootVal + 1, end);

            // Combine left + right with rootVal
            foreach (var left in leftTrees) {
                foreach (var right in rightTrees) {
                    TreeNode root = new TreeNode(rootVal);
                    root.left = left;
                    root.right = right;
                    result.Add(root);
                }
            }
        }

        return result;
    }
}
