public class Solution {
    public string PushDominoes(string dominoes) {
        int n = dominoes.Length;
        int[] forces = new int[n];
        int force = 0;

        // Left-to-right pass: assign "rightward" forces.
        for (int i = 0; i < n; i++) {
            if (dominoes[i] == 'R') {
                force = n; // Use a large number as "infinite" force.
            } else if (dominoes[i] == 'L') {
                force = 0;
            } else {
                force = Math.Max(force - 1, 0);
            }
            forces[i] = force;
        }

        force = 0;
        char[] result = new char[n];

        // Right-to-left pass: assign "leftward" forces (subtract them).
        for (int i = n - 1; i >= 0; i--) {
            if (dominoes[i] == 'L') {
                force = n;
            } else if (dominoes[i] == 'R') {
                force = 0;
            } else {
                force = Math.Max(force - 1, 0);
            }
            int net = forces[i] - force;
            if (net > 0) result[i] = 'R';
            else if (net < 0) result[i] = 'L';
            else result[i] = '.';
        }
        
        return new string(result);
    }
}
