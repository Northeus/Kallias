using System;
using System.Collections.Generic;

namespace game.Extensions
{
    /// <summary>
    /// Extension method over 2D arrays for nicer manipulation and casting.
    /// </summary>
    public static class ArrayExtension
    {
        /// <summary>
        /// Map function over 2D array and return result.
        /// </summary>
        /// <param name="array">2D array, which will be used for inputs.</param>
        /// <param name="function">Function, which should be mapped over array.</param>
        /// <typeparam name="TIn">Type of array we've got.</typeparam>
        /// <typeparam name="TOut">Type of outcome array.</typeparam>
        /// <returns>Result of mapping given function over input array.</returns>
        public static TOut[,] Map<TIn, TOut>(this TIn[,] array, Func<TIn, TOut> function)
        {
            var arrayOut = new TOut[array.GetLength(0), array.GetLength(1)];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    arrayOut[i, j] = function(array[i, j]);
                }
            }

            return arrayOut;
        }

        /// <summary>
        /// Transform 2D array into 2D IEnumerable.
        /// </summary>
        /// <param name="array">Array to be transformed.</param>
        /// <typeparam name="T">Type of array we want to transform.</typeparam>
        /// <returns>2D IEnumerable from given array.</returns>
        public static IEnumerable<IEnumerable<T>> GetAllRows<T>(this T[,] array)
        {
            for (var i = 0; i < array.GetLength(0); i++)
            {
                yield return array.GetRow(i);
            }
        }

        private static IEnumerable<T> GetRow<T>(this T[,] array, int row)
        {
            for (var i = 0; i < array.GetLength(1); i++)
            {
                yield return array[row, i];
            }
        }
    }
}