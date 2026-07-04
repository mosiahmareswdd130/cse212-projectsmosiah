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
    // Plan:
    // 1. Create a new array of doubles with size 'length' to hold the result.
    // 2. Loop from i = 0 to i = length - 1 (one iteration per multiple we need).
    // 3. On each loop iteration, the multiple we want is 'number' times (i + 1),
    //    since when i = 0 we want the 1st multiple (number * 1), when i = 1 we
    //    want the 2nd multiple (number * 2), and so on.
    // 4. Store that calculated value into the array at index i.
    // 5. Once the loop is done, return the completed array.

    double[] result = new double[length];

    for (int i = 0; i < length; i++)
    {
        result[i] = number * (i + 1);
    }

    return result;
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
    // Plan:
    // 1. Figure out the split point in the list: the last 'amount' elements are the
    //    ones that need to move to the front. That split point is
    //    splitIndex = data.Count - amount.
    // 2. Use GetRange(splitIndex, amount) to copy out those last 'amount' elements
    //    into a separate list called 'tail'.
    // 3. RemoveRange(splitIndex, amount) to delete those elements from 'data',
    //    leaving just the "head" part of the list behind.
    // 4. InsertRange(0, tail) to place the 'tail' elements back into 'data'
    //    at the very beginning, in front of the head.
    // 5. Because List is passed by reference, modifying 'data' directly means we
    //    don't need to return anything - the caller's list is already rotated.

    int splitIndex = data.Count - amount;

    List<int> tail = data.GetRange(splitIndex, amount);
    data.RemoveRange(splitIndex, amount);
    data.InsertRange(0, tail);
    }
}
