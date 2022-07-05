using System;

namespace Triangulator.Attempt.Four.Models
{
    [Serializable]
    public class VehiclePosition
    {
        /*
        PositionId: Int32
        VehicleRegistration: Null Terminated ASCII String
        Latitude: Float (4 byte floating-point number)
        Longitude: Float (4 byte floating-point number)
        RecordedTimeUTC: UInt64 (number of seconds since Epoch) 
        */

        public int PositionId { get; set; }

        public string VehicleRegistration { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public ulong RecordedTimeUTC { get; set; }

        public override string ToString()
        {
            return $"( {this.Latitude}; {this.Longitude} )";
        }
    }
}
