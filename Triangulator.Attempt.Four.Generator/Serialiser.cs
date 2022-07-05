using Salar.Bois;
using System;
using System.Collections.Generic;
using System.IO;
using Triangulator.Attempt.Four.Models;

namespace Triangulator.Attempt.Four.Generator
{
    public class Serialiser<T>
    {
        private readonly List<T> _items;

        public Serialiser(List<T> items)
        {
            this._items = items;
        }

        public bool Write(string path)
        {
            try
            {
                using (Stream fileStream = File.OpenWrite(path))
                {
                    BoisSerializer boisSerializer = new BoisSerializer();
                    boisSerializer.Serialize(this._items, fileStream);

                    fileStream.Flush();
                    fileStream.Close();
                    fileStream.Dispose();
                }

                return true;
            }
            catch (Exception error)
            {
                throw new Exception($"Could not perform bois serialisation: {error.Message}", error);
            }
        }
    }
}
