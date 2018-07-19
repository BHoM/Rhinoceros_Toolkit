using Rhino.Geometry;
using System;

namespace BH.Engine.Rhinoceros
{
    public static partial class Create
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static PolyCurve RandomPolyCurve(Random random)
        {
            PolyCurve polycurve = new PolyCurve();
            polycurve.Append(RandomArc(random));
            polycurve.Append(RandomLine(random));
            polycurve.Append(RandomNurbsCurve(random));
            polycurve.Append(RandomPolylineCurve(random));
            return polycurve;
        }

        /***************************************************/

        public static PolyCurve RandomPolyCurve(int seed = 0)
        {
            return RandomPolyCurve(new Random(seed));
        }

        /***************************************************/
    }
}
