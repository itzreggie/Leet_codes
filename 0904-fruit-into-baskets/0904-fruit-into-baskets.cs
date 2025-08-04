public class Solution {
    public int TotalFruit(int[] fruits) {
        int maxFruits = 0;
        int left = 0;
        Dictionary<int, int> basket = new Dictionary<int, int>();

        for (int right = 0; right < fruits.Length; right++) {
            int fruit = fruits[right];

            // Add current fruit to the basket
            if (!basket.ContainsKey(fruit))
                basket[fruit] = 0;
            basket[fruit]++;

            // If we have more than 2 types of fruits, shrink the window
            while (basket.Count > 2) {
                int leftFruit = fruits[left];
                basket[leftFruit]--;
                if (basket[leftFruit] == 0)
                    basket.Remove(leftFruit);
                left++;
            }

            // Update max fruits collected in a valid window
            maxFruits = Math.Max(maxFruits, right - left + 1);
        }

        return maxFruits;
    }
}
