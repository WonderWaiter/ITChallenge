using System;
using System.Collections.Generic;
using System.Text;

namespace ITCLibrary
{
    public static class Rusia
    {        
        public static string Work()
        {
            Dictionary<int, Tuple<int, int>> routers = new Dictionary<int, Tuple<int, int>>()
            {
                { 1, new Tuple<int, int>( 9106, 137 ) },
                { 2, new Tuple<int, int>( 5339, 852 ) },
                { 3, new Tuple<int, int>( 3726, 3952 ) },
                { 4, new Tuple<int, int>( 994, 210 ) },
                { 5, new Tuple<int, int>( 5304, 1471 ) },
                { 6, new Tuple<int, int>( 5990, 3581 ) },
                { 7, new Tuple<int, int>( 3266, 4392 ) },
                { 8, new Tuple<int, int>( 5290, 439 ) },
                { 9, new Tuple<int, int>( 9299, 296 ) },
                { 10, new Tuple<int, int>( 9437, 479 ) }
            };
            List<int> queries = new List<int>()
            {
                7, 6, 8, 1, 6, 7, 7, 3, 7, 6
            };
            StringBuilder sb = new StringBuilder();
            foreach (int q in queries)
            {
                int p = routers[q].Item1;
                int r = routers[q].Item2;
                int x1 = p - r;
                int x2 = p + r;
                int count = -1;
                foreach (int i in routers.Keys)
                {
                    int pi = routers[i].Item1;
                    int ri = routers[i].Item2;
                    int xi1 = pi - ri;
                    int xi2 = pi + ri;
                    if (xi1 >= x1 && xi2 <= x2)
                    {
                        count++;
                    }
                }
                sb.Append(count);
            }
            return sb.ToString();
        }
    }
}
