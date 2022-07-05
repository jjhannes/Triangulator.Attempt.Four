using System;
using System.Collections.Generic;
using System.Linq;
using Triangulator.Attempt.Four.Models;

namespace Triangulator.Attempt.Four.Generator
{
    public class VPGen : VGenerator<VehiclePosition>
    {
        public VPGen(int size) : base(size) { }

        public override List<VehiclePosition> Generate()
        {
            List<VehiclePosition> vehiclePositions = new List<VehiclePosition>();

            for (int i = 0; i < this._size; i++)
            {
                vehiclePositions.Add(new VehiclePosition
                {
                    PositionId = i,
                    Latitude = this.RandomLatitude(),
                    Longitude = this.RandomLongitude(),
                    VehicleRegistration = this.RandomRegistration(),
                    RecordedTimeUTC = this.RandomTime()
                });
            }

            return vehiclePositions;
        }

        private string RandomRegistration()
        {
            const string alphas = "BCDFGHJKLMNPQRSTVWXYZ";
            const string numerabls = "0123456789";
            string[] provs = new string[] { "L", "MP", "NW", "FS", "KN", "GP", "EC", "WC", "NC" };

            string part1 = this.RandomStringPart(alphas, 2);
            string part2 = this.RandomStringPart(numerabls, 2);
            string part3 = this.RandomStringPart(alphas, 2);
            string part4 = provs[this._random.Next(provs.Length)];

            return $"{part1}{part2}{part3}{part4}";
        }

        private string RandomStringPart(string set, int length)
        {
            return new string(Enumerable.Repeat(set, length).Select(s => s[this._random.Next(s.Length)]).ToArray());
        }

        private ulong RandomTime()
        {
            return (ulong)DateTime.Now.Ticks;
        }
    }
}
