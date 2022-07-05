using System;
using System.Collections.Generic;
using Triangulator.Attempt.Four.Models;

namespace Triangulator.Attempt.Four.Generator
{
    internal class Program
    {
        private const string _argFormat = "{targetCount:int} {targetFile:string} {positionCount:int} {positionFile:string}";

        static void Main(string[] args)
        {
            Console.WriteLine("Initialising...");

            if (args.Length < 4)
                throw new ArgumentException($"Invalid number of arguments (4): {_argFormat}");

            int targetCount = 0;
            int positionCount = 0;
            string targetFile = string.Empty;
            string positionFile = string.Empty;

            try
            {
                targetCount = int.Parse(args[0]);
                targetFile = args[1];

                positionCount = int.Parse(args[2]);
                positionFile = args[3];
            }
            catch (Exception error)
            {
                throw new ArgumentException($"Invalid argument format: {_argFormat}", error);
            }

            try
            {
                GenerateTargetCoordinates(targetCount, targetFile);
                GenerateVehiclePositions(positionCount, positionFile);
            }
            catch (Exception error)
            {
                throw new Exception($"Could not generate output: {error.Message}", error);
            }
        }

        static void GenerateTargetCoordinates(int targetCount, string targetFile)
        {
            Console.WriteLine($"Generating {targetCount} targets...");

            VGenerator<TargetCoordinate> gen = new TCGen(targetCount);

            List<TargetCoordinate> items = gen.Generate();

            Console.WriteLine($"Serialising {targetCount} targets to {targetFile}...");

            Serialiser<TargetCoordinate> ser = new Serialiser<TargetCoordinate>(items);

            ser.Write(targetFile);
        }

        static void GenerateVehiclePositions(int positionCount, string positionFile)
        {
            Console.WriteLine($"Generating {positionCount} positions...");

            VGenerator<VehiclePosition> gen = new VPGen(positionCount);

            List<VehiclePosition> items = gen.Generate();

            Console.WriteLine($"Serialising {positionCount} positions to {positionFile}...");

            Serialiser<VehiclePosition> ser = new Serialiser<VehiclePosition>(items);

            ser.Write(positionFile);
        }

        //static void GenerateFile<T, Y>(int count, string file) where Y : new()
        //{
        //    Console.WriteLine($"Generating {count} items...");

        //    VGenerator<T> gen = new Y(count);

        //    List<T> items = gen.Generate();

        //    Console.WriteLine($"Serialising {count} items to {file}...");

        //    Serialiser ser = new Serialiser(items);

        //    ser.Write(file);
        //}    
    }
}
