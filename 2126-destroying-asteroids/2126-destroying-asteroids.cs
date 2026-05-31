public class Solution {
    public bool AsteroidsDestroyed(int mass, int[] asteroids) {
        Array.Sort(asteroids);

        long current = mass; // use long to avoid overflow

        foreach (int a in asteroids) {
            if (current < a) return false;
            current += a;
        }

        return true;
    }
}
