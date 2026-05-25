public class Solution {
    public bool CanReach(string s, int minJump, int maxJump) {
        int n = s.Length;
        if (s[n - 1] == '1') return false;

        bool[] reachable = new bool[n];
        reachable[0] = true;

        int windowCount = 0;

        for (int i = 1; i < n; i++) {
            // Expand window: add left boundary
            if (i - minJump >= 0 && reachable[i - minJump]) {
                windowCount++;
            }

            // Shrink window: remove right boundary
            if (i - maxJump - 1 >= 0 && reachable[i - maxJump - 1]) {
                windowCount--;
            }

            // If window has at least one reachable index and s[i] == '0'
            if (windowCount > 0 && s[i] == '0') {
                reachable[i] = true;
            }
        }

        return reachable[n - 1];
    }
}
