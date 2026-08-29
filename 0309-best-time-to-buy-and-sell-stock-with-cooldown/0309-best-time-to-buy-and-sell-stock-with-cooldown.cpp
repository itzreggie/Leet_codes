class Solution {
public:
    int maxProfit(vector<int>& prices) {
        int n = prices.size();
        if (n <= 1) return 0;

        int hold = -prices[0];   // we buy on day 0
        int sold = 0;            // profit after selling
        int rest = 0;            // cooldown or idle

        for (int i = 1; i < n; i++) {
            int prev_hold = hold;
            int prev_sold = sold;
            int prev_rest = rest;

            hold = max(prev_hold, prev_rest - prices[i]); // buy or keep holding
            sold = prev_hold + prices[i];                 // sell today
            rest = max(prev_rest, prev_sold);             // cooldown or stay resting
        }

        return max(sold, rest); // cannot end in "hold"
    }
};

