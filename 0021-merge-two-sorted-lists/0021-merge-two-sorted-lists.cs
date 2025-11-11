

public class Solution {
    public ListNode MergeTwoLists(ListNode list1, ListNode list2) {
        // Create a dummy node to simplify edge cases
        ListNode dummy = new ListNode();
        ListNode current = dummy;

        // Traverse both lists
        while (list1 != null && list2 != null) {
            if (list1.val < list2.val) {
                current.next = list1;
                list1 = list1.next;
            } else {
                current.next = list2;
                list2 = list2.next;
            }
            current = current.next;
        }

        // Attach the remaining part of either list
        current.next = list1 ?? list2;

        return dummy.next;
    }
}
