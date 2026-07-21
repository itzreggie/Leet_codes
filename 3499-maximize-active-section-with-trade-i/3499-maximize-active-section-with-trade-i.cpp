class Solution {
public:
    int maxActiveSectionsAfterTrade(string s) {
        int n = s.size();

        // Augment: t = '1' + s + '1'
        string t = "1" + s + "1";
        int m = t.size();

        // Count original active sections in s
        int totalOnes = 0;
        for (char c : s) {
            if (c == '1') totalOnes++;
        }

        // Compress t into blocks of same character: types[i] in {0,1}, lens[i] = length
        vector<int> types;
        vector<int> lens;

        int i = 0;
        while (i < m) {
            int j = i;
            while (j < m && t[j] == t[i]) j++;
            types.push_back(t[i] - '0');
            lens.push_back(j - i);
            i = j;
        }

        int k = types.size();
        int bestGain = 0;

        // For each 1-block surrounded by 0-blocks, compute gain = leftZeros + rightZeros
        for (int idx = 0; idx < k; ++idx) {
            if (types[idx] == 1 && idx > 0 && idx + 1 < k &&
                types[idx - 1] == 0 && types[idx + 1] == 0) {
                int gain = lens[idx - 1] + lens[idx + 1];
                if (gain > bestGain) bestGain = gain;
            }
        }

        // If no valid trade, just return original ones
        return totalOnes + bestGain;
    }
};
