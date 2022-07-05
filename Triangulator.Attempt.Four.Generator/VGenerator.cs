using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangulator.Attempt.Four.Generator
{
    public abstract class VGenerator<T>
    {
        protected readonly int _size;
        protected Random _random;

        public VGenerator(int size)
        {
            this._random = new Random();
            this._size = size;
        }

        public abstract List<T> Generate();

        protected double RandomLatitude()
        {
            return this.RandomDoubleInRange(90);
        }

        protected double RandomLongitude()
        {
            return this.RandomDoubleInRange(180);
        }

        protected double RandomDoubleInRange(int range)
        {
            int r = this._random.Next(-range, range);
            double factor = this._random.NextDouble();

            return r * factor;
        }
    }
}
