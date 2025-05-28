using System;
using System.Collections.Generic;

namespace Template
{
    public static class CollectionUtility
    {
        private static Random rng = new Random();

        public static void DerangedShuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            if (n < 2)
                throw new InvalidOperationException("List must contain at least two elements for a derangement.");

            T[] original = new T[n];
            list.CopyTo(original, 0);

            while (true)
            {
                // Fisher–Yates shuffle
                for (int i = n - 1; i > 0; i--)
                {
                    int j = rng.Next(i + 1);
                    (list[i], list[j]) = (list[j], list[i]);
                }

                bool isDeranged = true;
                for (int i = 0; i < n; i++)
                {
                    if (EqualityComparer<T>.Default.Equals(list[i], original[i]))
                    {
                        isDeranged = false;
                        break;
                    }
                }

                if (isDeranged)
                    break;
            }
        }
    }
}