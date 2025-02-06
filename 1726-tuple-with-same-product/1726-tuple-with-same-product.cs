public class Solution {
    public int TupleSameProduct(int[] nums) {
        int count = 0;
        Dictionary<int, int> productCount = new Dictionary<int, int>();
        
        for (int i = 0; i < nums.Length; i++) {
            for (int j = i + 1; j < nums.Length; j++) {
                int product = nums[i] * nums[j];
                if (productCount.ContainsKey(product)) {
                    count += 8 * productCount[product];
                    productCount[product]++;
                } else {
                    productCount[product] = 1;
                }
            }
        }
        
        return count;
    }
}
