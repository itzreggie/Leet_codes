/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */

public class Solution {
    public ListNode SortList(ListNode head) {
        if (head == null || head.next == null)
            return head;

        // Step 1: Split the list into two halves
        ListNode slow = head, fast = head, prev = null;

        while (fast != null && fast.next != null) {
            prev = slow;
            slow = slow.next;
            fast = fast.next.next;
        }

        // Cut the list into two halves
        prev.next = null;

        // Step 2: Sort each half
        ListNode left = SortList(head);
        ListNode right = SortList(slow);

        // Step 3: Merge the sorted halves
        return Merge(left, right);
    }

    private ListNode Merge(ListNode l1, ListNode l2) {
        ListNode dummy = new ListNode();
        ListNode current = dummy;

        while (l1 != null && l2 != null) {
            if (l1.val < l2.val) {
                current.next = l1;
                l1 = l1.next;
            } else {
                current.next = l2;
                l2 = l2.next;
            }
            current = current.next;
        }

        // Attach the remaining nodes
        current.next = (l1 != null) ? l1 : l2;

        return dummy.next;
    }
}
