using System.Collections.Generic;

public class ProductOfNumbers {
    private List<int> nums;

    public ProductOfNumbers() {
        nums = new List<int>();
    }

    public void Add(int num) {
        nums.Add(num);
    }

    public int GetProduct(int k) {
        int product = 1;
        for (int i = nums.Count - k; i < nums.Count; i++) {
            product *= nums[i];
        }
        return product;
    }
}
