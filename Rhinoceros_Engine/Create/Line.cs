using Rhino.Geometry;
using System;

namespace BH.Engine.Rhinoceros
{
    public static partial class Create
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static Line RandomLine(Random random)
        {
            return new Line(RandomPoint3d(random), RandomPoint3d(random));
        }

        /***************************************************/

        public static Line RandomLine(int seed = 0)
        {
            return RandomLine(new Random(seed));
        }

        /***************************************************/

        public static LineCurve RandomLineCurve(Random random)
        {
            return new LineCurve(RandomPoint3d(random), RandomPoint3d(random));
        }

        /***************************************************/

        public static LineCurve RandomLineCurve(int seed = 0)
        {
            return RandomLineCurve(new Random(seed));
        }

        /***************************************************/
    }
}
