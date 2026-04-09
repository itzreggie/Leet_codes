public class Robot {
    private int width, height;
    private int x, y;
    private int dir; 
    // 0 = East, 1 = North, 2 = West, 3 = South

    private readonly string[] dirNames = { "East", "North", "West", "South" };
    private readonly int[] dx = { 1, 0, -1, 0 };
    private readonly int[] dy = { 0, 1, 0, -1 };

    private int perimeter;

    public Robot(int width, int height) {
        this.width = width;
        this.height = height;
        this.x = 0;
        this.y = 0;
        this.dir = 0; // East

        // Perimeter of the walking loop
        perimeter = 2 * (width + height - 2);
    }

    public void Step(int num) {
        if (perimeter == 0) return;

        num %= perimeter;

        // Special case: full loop → direction must update
        if (num == 0) num = perimeter;

        while (num > 0) {
            int nx = x + dx[dir];
            int ny = y + dy[dir];

            // Out of bounds → turn CCW
            if (nx < 0 || nx >= width || ny < 0 || ny >= height) {
                dir = (dir + 1) % 4;
                continue;
            }

            // Move
            x = nx;
            y = ny;
            num--;
        }
    }

    public int[] GetPos() {
        return new int[] { x, y };
    }

    public string GetDir() {
        return dirNames[dir];
    }
}
