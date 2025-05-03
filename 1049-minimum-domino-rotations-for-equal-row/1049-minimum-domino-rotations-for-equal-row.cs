public class Solution {
    public int MinDominoRotations(int[] tops, int[] bottoms) {
        int rotations = GetMinRotations(tops, bottoms, tops[0]);
        if (rotations != -1 || tops[0] == bottoms[0]) return rotations;
        return GetMinRotations(tops, bottoms, bottoms[0]);
    }
    
    private int GetMinRotations(int[] tops, int[] bottoms, int target) {
        int topRotations = 0, bottomRotations = 0;
        
        for (int i = 0; i < tops.Length; i++) {
            if (tops[i] != target && bottoms[i] != target) return -1;
            if (tops[i] != target) topRotations++;
            if (bottoms[i] != target) bottomRotations++;
        }
        
        return Math.Min(topRotations, bottomRotations);
    }
}
