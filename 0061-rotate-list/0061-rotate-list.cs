public class Solution {
    public ListNode RotateRight(ListNode head, int k) {
        if (head == null || head.next == null || k == 0)
            return head;

        // Step 1: Count length and get tail
        ListNode tail = head;
        int length = 1;
        while (tail.next != null) {
            tail = tail.next;
            length++;
        }

        // Step 2: Make it circular
        tail.next = head;

        // Step 3: Find new tail: (length - k % length - 1)
        int stepsToNewTail = length - (k % length) - 1;
        ListNode newTail = head;

        for (int i = 0; i < stepsToNewTail; i++) {
            newTail = newTail.next;
        }

        // Step 4: New head is next of newTail
        ListNode newHead = newTail.next;
        newTail.next = null; // break the circle

        return newHead;
    }
}
