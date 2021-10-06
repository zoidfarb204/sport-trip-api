using System;

namespace Common
{
    public class Distance
    {
        public static double getDistanceBetweenPoints(double lat1, double lng1, double lat2, double lng2, bool kilometers = false)
        {
            var r = 6371;
            var dLat = deg2Rad(lat2 - lat1);
            var dLng = deg2Rad(lng2-lng1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(deg2Rad(lat1)) * Math.Cos(deg2Rad(lat2)) *
                Math.Sin(dLng / 2) * Math.Sin(dLng / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = r * c;
            if (kilometers)
            {
                return d;
            }
            return d / 1.60934;
        }

        private static double deg2Rad(double deg)
        {
            return deg * (Math.PI / 180);
        }
    }
}