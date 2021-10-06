using System;
using System.Collections.Generic;
using System.Linq;
using Common.Models;

namespace Common
{
    public class Routing
    {
        private List<Stop> Stops { get; set; }
        private List<Park>Parks { get; set; }
        public Routing(List<Park> parks)
        {
            Stops = parks.Select(park => new Stop(park)).NearestNeighbors().ToList();
            Parks = parks;
            //create next pointers between them
            Stops.Connect(true);

            //wrap in a tour object
            
        }

        public Path GetRoute(string anchor = null)
        {
            var startingTour = new Tour(Stops, anchor);

            //the actual algorithm
            while (true)
            {
                Console.WriteLine(startingTour);
                var newTour = startingTour.GenerateMutations()
                    .MinBy(tour => tour.Cost());
                if (newTour.Cost() < startingTour.Cost()) startingTour = newTour;
                else break;
            }

            Path path = new Path();

            var tour = startingTour.Cycle();
            path.Distance = startingTour.Cost();
            foreach (var stop in tour)
            {
                path.Parks.Add(stop.Park);
            }
            
            return path;
        }
    }
}