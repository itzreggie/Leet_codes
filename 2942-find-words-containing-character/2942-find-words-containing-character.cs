
public class Solution 
{
    public IList<int> FindWordsContaining(string[] words, char x) 
    {
        var indices = new List<int>();
        
        // Loop through all words along with their index.
        for (int i = 0; i < words.Length; i++)
        {
            // Check if the word contains the character x.
            // You can use the Contains method by converting x to a string.
            if (words[i].Contains(x.ToString()))
            {
                indices.Add(i);
            }
        }
        
        return indices;
    }
}
