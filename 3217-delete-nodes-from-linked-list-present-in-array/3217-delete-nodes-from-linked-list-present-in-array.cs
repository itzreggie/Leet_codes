public class Solution {
   public ListNode ModifiedList(int[] nums, ListNode head)
 {
        HashSet<int> toRemove = new HashSet<int>(nums);
        ListNode dummy = new ListNode(0, head);
        ListNode current = dummy;

        while (current.next != null) {
            if (toRemove.Contains(current.next.val)) {
                current.next = current.next.next;
            } else {
                current = current.next;
            }
        }

        return dummy.next;
    }
}
