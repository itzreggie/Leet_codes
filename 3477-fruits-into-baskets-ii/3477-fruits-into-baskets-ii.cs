

public class Solution {
    public int NumOfUnplacedFruits(int[] fruits, int[] baskets) {
        int n = fruits.Length;
        bool[] used = new bool[n];
        int unplaced = 0;

        for (int i = 0; i < fruits.Length; i++) {
            bool placed = false;
            for (int j = 0; j < baskets.Length; j++) {
                if (!used[j] && baskets[j] >= fruits[i]) {
                    used[j] = true;
                    placed = true;
                    break;
                }
            }

            if (!placed) {
                unplaced++;
            }
        }

        return unplaced;
    }
}
