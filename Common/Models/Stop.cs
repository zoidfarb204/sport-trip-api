using System.Collections.Generic;

namespace Common.Models
{
    public class Stop
    {
        public Stop Next { get; set; }
        public Park Park { get; set; }

        public Stop(Park park)
        {
            Park = park;
        }

        public Stop Clone()
        {
            return new Stop(Park);
        }

        public static double CalcDistance(Stop first, Stop second)
        {
            return Distance.getDistanceBetweenPoints(first.Park.Lat, first.Park.Lng, second.Park.Lat, second.Park.Lng);
        }

        public IEnumerable<Stop> CanGetTo()
        {
            var current = this;
            while (true)
            {
                yield return current;
                current = current.Next;
                if (current == this) break;
            }
        }

        public override bool Equals(object obj)
        {
            return Park == ((Stop)obj).Park;
        }

        public override int GetHashCode()
        {
            return Park.GetHashCode();
        }

        public override string ToString()
        {
            return Park.ParkName.ToString();
        }
    }
}