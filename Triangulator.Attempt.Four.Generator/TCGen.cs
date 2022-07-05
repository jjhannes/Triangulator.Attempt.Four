using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triangulator.Attempt.Four.Models;

namespace Triangulator.Attempt.Four.Generator
{
    public class TCGen : VGenerator<TargetCoordinate>
    {
        public TCGen(int size) : base(size) { }

        public override List<TargetCoordinate> Generate()
        {
            List<TargetCoordinate> targetCoordinates = new List<TargetCoordinate>();

            for (int i = 0; i < this._size; i++)
            {
                targetCoordinates.Add(new TargetCoordinate
                {
                    PositionId = i,
                    Latitude = this.RandomLatitude(),
                    Longitude = this.RandomLongitude()
                });
            }

            return targetCoordinates;
        }
    }
}
