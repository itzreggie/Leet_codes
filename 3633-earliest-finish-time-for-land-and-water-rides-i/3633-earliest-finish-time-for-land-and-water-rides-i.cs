public class Solution {
    public int EarliestFinishTime(int[] landStartTime, int[] landDuration,
                                  int[] waterStartTime, int[] waterDuration) 
    {
        int n = landStartTime.Length;
        int m = waterStartTime.Length;

        int best = int.MaxValue;

        // Try all land → water combinations
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < m; j++) {

                // Land first
                int landStart = landStartTime[i];
                int landEnd = landStart + landDuration[i];

                int waterStart = Math.Max(landEnd, waterStartTime[j]);
                int finishLW = waterStart + waterDuration[j];

                best = Math.Min(best, finishLW);

                // Water first
                int waterStart2 = waterStartTime[j];
                int waterEnd2 = waterStart2 + waterDuration[j];

                int landStart2 = Math.Max(waterEnd2, landStartTime[i]);
                int finishWL = landStart2 + landDuration[i];

                best = Math.Min(best, finishWL);
            }
        }

        return best;
    }
}
