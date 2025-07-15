public class Solution {
    public bool IsValid(string word) {
        if (word.Length < 3)
            return false;

        bool hasVowel = false;
        bool hasConsonant = false;

        foreach (char c in word) {
            if (!char.IsLetterOrDigit(c))
                return false;

            if (IsVowel(c))
                hasVowel = true;
            else if (char.IsLetter(c))
                hasConsonant = true;
        }

        return hasVowel && hasConsonant;
    }

    private bool IsVowel(char c) {
        char lower = char.ToLower(c);
        return lower == 'a' || lower == 'e' || lower == 'i' || lower == 'o' || lower == 'u';
    }
}
