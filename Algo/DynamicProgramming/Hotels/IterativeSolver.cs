using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotels
{
    internal sealed class IterativeSolver : ISolver
    {
        private readonly bool _isTroubleshooting;

        public IterativeSolver(bool isTroubleshooting)
        {
            _isTroubleshooting = isTroubleshooting;
        }

        int ISolver.FindSolution(int[] hotels, int idealDistance)
        {
            int hotelIndex = -1;
            ICollection<int> penalties = new List<int>();

            for (int location = 0, day = 0; location < hotels[hotels.Length - 1]; ++day)
            {
                int idealLocation = location + idealDistance;
                int nextHotelIndex = Array.BinarySearch(
                    hotels, 
                    hotelIndex + 1, 
                    hotels.Length - hotelIndex - 1,
                    idealLocation);
                int penalty = 0;

                if (nextHotelIndex >= 0)
                {
                    // exact match with no penalty
                    hotelIndex = nextHotelIndex;
                }
                else
                {
                    // choose the hotel with smaller penalty
                    nextHotelIndex = ~nextHotelIndex;

                    if (nextHotelIndex >= hotels.Length || nextHotelIndex == hotelIndex + 1)
                    {
                        // just add distance to the penalty
                        hotelIndex = Math.Min(nextHotelIndex, hotels.Length - 1);
                        penalty = GetPenalty(idealLocation, hotels[hotelIndex]);
                    }
                    else
                    {
                        // choose a hotel with a lower penalty
                        int closerPenalty = GetPenalty(idealLocation, hotels[nextHotelIndex - 1]);
                        int fartherPenalty = GetPenalty(idealLocation, hotels[nextHotelIndex]);

                        if (fartherPenalty <= closerPenalty)
                        {
                            penalty = fartherPenalty;
                            hotelIndex = nextHotelIndex;
                        }
                        else
                        {
                            penalty = closerPenalty;
                            hotelIndex = nextHotelIndex - 1;
                        }
                    }
                }

                if (_isTroubleshooting)
                {
                    Console.WriteLine($"Day {day}: staying at hotel #{hotelIndex}, {hotels[hotelIndex]} mi from the start");
                    Console.WriteLine($">>>>Distance is {hotels[hotelIndex] - idealLocation}; incurred penalty is {penalty}.");
                }

                location = hotels[hotelIndex];
                penalties.Add(penalty);
            }

            return penalties.Sum();
        }

        private static int GetPenalty(int startLocation, int endLocation)
        {
            int distance = endLocation - startLocation;
            return distance * distance;
        }
    }
}
        