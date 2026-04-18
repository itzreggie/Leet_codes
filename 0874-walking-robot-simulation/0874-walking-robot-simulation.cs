public class Solution {
    public int RobotSim(int[] commands, int[][] obstacles) {
        // Directions: North, East, South, West
        int[][] dirs = new int[][] {
            new int[] {0, 1},   // North
            new int[] {1, 0},   // East
            new int[] {0, -1},  // South
            new int[] {-1, 0}   // West
        };

        int dir = 0; // start facing north
        int x = 0, y = 0;
        int maxDist = 0;

        // Store obstacles in a hash set for O(1) lookup
        HashSet<string> obs = new HashSet<string>();
        foreach (var o in obstacles) {
            obs.Add(o[0] + "," + o[1]);
        }

        foreach (int cmd in commands) {
            if (cmd == -2) {
                // turn left
                dir = (dir + 3) % 4;
            } else if (cmd == -1) {
                // turn right
                dir = (dir + 1) % 4;
            } else {
                // move forward cmd steps
                for (int i = 0; i < cmd; i++) {
                    int nx = x + dirs[dir][0];
                    int ny = y + dirs[dir][1];

                    // Check obstacle
                    if (obs.Contains(nx + "," + ny)) {
                        break; // stop moving for this command
                    }

                    x = nx;
                    y = ny;

                    maxDist = Math.Max(maxDist, x * x + y * y);
                }
            }
        }

        return maxDist;
    }
}
