public class Solution {
    public int NumOfUnplacedFruits(int[] fruits, int[] baskets) {
        Array.Sort(fruits);   // Sort fruits in ascending order
        Array.Sort(baskets);  // Sort baskets in ascending order

        int i = 0; // Pointer for fruits
        int j = 0; // Pointer for baskets

        int n = fruits.Length;

        while (i < n && j < n) {
            if (baskets[j] >= fruits[i]) {
                // Place fruit[i] in basket[j]
                i++;
                j++;
            } else {
                // Basket too small, try next basket
                j++;
            }
        }

        return n - i; // Remaining unplaced fruits
    }
}
