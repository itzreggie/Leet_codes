public class Solution {
    public int NumRabbits(int[] answers) {
        var count = new Dictionary<int, int>();
        foreach (var answer in answers) {
            if (!count.ContainsKey(answer)) {
                count[answer] = 0;
            }
            count[answer]++;
        }

        int result = 0;
        foreach (var key in count.Keys) {
            int groupSize = key + 1; // Total rabbits in a group for this answer
            int groups = (count[key] + groupSize - 1) / groupSize; // Calculate number of groups
            result += groups * groupSize;
        }

        return result;
    }
}
