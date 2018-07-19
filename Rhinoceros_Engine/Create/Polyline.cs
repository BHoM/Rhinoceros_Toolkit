using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Engine.Rhinoceros
{
    public static partial class Create
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static Polyline RandomPolyline(Random random)
        {
            return new Polyline(RandomPoint3dList(random));
        }

        /***************************************************/

        public static Polyline RandomPolyline(int seed = 0)
        {
            return RandomPolyline(new Random(seed));
        }

        /***************************************************/

        public static PolylineCurve RandomPolylineCurve(Random random)
        {
            return new PolylineCurve(RandomPoint3dList(random));
        }

        /***************************************************/

        public static PolylineCurve RandomPolylineCurve(int seed = 0)
        {
            return RandomPolylineCurve(new Random(seed));
        }

        /***************************************************/
    }
}
