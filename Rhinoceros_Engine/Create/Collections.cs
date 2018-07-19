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

        public static List<Curve> RandomCurves(Random random)
        {
            return new List<Curve>()
            {
                RandomArcCurve(random),
                RandomLineCurve(random),
                RandomNurbsCurve(random),
                RandomPolylineCurve(random),
                RandomPolyCurve(random),
            };
        }

        /***************************************************/

        public static List<Point3d> RandomPoint3dList(Random random)
        {
            List<Point3d> points = new List<Point3d>();
            for (int i = 0; i < 10; i++)
                points.Add(RandomPoint3d(random));
            return points;
        }

        /***************************************************/

        public static List<Curve> RandomCurves(int  seed = 0)
        {
            return RandomCurves(new Random(seed));
        }

        /***************************************************/

        public static List<Point3d> RandomPoint3dList(int seed = 0)
        {
            return RandomPoint3dList(new Random(seed));
        }

        /***************************************************/
    }
}
