using Salar.Bois;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangulator.Attempt.Four.Finder
{
    internal class Deserialiser<T>
    {
        private readonly string _path;

        public Deserialiser(string path)
        {
            this._path = path;
        }

        public List<T> Deserialise()
        {
            try
            {
                using (FileStream fileStream = File.Open(this._path, FileMode.Open))
                {
                    BoisSerializer boisSerializer = new BoisSerializer();

                    List<T> vehiclePositions = boisSerializer.Deserialize<List<T>>(fileStream);

                    fileStream.Flush();
                    fileStream.Close();
                    fileStream.Dispose();

                    return vehiclePositions;
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Could not perform bois deserialisation: {0}", error.Message);

                return null;
            }
        }
    }
}
