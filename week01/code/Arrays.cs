public static class Arrays
{

    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.
        var result = new List<double>();
        for (var i = 1; i < length + 1; i++)
        {

            var multi = number * i;
            Console.WriteLine(multi);
            result.Add(multi);

        }

        return result.ToArray(); // replace this return statement with your own
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {  
         // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Handle edge cases: null list, empty list, or zero rotation amount.
        if (data == null || data.Count == 0 || amount <= 0)
        {
            return;
        }

        int n = data.Count;
        // Calculate the effective rotation amount, as rotating by n is the same as no rotation.
        // Also handles cases where amount is greater than n.
        int effectiveAmount = amount % n;

        // If effectiveAmount is 0, no rotation is needed.
        if (effectiveAmount == 0)
        {
            return;
        }

          for (int i = 0; i < effectiveAmount; i++)
        {
            int lastElement = data[n - 1]; // Get the last element
            data.RemoveAt(n - 1); // Remove the last element
            data.Insert(0, lastElement); // Insert it at the beginning
        }

         return;

    }
}
