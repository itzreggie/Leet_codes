public class Solution {
    public int FindNumbers(int[] nums) {
        int count = 0;
        foreach (int num in nums) {
            // Convert the number to string and count its characters (digits)
            if(num.ToString().Length % 2 == 0)
                count++;
        }
        return count;
    }
}
