﻿using System;

namespace BH.Engine.Rhinoceros
{
    public static partial class Create
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static object RandomGeometry(int seed = 0)
        {
            return RandomGeometry(new Random(seed));
        }

        /***************************************************/

        public static object RandomGeometry(Random random)
        {
            int throwDice = random.Next(15);
            switch (throwDice)
            {
                case 0:
                    return RandomArc(random);
                case 1:
                    return RandomArcCurve(random);
                case 2:
                    return RandomCircle(random);
                case 3:
                    return RandomLine(random);
                case 4:
                    return RandomLineCurve(random);
                case 5:
                    return RandomNurbsCurve(random);
                case 6:
                    return RandomPoint3d(random);
                case 7:
                    return RandomPoint3f(random);
                case 8:
                    return RandomControlPoint(random);
                case 9:
                    return RandomPolyCurve(random);
                case 10:
                    return RandomPolyline(random);
                case 11:
                    return RandomPolylineCurve(random);
                case 12:
                    return RandomVector3d(random);
                case 13:
                    return RandomPoint3f(random);
                case 14:
                    return RandomCurves(random);
                case 15:
                    return RandomPoint3dList(random);
                default:
                    return null;
            }
        }

        /***************************************************/

        public static object RandomGeometry(Type type, Random random)
        {
            switch(type.Name.ToLower())
            {
                case "arc":
                    return RandomArc(random);
                case "arccurve":
                    return RandomArcCurve(random);
                case "circle":
                    return RandomCircle(random);
                case "line":
                    return RandomLine(random);
                case "linecurve":
                    return RandomLineCurve(random);
                case "nurbscurve":
                case "nurbcurve":
                    return RandomNurbsCurve(random);
                case "point":
                case "point3d":
                    return RandomPoint3d(random);
                case "point3f":
                    return RandomPoint3f(random);
                case "controlpoint":
                    return RandomControlPoint(random);
                case "polycurve":
                    return RandomPolyCurve(random);
                case "polyline":
                    return RandomPolyline(random);
                case "polylinecurve":
                    return RandomPolylineCurve(random);
                case "vector":
                case "vector3d":
                    return RandomVector3d(random);
                case "vector3f":
                    return RandomPoint3f(random);
                case "curvelist":
                case "curves":
                    return RandomCurves(random);
                case "pointlist":
                case "point3dlist":
                case "points":
                    return RandomPoint3dList(random);
                default:
                    return null;
            }
        }
        
        /***************************************************/
    }
}
