using System;
using RHG = Rhino.Geometry;
using BHG = BH.oM.Geometry;
using System.Collections.Generic;

namespace BH.Engine.Rhinoceros
{
    public static partial class Create
    {
        /***************************************************/
        /**** Public Methods  - Vectors                 ****/
        /***************************************************/

        public static bool IsEqual(this BHG.Point bhPoint, RHG.Point3d rhPoint)
        {
            if (bhPoint == null & rhPoint == default(RHG.Point3d))
                return true;
            return (bhPoint.X == rhPoint.X && bhPoint.Y == rhPoint.Y && bhPoint.Z == rhPoint.Z);
        }

        /***************************************************/

        public static bool IsEqual(this BHG.Point bhPoint, RHG.Point3f rhPoint)
        {
            if (bhPoint == null & rhPoint == default(RHG.Point3f))
                return true;
            return (bhPoint.X == rhPoint.X && bhPoint.Y == rhPoint.Y && bhPoint.Z == rhPoint.Z);
        }

        /***************************************************/

        public static bool IsEqual(this BHG.Point bhPoint, RHG.ControlPoint rhPoint)
        {
            if (bhPoint == null)    // RHG.ControlPoint is a non-nullable type and does not have an == operator override
                return false;       // because RHG.ControlPoint cannot ever be null
            return (bhPoint.X == rhPoint.Location.X & bhPoint.Y == rhPoint.Location.Y & bhPoint.Z == rhPoint.Location.Z);
        }

        /***************************************************/

        public static bool IsEqual(this BHG.Vector bhVector, RHG.Vector3d rhVector)
        {
            if (bhVector == null & rhVector == default(RHG.Vector3d))
                return true;
            return (bhVector.X == rhVector.X && bhVector.Y == rhVector.Y && bhVector.Z == rhVector.Z);
        }

        /***************************************************/

        public static bool IsEqual(this BHG.Vector bhVector, RHG.Vector3f rhVector)
        {
            if (bhVector == null & rhVector == default(RHG.Vector3f))
                return true;
            return (bhVector.X == rhVector.X && bhVector.Y == rhVector.Y && bhVector.Z == rhVector.Z);
        }


        /***************************************************/
        /**** Public Methods  - Curves                  ****/
        /***************************************************/

        public static bool IsEqual(this BHG.Arc bhArc, RHG.Arc rhArc)
        {
            if (bhArc == null & rhArc == default(RHG.Arc))
                return true;
            return (bhArc.CoordinateSystem == rhArc.Plane.ToBHoM() & bhArc.Radius == rhArc.Radius &
                    bhArc.StartAngle == rhArc.StartAngle & bhArc.EndAngle == rhArc.EndAngle);
        }

        /***************************************************/

        public static bool IsEqual(this BHG.Arc bhArc, RHG.ArcCurve rhArc)
        {
            if (bhArc == null & rhArc == null)
                return true;

            RHG.Arc innerArc;
            rhArc.TryGetArc(out innerArc);
            return (bhArc.CoordinateSystem == innerArc.Plane.ToBHoM() & bhArc.Radius == innerArc.Radius &
                    bhArc.StartAngle == innerArc.StartAngle & bhArc.EndAngle == innerArc.EndAngle);
        }

        /***************************************************/

        public static bool IsEqual(this BHG.Circle bhCircle, RHG.Circle rhCircle)
        {
            if (bhCircle == null)    // RHG.Circle is a non-nullable type and does not have an == operator override
                return false;        // because RHG.Circle cannot ever be null
            return (bhCircle.Centre.IsEqual(rhCircle.Center) && bhCircle.Normal.IsEqual(rhCircle.Normal) && bhCircle.Radius == rhCircle.Radius);
        }

        /***************************************************/

        public static bool IsEqual(this BHG.Line bhLine, RHG.Line rhLine)
        {
            if (bhLine == null & rhLine == default(RHG.Line))
                return true;
            return (bhLine.Start.IsEqual(rhLine.PointAt(0)) && bhLine.End.IsEqual(rhLine.PointAt(1)));
        }

        /***************************************************/

        public static bool IsEqual(this BHG.Line bhLine, RHG.LineCurve rhLine)
        {
            if (bhLine == null & rhLine == null)
                return true;
            return (bhLine.Start.IsEqual(rhLine.PointAtStart) && bhLine.End.IsEqual(rhLine.PointAtEnd));
        }

        /***************************************************/

        public static bool IsEqual(this BHG.NurbCurve bhCurve, RHG.NurbsCurve rhCurve)
        {
            if (bhCurve == null & rhCurve == null)
                return true;

            List<BHG.Point> bhPoints = bhCurve.ControlPoints;
            List<double> bhWeights = bhCurve.Weights;

            RHG.Collections.NurbsCurvePointList rhPoints = rhCurve.Points;

            bool pointsEqual = true;
            bool weightsEqual = true;

            for (int i = 0; i < bhPoints.Count; i++)
                pointsEqual &= bhPoints[i].IsEqual(rhPoints[i]);
            for (int i = 0; i < bhWeights.Count; i++)
                weightsEqual &= bhWeights[i] == rhPoints[i].Weight;
            return (pointsEqual && weightsEqual && bhCurve.Knots == bhCurve.Knots);
        }

        /***************************************************/

        public static bool IsEqual(this BHG.PolyCurve bhCurve, RHG.PolyCurve rhCurve)
        {
            if (bhCurve == null & rhCurve == null)
                return true;

            bool equal = true;
            List<BHG.ICurve> bhCurves = bhCurve.Curves;
            rhCurve.RemoveNesting();
            RHG.Curve[] rhCurves = rhCurve.Explode();

            if (bhCurves.Count != rhCurves.Length)
                return false;

            for (int i = 0; i < bhCurves.Count; i++)
            {
                equal &= bhCurves[i].IIsEqual(rhCurves[i]);
            }

            return equal;
        }

        /***************************************************/

        public static bool IsEqual(this BHG.Polyline bhPolyline, RHG.Polyline rhPolyline)
        {
            if (bhPolyline == null & rhPolyline == null)
                return true;

            RHG.Point3d[] rhPoints = rhPolyline.ToArray();
            List<BHG.Point> bhPoints = bhPolyline.ControlPoints;
            bool pointsEqual = false;
            for (int i = 0; i < bhPoints.Count; i++)
                pointsEqual = bhPoints[i].IsEqual(rhPoints[i]);

            return (pointsEqual);
        }

        /***************************************************/

        public static bool IsEqual(this BHG.Polyline bhPolyline, RHG.PolylineCurve rhPolylineCurve)
        {
            if (bhPolyline == null & rhPolylineCurve == null)
                return true;

            RHG.Polyline rhPolyline; rhPolylineCurve.TryGetPolyline(out rhPolyline);
            return (bhPolyline.IsEqual(rhPolyline));
        }


        /***************************************************/
        /**** Public Methods  - Interfaces              ****/
        /***************************************************/

        public static bool IIsEqual(this BHG.ICurve bhCurve, RHG.Curve rhCurve)
        {
            return IsEqual(bhCurve as dynamic, rhCurve as dynamic);
        }

        /***************************************************/
    }
}
