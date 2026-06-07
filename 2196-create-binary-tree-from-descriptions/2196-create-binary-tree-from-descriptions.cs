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
    public TreeNode CreateBinaryTree(int[][] descriptions) {
        var nodes = new Dictionary<int, TreeNode>();
        var hasParent = new HashSet<int>();

        foreach (var d in descriptions) {
            int parent = d[0];
            int child = d[1];
            int isLeft = d[2];

            if (!nodes.ContainsKey(parent))
                nodes[parent] = new TreeNode(parent);

            if (!nodes.ContainsKey(child))
                nodes[child] = new TreeNode(child);

            if (isLeft == 1)
                nodes[parent].left = nodes[child];
            else
                nodes[parent].right = nodes[child];

            hasParent.Add(child);
        }

        // root = the one node that never appears as a child
        foreach (var kv in nodes) {
            if (!hasParent.Contains(kv.Key))
                return kv.Value;
        }

        return null; // should never happen with valid input
    }
}
