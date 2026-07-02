using System.Collections.Generic;

public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.
    /// For example, MultiplesOf(3, 5) will result in {3, 6, 9, 12, 15}.
    /// </summary>
    /// <param name="number">The starting number (also the multiple used to generate the rest)</param>
    /// <param name="length">The number of multiples to produce</param>
    /// <returns>double array of the multiples of 'number'</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // Plan:
        // 1. Create a new double array with a fixed size of 'length'. Since arrays are
        //    fixed-size, we can allocate all the space we need right away.
        // 2. Loop from 1 to 'length' (inclusive). The loop variable represents which
        //    multiple we are on (1st multiple, 2nd multiple, etc).
        // 3. On each iteration, calculate the multiple by multiplying 'number' by the
        //    current loop value, and store it in the array. Since arrays are zero-indexed
        //    but our loop starts at 1, we store at position (loopValue - 1).
        // 4. After the loop finishes, the array is completely filled, so return it.

        double[] multiples = new double[length];

        for (int i = 1; i <= length; i++)
        {
            multiples[i - 1] = number * i;
        }

        return multiples;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'. For example, if the data is
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 5, then the list after the function
    /// runs should be List<int>{5, 6, 7, 8, 9, 1, 2, 3, 4}. The value of amount will be in the
    /// range of 1 and data.Count, inclusive.
    ///
    /// This is done in-place - the 'data' list passed in is modified directly. Nothing is returned.
    /// </summary>
    /// <param name="data">The list of integers to rotate</param>
    /// <param name="amount">How many places to rotate to the right</param>
    public static void RotateListRight(List<int> data, int amount)
    {
        // Plan:
        // 1. The last 'amount' items in the list need to move to the front, and everything
        //    else needs to shift back to make room for them - but keep their same relative order.
        // 2. Figure out the index where the "last amount items" begin. That index is
        //    (data.Count - amount). Everything from that index to the end is the chunk
        //    that moves to the front.
        // 3. Use GetRange to pull out that ending chunk (the part that moves to the front)
        //    into its own list, using GetRange(splitIndex, amount).
        // 4. Use GetRange to pull out the beginning chunk (the part that stays in the same
        //    relative order but now goes at the end), using GetRange(0, splitIndex).
        // 5. Clear the original list so it is empty.
        // 6. Add the "moved to front" chunk first, then add the "beginning" chunk after it,
        //    using AddRange for both. This rebuilds 'data' in its new rotated order.

        int splitIndex = data.Count - amount;

        List<int> movedToFront = data.GetRange(splitIndex, amount);
        List<int> stayingPart = data.GetRange(0, splitIndex);

        data.Clear();
        data.AddRange(movedToFront);
        data.AddRange(stayingPart);
    }
}
