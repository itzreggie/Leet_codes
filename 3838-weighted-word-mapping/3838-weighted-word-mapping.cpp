class Solution{
public:
    string mapWordWeights(vector<string>& words, vector<int>& weights){
        string result;

        for( auto &w: words){
            long long sum= 0;

            for (char c: w){
                sum += weights[c - 'a'];
            }

            int r= sum% 26;
            
            char mapped = 'z' - r;

            result.push_back(mapped);
        }

        return result;
    }
};