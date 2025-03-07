public class Solution {
    public int[] ClosestPrimes(int left, int right) {
        List<int> primes = GeneratePrimes(right);
        List<int> primesInRange = primes.Where(p => p >= left && p <= right).ToList();
        
        if (primesInRange.Count < 2) {
            return new int[] { -1, -1 };
        }
        
        int minDiff = int.MaxValue;
        int[] result = new int[2];
        
        for (int i = 1; i < primesInRange.Count; i++) {
            int diff = primesInRange[i] - primesInRange[i - 1];
            if (diff < minDiff) {
                minDiff = diff;
                result[0] = primesInRange[i - 1];
                result[1] = primesInRange[i];
            }
        }
        
        return result;
    }

    private List<int> GeneratePrimes(int max) {
        bool[] isPrime = new bool[max + 1];
        for (int i = 2; i <= max; i++) {
            isPrime[i] = true;
        }
        
        for (int i = 2; i * i <= max; i++) {
            if (isPrime[i]) {
                for (int j = i * i; j <= max; j += i) {
                    isPrime[j] = false;
                }
            }
        }
        
        List<int> primes = new List<int>();
        for (int i = 2; i <= max; i++) {
            if (isPrime[i]) {
                primes.Add(i);
            }
        }
        
        return primes;
    }
}
