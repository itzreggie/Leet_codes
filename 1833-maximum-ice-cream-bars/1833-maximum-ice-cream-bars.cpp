class Solution {
public:
    int maxIceCream(vector<int>& costs, int coins) {
        int maxcost = 0;
        for (int c: costs) maxcost = max(maxcost, c);

        vector<int> freq(maxcost + 1, 0);
        for(int c: costs) freq[c]++;

        int count = 0;
        for (int cost = 1; cost<=maxcost; ++cost){
            if (freq[cost]== 0)continue;

            long long totalcost= 1LL * cost* freq[cost];

            if(coins >= totalcost){
                coins -= totalcost;
                count += freq[cost];
            }else{
                count += coins/cost;
                break;
            }
        }
        return count;
    }
};