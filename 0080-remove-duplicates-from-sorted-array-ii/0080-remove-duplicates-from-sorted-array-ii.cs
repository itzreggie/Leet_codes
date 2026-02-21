public int RemoveDuplicates(int[] nums)
{
    if (nums.Length <= 2)
        return nums.Length;

    int k = 2; // Start from index 2 because first two elements are always allowed

    for (int i = 2; i < nums.Length; i++)
    {
        // Compare current element with the element two positions before
        if (nums[i] != nums[k - 2])
        {
            nums[k] = nums[i];
            k++;
        }
    }

    return k;
}
