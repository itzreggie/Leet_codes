public class Solution {
    public long GoodTriplets(int[] nums1, int[] nums2) {
        int n = nums1.Length;

        // Step 1: Map each number to its index in nums2
        int[] posInNums2 = new int[n];
        for (int i = 0; i < n; i++) {
            posInNums2[nums2[i]] = i;
        }

        // Step 2: Transform nums1 into a new array using positions in nums2
        int[] transformed = new int[n];
        for (int i = 0; i < n; i++) {
            transformed[i] = posInNums2[nums1[i]];
        }

        // Step 3: Count increasing subsequences of length 3
        // BIT1 tracks how many elements are less than current (left side)
        // BIT2 tracks how many pairs (i,j) exist where i < j and transformed[i] < transformed[j]
        BIT bit1 = new BIT(n);
        BIT bit2 = new BIT(n);

        long result = 0;

        foreach (int num in transformed) {
            long left = bit1.Query(num);       // # elements less than current before
            long right = bit2.Query(num);      // # pairs (i, j) where i < j < k
            result += right;
            bit2.Update(num, left);            // Store # of increasing pairs ending at this point
            bit1.Update(num, 1);               // Mark this index as seen
        }

        return result;
    }

    // Binary Indexed Tree (Fenwick Tree) for prefix sums
    public class BIT {
        private long[] tree;
        private int size;

        public BIT(int size) {
            this.size = size + 2;
            tree = new long[this.size];
        }

        public void Update(int i, long delta) {
            i += 1;
            while (i < size) {
                tree[i] += delta;
                i += i & -i;
            }
        }

        public long Query(int i) {
            i += 1;
            long sum = 0;
            while (i > 0) {
                sum += tree[i];
                i -= i & -i;
            }
            return sum;
        }
    }
}
