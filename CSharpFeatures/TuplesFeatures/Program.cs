using System;
using System.Collections.Generic;

namespace TuplesFeatures
{
    class Program
    {
        static void Main(string[] args)
        {
            // The first method of using tuples
            List<int>list=new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            var result=Range(list);

            Console.WriteLine("first:");
            Console.WriteLine($"MAX:{result.Max};MIN:{result.Min}");
            var result2=Range2(list);
            Console.WriteLine("second:");
            Console.WriteLine($"MAX:{result2.Item1};MIN:{result2.Item2}");
        }
        
        /// <summary>
        /// The first method of using tuples
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        private static (int Max,int Min) Range(IEnumerable<int> numbers)
        {
            int min=int.MaxValue;
            int max=int.MinValue;
            foreach (var item in numbers)
            {
                min = (item < min) ? item : min;
                max = (item > max) ? item : max;
            }
            return (max,min);
        }

        /// <summary>
        /// the second method of using tuples
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        private static (int,int) Range2(IEnumerable<int> numbers)
        {
            int min=int.MaxValue;
            int max=int.MinValue;
            foreach (var item in numbers)
            {
                min = (item < min) ? item : min;
                max = (item > max) ? item : max;
            }
            return (max,min);
        }
    }
}
