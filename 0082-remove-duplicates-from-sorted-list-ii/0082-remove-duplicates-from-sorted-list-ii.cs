public class Solution {
    public ListNode DeleteDuplicates(ListNode head) {
        if (head == null) return null;

        ListNode dummy = new ListNode(0, head);
        ListNode prev = dummy;

        while (head != null) {
            // If current value is duplicated
            if (head.next != null && head.val == head.next.val) {
                int duplicateVal = head.val;

                // Skip all nodes with this value
                while (head != null && head.val == duplicateVal) {
                    head = head.next;
                }

                // Link prev to the first non-duplicate node
                prev.next = head;
            } else {
                // No duplicate, move prev forward
                prev = prev.next;
                head = head.next;
            }
        }

        return dummy.next;
    }
}
