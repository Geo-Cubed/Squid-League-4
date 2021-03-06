﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Common.Helpers
{
    public static class ListHelper
    {
        /// <summary>
        /// Splits up a list into multiple lists of size n
        /// </summary>
        /// <typeparam name="T">Type of the list items</typeparam>
        /// <param name="input">Single list</param>
        /// <param name="numPerList">Number of items per list</param>
        /// <returns>A list containing lists of length <paramref name="numPerList"/></returns>
        public static List<List<T>> SplitUpList<T>(IEnumerable<T> input, int numPerList)
        {
            var newList = new List<List<T>>();
            var counter = 0;
            foreach (var item in input)
            {
                if (counter % numPerList == 0)
                {
                    newList.Add(new List<T>() { item });
                }
                else
                {
                    newList.Last().Add(item);
                }

                ++counter;
            }

            return newList;
        }
    }
}
