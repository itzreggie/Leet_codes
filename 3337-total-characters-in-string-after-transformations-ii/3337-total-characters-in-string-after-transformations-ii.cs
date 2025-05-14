using System;
using System.Collections.Generic;

public class Solution {
    const long MOD = 1000000007;
    const int DIM = 26;
    
    public int LengthAfterTransformations(string s, int t, IList<int> nums) {
        // Build initial frequency vector for s.
        long[] freq = new long[DIM];
        foreach (char c in s) {
            freq[c - 'a']++;
        }
        
        // Build transformation matrix T.
        // For each letter with index i, it transforms into a string of length nums[i],
        // producing letters ((i + 1) mod 26) to ((i + nums[i]) mod 26).
        long[,] T = new long[DIM, DIM];
        for (int src = 0; src < DIM; src++) {
            int count = nums[src];
            for (int j = 1; j <= count; j++) {
                int dest = (src + j) % DIM;
                T[dest, src] = (T[dest, src] + 1) % MOD;
            }
        }
        
        // Compute T raised to the t-th power.
        long[,] Tt = MatrixPower(T, t);
        
        // Multiply Tt * freq vector to get the resulting frequency vector.
        long[] newFreq = new long[DIM];
        for (int i = 0; i < DIM; i++) {
            long sum = 0;
            for (int j = 0; j < DIM; j++) {
                sum = (sum + Tt[i, j] * freq[j]) % MOD;
            }
            newFreq[i] = sum;
        }
        
        // The length of the resulting string is the sum of newFreq.
        long result = 0;
        for (int i = 0; i < DIM; i++) {
            result = (result + newFreq[i]) % MOD;
        }
        
        return (int)result;
    }
    
    // Standard matrix multiplication mod MOD for two 26x26 matrices.
    private long[,] MatrixMultiply(long[,] A, long[,] B) {
        long[,] C = new long[DIM, DIM];
        for (int i = 0; i < DIM; i++) {
            for (int j = 0; j < DIM; j++) {
                long sum = 0;
                for (int k = 0; k < DIM; k++) {
                    sum = (sum + A[i, k] * B[k, j]) % MOD;
                }
                C[i, j] = sum;
            }
        }
        return C;
    }
    
    // Matrix exponentiation using binary exponentiation.
    private long[,] MatrixPower(long[,] baseMatrix, int exp) {
        long[,] result = new long[DIM, DIM];
        // Initialize result as the identity matrix.
        for (int i = 0; i < DIM; i++) {
            result[i, i] = 1;
        }
        while (exp > 0) {
            if ((exp & 1) == 1) {
                result = MatrixMultiply(result, baseMatrix);
            }
            baseMatrix = MatrixMultiply(baseMatrix, baseMatrix);
            exp >>= 1;
        }
        return result;
    }
}
