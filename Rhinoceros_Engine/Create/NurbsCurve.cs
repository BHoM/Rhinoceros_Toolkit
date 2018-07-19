using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace BH.Engine.Rhinoceros
{
    public static partial class Create
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static NurbsCurve RandomNurbsCurve(Random random)
        {
            List<Point3d> points = RandomPoint3dList(random);
            return NurbsCurve.Create(false, random.Next(3, 6), points);
        }

        /***************************************************/

        public static NurbsCurve RandomNurbsCurve(int seed = 0)
        {
            return RandomNurbsCurve(new Random(seed));
        }

        /***************************************************/
    }
}
