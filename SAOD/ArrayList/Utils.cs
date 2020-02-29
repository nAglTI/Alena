using System;

namespace ArrayList
{
    public class Utils
    {
        public static int BinarySearch<T>(ArrayList<T> list, T value) where T : IComparable<T>
        {
            var first = 0;
            var last = list.Size - 1;

            var mid = last / 2;

            while (first <= last)
            {
                if (list[mid].CompareTo(value) > 0)
                {
                    last = mid - 1;
                }
                else if (list[mid].CompareTo(value) < 0)
                {
                    first = mid + 1;
                }
                else if (mid != 0 && list[mid - 1].CompareTo(list[mid]) == 0) // Этого достаточно для первого вхождения, O(logn)
                {
                    last = mid - 1;
                }
                else
                {
                    break;
                }

                mid = (first + last) / 2;
            }

            return list[mid].CompareTo(value) == 0 ? mid : -1;
        }
    }
}