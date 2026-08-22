class Solution{
public:
    bool checkDivisibility(int n){
        int x =n;
        int sum = 0;
        int prod = 1;

        while(x > 0){
            int d = x % 10;
            sum += d;
            prod *= d;
            x/=10;
        }

        int total = sum + prod;
        return (n % total == 0);
    }
};


