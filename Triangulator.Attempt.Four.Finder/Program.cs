using Salar.Bois;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Triangulator.Attempt.Four.Models;

namespace Triangulator.Attempt.Four.Finder
{
    class Program
    {
        private const string _argFormat = "{batchSize:int} {targetFile:string} {positionFile:string}";
        
        static void Main(string[] args)
        {
            int batchSize = 0;
            string targetPath = string.Empty;
            string positionsPath = string.Empty;

            if (args.Length < 3)
                throw new ArgumentException($"Invalid number of arguments (2): {_argFormat}");

            try
            {
                batchSize = int.Parse(args[0]);
                targetPath = args[1];
                positionsPath = args[2];
            }
            catch (Exception error)
            {
                throw new ArgumentException($"Invalid argument format: {_argFormat}", error);
            }

            Deserialiser<TargetCoordinate> targetDes = new Deserialiser<TargetCoordinate>(targetPath);
            Deserialiser<VehiclePosition> positionDes = new Deserialiser<VehiclePosition>(positionsPath);

            List<TargetCoordinate> targetData = targetDes.Deserialise();
            List<VehiclePosition> positionsData = positionDes.Deserialise();

            Console.WriteLine($"{targetData.Count} targets found @ {targetPath}!");
            Console.WriteLine($"{positionsData.Count} positions found @ {positionsPath}!");
            Console.WriteLine();

            if (positionsData != null)
            {
                Triangulate(batchSize, targetData, positionsData);
            }
            else
                Console.WriteLine($"Could not read file at {targetPath}");

            Console.ReadKey();
        }

        static void Triangulate(int batchSize, List<TargetCoordinate> targets, List<VehiclePosition> data)
        {
            Stopwatch totalTime = Stopwatch.StartNew();
            Stopwatch batchTime = new Stopwatch();

            foreach (TargetCoordinate target in targets)
            {
                batchTime.Reset();
                batchTime.Start();

                VehiclePosition nearestPosition = BatchTriangulate(batchSize, target, data);

                batchTime.Stop();

                Console.WriteLine($"Triangulation batch time: {batchTime.ElapsedMilliseconds / 1000d} s");
                Console.WriteLine($"Nearest position to {target} is {nearestPosition}");
                Console.WriteLine();
            }

            totalTime.Stop();

            Console.WriteLine($"Total triangulation time: {totalTime.ElapsedMilliseconds / 1000d} s");
        }

        static VehiclePosition BatchTriangulate(int batchSize, TargetCoordinate target, List<VehiclePosition> items)
        {
            int count = 0;
            double closestDistance = double.MaxValue;
            VehiclePosition nearestPosition = null;
            List<VehiclePosition> batch = items.Take(batchSize).ToList();
            List<VehiclePosition> remaining = items.Skip(batchSize).ToList();

            do
            {
#if DEBUG
                Console.WriteLine($"Comparing batch to: {target}");
#endif

                Parallel.For(0, batch.Count, (i) =>
                {
                    VehiclePosition compare = batch[i];

                    double latDelta = target.Latitude - compare.Latitude;
                    double lonDelta = target.Longitude - compare.Longitude;

                    double distance = GetDistance(target, compare);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        nearestPosition = compare;
                    }

#if DEBUG
                    Console.WriteLine($"Processing item [{i}]: {batch[i]} @ [{Math.Round(distance / 1000d, 4)} km]");
#endif
                });

                batch = remaining.Take(batchSize).ToList();
                remaining = remaining.Skip(batchSize).ToList();

                count++;
            }
            while (batch.Any());

            return nearestPosition;
        }

        static double GetDistance(TargetCoordinate target, VehiclePosition compare)
        {
            // Source: http://www.movable-type.co.uk/scripts/latlong.html
            double R = 6371e3; // metres
            double φ1 = target.Latitude * Math.PI / 180; // φ, λ in radians
            double φ2 = compare.Latitude * Math.PI / 180;
            double Δφ = (compare.Latitude - target.Latitude) * Math.PI / 180;
            double Δλ = (compare.Longitude - target.Longitude) * Math.PI / 180;

            double a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
                      Math.Cos(φ1) * Math.Cos(φ2) *
                      Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double d = R * c; // in metres

            return d;
        }
    }
}
