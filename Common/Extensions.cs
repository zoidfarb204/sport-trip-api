using System;
using System.Collections.Generic;
using System.Linq;
using Common.Models;

namespace Common
{
    public static class Extensions
    {
        public static void Connect(this IEnumerable<Stop> stops, bool loop)
        {
            Stop prev = null, first = null;
            foreach (var stop in stops)
            {
                if (first == null) first = stop;
                if (prev != null) prev.Next = stop;
                prev = stop;
            }

            if (loop)
            {
                prev.Next = first;
            }
        }


        //T with the smallest func(T)
        public static T MinBy<T, TComparable>(
            this IEnumerable<T> xs,
            Func<T, TComparable> func)
            where TComparable : IComparable<TComparable>
        {
            return xs.DefaultIfEmpty().Aggregate(
                (maxSoFar, elem) =>
                    func(elem).CompareTo(func(maxSoFar)) > 0 ? maxSoFar : elem);
        }


        //return an ordered nearest neighbor set
        public static IEnumerable<Stop> NearestNeighbors(this IEnumerable<Stop> stops)
        {
            var stopsLeft = stops.ToList();
            for (var stop = stopsLeft.First();
                stop != null;
                stop = stopsLeft.MinBy(s => Stop.CalcDistance(stop, s)))
            {
                stopsLeft.Remove(stop);
                yield return stop;
            }
        }
    }
}