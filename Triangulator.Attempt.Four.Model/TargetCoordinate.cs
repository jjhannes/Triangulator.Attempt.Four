namespace Triangulator.Attempt.Four.Models
{
    public class TargetCoordinate
    {
        /*
        Position 1
        Latitude = 34.544909
        Longitude = -102.100843
        */

        public int PositionId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public TargetCoordinate()
        {

        }

        public TargetCoordinate(double lat, double lon)
        {
            this.Latitude = lat;
            this.Longitude = lon;
        }

        public override string ToString()
        {
            return $"( {this.Latitude}; {this.Longitude} )";
        }
    }
}
