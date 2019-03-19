using System;
namespace SecretSanta.Core.Extensions
{
    public static class ArrayExtensions
    {
        private static Random _random = new Random();

        public static void Shuffle<T>(this T[] array)
        {
            if (array != null && array.Length > 1)
            {
                int n = array.Length;
                for (int i = 0; i < n; ++i)
                {
                    var j = i + _random.Next(n - i);
                    T t = array[i];
                    array[i] = array[j];
                    array[j] = t;
                }
            }
        }
    }
}
