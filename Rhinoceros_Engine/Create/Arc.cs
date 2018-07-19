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

        public static Arc RandomArc(Random random)
        {
            return new Arc(RandomPoint3d(random), random.NextDouble(), random.NextDouble());
        }

        /***************************************************/

        public static Arc RandomArc(int seed = 0)
        {
            return RandomArc(new Random(seed));
        }

        /***************************************************/

        public static ArcCurve RandomArcCurve(Random random)
        {
            return new ArcCurve(RandomArc(random));
        }

        /***************************************************/

        public static ArcCurve RandomArcCurve(int seed = 0)
        {
            return RandomArcCurve(new Random(seed));
        }

        /***************************************************/
    }
}
