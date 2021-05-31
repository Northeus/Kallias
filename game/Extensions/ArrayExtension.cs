using System;
using System.Collections.Generic;

namespace game.Extensions
{
    public static class ArrayExtension
    {
        public static V[,] Map<T, V>(this T[,] array, Func<T, V> function)
        {
            var arrayOut = new V[array.GetLength(0), array.GetLength(1)];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    arrayOut[i, j] = function(array[i, j]);
                }
            }

            return arrayOut;
        }

        public static IEnumerable<IEnumerable<T>> GetAllRows<T>(this T[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                yield return array.GetRow(i);
            }
        }

        public static IEnumerable<T> GetRow<T>(this T[,] array, int row)
        {
            for (int i = 0; i < array.GetLength(1); i++)
            {
                yield return array[row, i];
            }
        }
    }
}