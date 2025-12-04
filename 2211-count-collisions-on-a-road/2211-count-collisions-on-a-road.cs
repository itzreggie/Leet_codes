public class Solution {
    public int CountCollisions(string directions) {
        // Convert to char array for easier handling
        char[] arr = directions.ToCharArray();
        int n = arr.Length;

        // Trim leading L's (they never collide)
        int left = 0;
        while (left < n && arr[left] == 'L') {
            left++;
        }

        // Trim trailing R's (they never collide)
        int right = n - 1;
        while (right >= 0 && arr[right] == 'R') {
            right--;
        }

        int collisions = 0;
        // Count all non-stationary cars in the middle segment
        for (int i = left; i <= right; i++) {
            if (arr[i] != 'S') {
                collisions++;
            }
        }

        return collisions;
    }
}
