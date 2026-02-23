public class Solution {
    public int[][] GenerateMatrix(int n)
{
    int[][] matrix = new int[n][];
    for (int i = 0; i < n; i++)
        matrix[i] = new int[n];

    int left = 0, right = n - 1;
    int top = 0, bottom = n - 1;
    int num = 1;

    while (left <= right && top <= bottom)
    {
        // Fill top row
        for (int j = left; j <= right; j++)
            matrix[top][j] = num++;
        top++;

        // Fill right column
        for (int i = top; i <= bottom; i++)
            matrix[i][right] = num++;
        right--;

        // Fill bottom row
        if (top <= bottom)
        {
            for (int j = right; j >= left; j--)
                matrix[bottom][j] = num++;
            bottom--;
        }

        // Fill left column
        if (left <= right)
        {
            for (int i = bottom; i >= top; i--)
                matrix[i][left] = num++;
            left++;
        }
    }

    return matrix;
}

}