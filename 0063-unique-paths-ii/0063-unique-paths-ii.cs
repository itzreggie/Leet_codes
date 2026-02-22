public class Solution {
    public int UniquePathsWithObstacles(int[][] obstacleGrid)
{
    int m = obstacleGrid.Length;
    int n = obstacleGrid[0].Length;

    // If the starting cell has an obstacle, no paths exist
    if (obstacleGrid[0][0] == 1)
        return 0;

    // Use the grid itself to store DP values
    obstacleGrid[0][0] = 1;

    // Fill the first column
    for (int i = 1; i < m; i++)
    {
        obstacleGrid[i][0] = (obstacleGrid[i][0] == 0 && obstacleGrid[i - 1][0] == 1) ? 1 : 0;
    }

    // Fill the first row
    for (int j = 1; j < n; j++)
    {
        obstacleGrid[0][j] = (obstacleGrid[0][j] == 0 && obstacleGrid[0][j - 1] == 1) ? 1 : 0;
    }

    // Fill the rest of the grid
    for (int i = 1; i < m; i++)
    {
        for (int j = 1; j < n; j++)
        {
            if (obstacleGrid[i][j] == 1)
            {
                obstacleGrid[i][j] = 0; // No paths through obstacles
            }
            else
            {
                obstacleGrid[i][j] = obstacleGrid[i - 1][j] + obstacleGrid[i][j - 1];
            }
        }
    }

    return obstacleGrid[m - 1][n - 1];
}

}