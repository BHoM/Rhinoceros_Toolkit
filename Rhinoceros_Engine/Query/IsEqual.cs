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

        public static bool IsEqual(this BHG.Plane bhPlane, RHG.Plane rhPlane, double tolerance = BHG.Tolerance.Distance)
        {
            if (bhPlane == null & rhPlane == default(RHG.Plane))
                return true;

            return bhPlane.Origin.IsEqual(rhPlane.Origin, tolerance)
                && bhPlane.Normal.IsEqual(rhPlane.Normal, tolerance);
        }

        /***************************************************/

        public static bool IsEqual(this BHG.CoordinateSystem bhCoordinates, RHG.Plane rhPlane, double tolerance = BHG.Tolerance.Distance)
        {
            if (bhCoordinates == null & rhPlane == default(RHG.Plane))
                return true;

            return bhCoordinates.Origin.IsEqual(rhPlane.Origin, tolerance)
                && bhCoordinates.X.IsEqual(rhPlane.XAxis)
                && bhCoordinates.Y.IsEqual(rhPlane.YAxis)
                && bhCoordinates.Z.IsEqual(rhPlane.ZAxis);
        }

        /***************************************************/

        public static bool IsEqual(this BHG.Point bhPoint, RHG.Point3d rhPoint, double tolerance = BHG.Tolerance.Distance)
        {
            if (bhPoint == null & rhPoint == default(RHG.Point3d))
                return true;

            return Math.Abs(bhPoint.X - rhPoint.X) < tolerance
                && Math.Abs(bhPoint.Y - rhPoint.Y) < tolerance
                && Math.Abs(bhPoint.Z - rhPoint.Z) < tolerance;
        }

        /***************************************************/

        public static bool IsEqual(this BHG.Point bhPoint, RHG.Point3f rhPoint, double tolerance = BHG.Tolerance.Distance)
        {
            if (bhPoint == null & rhPoint == default(RHG.Point3f))
                return true;

            return Math.Abs(bhPoint.X - rhPoint.X) < tolerance
                && Math.Abs(bhPoint.Y - rhPoint.Y) < tolerance
                && Math.Abs(bhPoint.Z - rhPoint.Z) < tolerance;
        }

        /***************************************************/

        public static bool IsEqual(this BHG.Point bhPoint, RHG.ControlPoint rhPoint, double tolerance = BHG.Tolerance.Distance)
        {
            if (bhPoint == null & rhPoint.Equals(default(RHG.ControlPoint)))
                return true;

            return Math.Abs(bhPoint.X - rhPoint.Location.X) < tolerance
                && Math.Abs(bhPoint.Y - rhPoint.Location.Y) < tolerance
                && Math.Abs(bhPoint.Z - rhPoint.Location.Z) < tolerance;
        }

        /***************************************************/

        public static bool IsEqual(this BHG.Vector bhVector, RHG.Vector3d rhVector, double tolerance = BHG.Tolerance.Distance)
        {
            if (bhVector == null & rhVector == default(RHG.Vector3d))
                return true;

            return Math.Abs(bhVector.X - rhVector.X) < tolerance
                && Math.Abs(bhVector.Y - rhVector.Y) < tolerance
                && Math.Abs(bhVector.Z - rhVector.Z) < tolerance;
        }

        /***************************************************/

        public static bool IsEqual(this BHG.Vector bhVector, RHG.Vector3f rhVector, double tolerance = BHG.Tolerance.Distance)
        {
            if (bhVector == null & rhVector == default(RHG.Vector3f))
                return true;

            return Math.Abs(bhVector.X - rhVector.X) < tolerance
                && Math.Abs(bhVector.Y - rhVector.Y) < tolerance
                && Math.Abs(bhVector.Z - rhVector.Z) < tolerance;
        }


        /***************************************************/
        /**** Public Methods  - Curves                  ****/
        /***************************************************/

        public static bool IsEqual(this BHG.Arc bhArc, RHG.Arc rhArc, double tolerance = BHG.Tolerance.Distance)
        {
            if (bhArc == null & rhArc == default(RHG.Arc))
                return true;

            return bhArc.CoordinateSystem.IsEqual(rhArc.Plane, tolerance)
                && Math.Abs(bhArc.Radius - rhArc.Radius) < tolerance
                && Math.Abs(bhArc.StartAngle - rhArc.StartAngle) < tolerance
                && Math.Abs(bhArc.EndAngle - rhArc.EndAngle) < tolerance;
        }

        /***************************************************/

        public static bool IsEqual(this BHG.Arc bhArc, RHG.ArcCurve rhArc, double tolerance = BHG.Tolerance.Distance)
        {
            if (bhArc == null & rhArc == null)
                return true;

            RHG.Arc innerArc;
            rhArc.TryGetArc(out innerArc);
            return bhArc.IsEqual(innerArc, tolerance);
        }

        /***************************************************/

        public static bool IsEqual(this BHG.Circle bhCircle, RHG.Circle rhCircle, double tolerance = BHG.Tolerance.Distance)
        {
            if (bhCircle == null & rhCircle.Equals(default(RHG.Circle)))
                return true;

            return bhCircle.Centre.IsEqual(rhCircle.Center, tolerance)
                && bhCircle.Normal.IsEqual(rhCircle.Normal, tolerance)
                && Math.Abs(bhCircle.Radius - rhCircle.Radius) < tolerance;
        }

        /***************************************************/

        public static bool IsEqual(this BHG.Line bhLine, RHG.Line rhLine, double tolerance = BHG.Tolerance.Distance)
        {
            if (bhLine == null & rhLine == default(RHG.Line))
                return true;

            return bhLine.Start.IsEqual(rhLine.PointAt(0), tolerance)
                && bhLine.End.IsEqual(rhLine.PointAt(1), tolerance);
        }

        /***************************************************/
        
        public static bool IsEqual(this BHG.Line bhLine, RHG.LineCurve rhLine, double tolerance = BHG.Tolerance.Distance)
        {
            if (bhLine == null & rhLine == null)
                return true;

            return bhLine.IsEqual(rhLine.Line, tolerance);
        }

        /***************************************************/

        public static bool IsEqual(this BHG.NurbCurve bhCurve, RHG.NurbsCurve rhCurve, double tolerance = BHG.Tolerance.Distance)
        {
            if (bhCurve == null & rhCurve == null)
                return true;

            List<BHG.Point> bhPoints = bhCurve.ControlPoints;
            List<double> bhWeights = bhCurve.Weights;
            List<double> bhKnots = bhCurve.Knots;

            RHG.Collections.NurbsCurvePointList rhPoints = rhCurve.Points;

            bool pointsEqual = true;
            bool weightsEqual = true;
            bool knotsEqual = true;

            for (int i = 0; i < bhPoints.Count; i++)
                pointsEqual &= bhPoints[i].IsEqual(rhPoints[i], tolerance);
            for (int i = 0; i < bhWeights.Count; i++)
                weightsEqual &= Math.Abs(bhWeights[i] - rhPoints[i].Weight) < tolerance;
            for (int i = 0; i < bhKnots.Count; i++)
                knotsEqual &= Math.Abs(bhKnots[i] - rhCurve.Knots[i]) < tolerance;

            return (pointsEqual && weightsEqual && knotsEqual);
        }

        /***************************************************/

        public static bool IsEqual(this BHG.PolyCurve bhCurve, RHG.PolyCurve rhCurve, double tolerance = BHG.Tolerance.Distance)
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
                equal &= bhCurves[i].IIsEqual(rhCurves[i], tolerance);

            return equal;
        }

        /***************************************************/

        public static bool IsEqual(this BHG.Polyline bhPolyline, RHG.Polyline rhPolyline, double tolerance = BHG.Tolerance.Distance)
        {
            if (bhPolyline == null & rhPolyline == null)
                return true;

            RHG.Point3d[] rhPoints = rhPolyline.ToArray();
            List<BHG.Point> bhPoints = bhPolyline.ControlPoints;
            bool pointsEqual = false;

            for (int i = 0; i < bhPoints.Count; i++)
                pointsEqual = bhPoints[i].IsEqual(rhPoints[i], tolerance);

            return (pointsEqual);
        }

        /***************************************************/

        public static bool IsEqual(this BHG.Polyline bhPolyline, RHG.PolylineCurve rhPolylineCurve, double tolerance = BHG.Tolerance.Distance)
        {
            if (bhPolyline == null & rhPolylineCurve == null)
                return true;

            RHG.Polyline rhPolyline; rhPolylineCurve.TryGetPolyline(out rhPolyline);
            return (bhPolyline.IsEqual(rhPolyline, tolerance));
        }


        /***************************************************/
        /**** Public Methods  - Interfaces              ****/
        /***************************************************/

        public static bool IIsEqual(this BHG.ICurve bhCurve, RHG.Curve rhCurve, double tolerance = BHG.Tolerance.Distance)
        {
            return IsEqual(bhCurve as dynamic, rhCurve as dynamic, tolerance);
        }

        /***************************************************/
    }
}
