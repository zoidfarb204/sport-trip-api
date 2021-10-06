using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Models
{
    public class Tour
    {
        public Stop Anchor { get; set; }

        public Tour(IEnumerable<Stop> stops, string anchor = null)
        {
            Anchor = string.IsNullOrEmpty(anchor) ? stops.First() : stops.FirstOrDefault(x=> x.Park.ParkName == anchor);
        }

        //the set of tours we can make with 2-opt out of this one
        public IEnumerable<Tour> GenerateMutations()
        {
            for (Stop stop = Anchor; stop.Next != Anchor; stop = stop.Next)
            {
                //skip the next one, since you can't swap with that
                Stop current = stop.Next.Next;
                while (current != Anchor)
                {
                    yield return CloneWithSwap(stop.Park, current.Park);
                    current = current.Next;
                }
            }
        }

        public Tour CloneWithSwap(Park firstPark, Park secondPark)
        {
            Stop firstFrom = null, secondFrom = null;
            var stops = UnconnectedClones();
            stops.Connect(true);

            foreach (Stop stop in stops)
            {
                if (stop.Park == firstPark) firstFrom = stop;

                if (stop.Park == secondPark) secondFrom = stop;
            }

            //the swap part
            var firstTo = firstFrom.Next;
            var secondTo = secondFrom.Next;

            //reverse all of the links between the swaps
            firstTo.CanGetTo()
                .TakeWhile(stop => stop != secondTo)
                .Reverse()
                .Connect(false);

            firstTo.Next = secondTo;
            firstFrom.Next = secondFrom;

            var tour = new Tour(stops);
            return tour;
        }

        public IList<Stop> UnconnectedClones()
        {
            return Cycle().Select(stop => stop.Clone()).ToList();
        }


        public double Cost()
        {
            return Cycle().Aggregate(
                0.0,
                (sum, stop) =>
                    sum + Stop.CalcDistance(stop, stop.Next));
        }


        public IEnumerable<Stop> Cycle()
        {
            return Anchor.CanGetTo();
        }


        public override string ToString()
        {
            string path = String.Join(
                "->",
                Cycle().Select(stop => stop.ToString()).ToArray());
            return String.Format("Cost: {0}, Path:{1}", Cost(), path);
        }
    }
}