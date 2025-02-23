public class Solution {
    public TreeNode ConstructFromPrePost(int[] preorder, int[] postorder) {
        return BuildTree(preorder, 0, preorder.Length - 1, postorder, 0, postorder.Length - 1);
    }
    
    private TreeNode BuildTree(int[] preorder, int preStart, int preEnd, int[] postorder, int postStart, int postEnd) {
        if (preStart > preEnd || postStart > postEnd) return null;
        
        TreeNode root = new TreeNode(preorder[preStart]);
        if (preStart == preEnd) return root;
        
        int leftRootVal = preorder[preStart + 1];
        int index = postStart;
        while (postorder[index] != leftRootVal) {
            index++;
        }
        int leftSize = index - postStart + 1;
        
        root.left = BuildTree(preorder, preStart + 1, preStart + leftSize, postorder, postStart, index);
        root.right = BuildTree(preorder, preStart + leftSize + 1, preEnd, postorder, index + 1, postEnd - 1);
        
        return root;
    }
}
