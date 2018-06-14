using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leelanet.Engine
{
    public class MoveData : IComparable<MoveData>
    {
        public string coordinate;
        public int playouts;
        public double winrate;
        public int order;
        public List<string> variation;

        /// <summary>
        /// Parses a leelaz ponder output line </summary>
        /// <param name="line"> line of ponder output </param>
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public MoveData(String line) throws ArrayIndexOutOfBoundsException
        public MoveData(string line)
        {
            string[] data = line.Trim().Split(' ');

            // Todo: Proper tag parsing in case gtp protocol is extended(?)/changed
            coordinate = data[1];
            playouts = int.Parse(data[3]);
            winrate = int.Parse(data[5]) / 100.0;
            order = int.Parse(data[7]);

            variation = new List<string>(data);
            variation = variation.GetRange(9, variation.Count - 9);
        }

        public virtual int CompareTo(MoveData b)
        {
            return order - b.order;
        }
    }
}
