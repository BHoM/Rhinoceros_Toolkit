/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2026, the respective contributors. All rights reserved.
 *
 * Each contributor holds copyright over their respective contributions.
 * The project versioning (Git) records all such contribution source information.
 *
 *
 * The BHoM is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3.0 of the License, or
 * (at your option) any later version.
 *
 * The BHoM is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with this code. If not, see <https://www.gnu.org/licenses/lgpl-3.0.html>.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using BH.oM.Base.Attributes;
using BHG = BH.oM.Geometry;
using RHG = Rhino.Geometry;

namespace BH.Engine.Adapters.Rhinoceros
{
    public static partial class Query
    {
        /***************************************************/
        /**** Public Methods  - Vectors                 ****/
        /***************************************************/

        [Description("Checks whether a BHoM Plane is equal to a Rhino Plane within a given tolerance.")]
        [Input("bhPlane", "The BHoM Plane to compare.")]
        [Input("rhPlane", "The Rhino Plane to compare against.")]
        [Input("tolerance", "Distance tolerance used for comparison. Defaults to BHoM distance tolerance.")]
        [Output("result", "True if the planes are equal within the given tolerance, false otherwise.")]
        public static bool IsEqual(this BHG.Plane bhPlane, RHG.Plane rhPlane, double tolerance = BHG.Tolerance.Distance)
        {
            if (bhPlane == null & rhPlane == default(RHG.Plane))
                return true;

            return bhPlane.Origin.IsEqual(rhPlane.Origin, tolerance)
                && bhPlane.Normal.IsEqual(rhPlane.Normal, tolerance);
        }

        /***************************************************/

        [Description("Checks whether a BHoM Cartesian coordinate system is equal to a Rhino Plane within a given tolerance.")]
        [Input("bhCoordinates", "The BHoM Cartesian coordinate system to compare.")]
        [Input("rhPlane", "The Rhino Plane to compare against.")]
        [Input("tolerance", "Distance tolerance used for comparison. Defaults to BHoM distance tolerance.")]
        [Output("result", "True if the coordinate system and plane are equal within the given tolerance, false otherwise.")]
        public static bool IsEqual(this BHG.CoordinateSystem.Cartesian bhCoordinates, RHG.Plane rhPlane, double tolerance = BHG.Tolerance.Distance)
        {
            if (bhCoordinates == null & rhPlane == default(RHG.Plane))
                return true;

            return bhCoordinates.Origin.IsEqual(rhPlane.Origin, tolerance)
                && bhCoordinates.X.IsEqual(rhPlane.XAxis)
                && bhCoordinates.Y.IsEqual(rhPlane.YAxis)
                && bhCoordinates.Z.IsEqual(rhPlane.ZAxis);
        }

        /***************************************************/

        [Description("Checks whether a BHoM Point is equal to a Rhino Point3d within a given tolerance.")]
        [Input("bhPoint", "The BHoM Point to compare.")]
        [Input("rhPoint", "The Rhino Point3d to compare against.")]
        [Input("tolerance", "Distance tolerance used for comparison. Defaults to BHoM distance tolerance.")]
        [Output("result", "True if the points are equal within the given tolerance, false otherwise.")]
        public static bool IsEqual(this BHG.Point bhPoint, RHG.Point3d rhPoint, double tolerance = BHG.Tolerance.Distance)
        {
            if (bhPoint == null & rhPoint == default(RHG.Point3d))
                return true;

            return Math.Abs(bhPoint.X - rhPoint.X) < tolerance
                && Math.Abs(bhPoint.Y - rhPoint.Y) < tolerance
                && Math.Abs(bhPoint.Z - rhPoint.Z) < tolerance;
        }

        /***************************************************/

        [Description("Checks whether a BHoM Point is equal to a Rhino Point3f within a given tolerance.")]
        [Input("bhPoint", "The BHoM Point to compare.")]
        [Input("rhPoint", "The Rhino Point3f to compare against.")]
        [Input("tolerance", "Distance tolerance used for comparison. Defaults to BHoM distance tolerance.")]
        [Output("result", "True if the points are equal within the given tolerance, false otherwise.")]
        public static bool IsEqual(this BHG.Point bhPoint, RHG.Point3f rhPoint, double tolerance = BHG.Tolerance.Distance)
        {
            if (bhPoint == null & rhPoint == default(RHG.Point3f))
                return true;

            return Math.Abs(bhPoint.X - rhPoint.X) < tolerance
                && Math.Abs(bhPoint.Y - rhPoint.Y) < tolerance
                && Math.Abs(bhPoint.Z - rhPoint.Z) < tolerance;
        }

        /***************************************************/

        [Description("Checks whether a BHoM Point is equal to a Rhino ControlPoint within a given tolerance.")]
        [Input("bhPoint", "The BHoM Point to compare.")]
        [Input("rhPoint", "The Rhino ControlPoint to compare against.")]
        [Input("tolerance", "Distance tolerance used for comparison. Defaults to BHoM distance tolerance.")]
        [Output("result", "True if the points are equal within the given tolerance, false otherwise.")]
        public static bool IsEqual(this BHG.Point bhPoint, RHG.ControlPoint rhPoint, double tolerance = BHG.Tolerance.Distance)
        {
            if (bhPoint == null & rhPoint.Equals(default(RHG.ControlPoint)))
                return true;

            return Math.Abs(bhPoint.X - rhPoint.Location.X) < tolerance
                && Math.Abs(bhPoint.Y - rhPoint.Location.Y) < tolerance
                && Math.Abs(bhPoint.Z - rhPoint.Location.Z) < tolerance;
        }

        /***************************************************/

        [Description("Checks whether a BHoM Vector is equal to a Rhino Vector3d within a given tolerance.")]
        [Input("bhVector", "The BHoM Vector to compare.")]
        [Input("rhVector", "The Rhino Vector3d to compare against.")]
        [Input("tolerance", "Distance tolerance used for comparison. Defaults to BHoM distance tolerance.")]
        [Output("result", "True if the vectors are equal within the given tolerance, false otherwise.")]
        public static bool IsEqual(this BHG.Vector bhVector, RHG.Vector3d rhVector, double tolerance = BHG.Tolerance.Distance)
        {
            if (bhVector == null & rhVector == default(RHG.Vector3d))
                return true;

            return Math.Abs(bhVector.X - rhVector.X) < tolerance
                && Math.Abs(bhVector.Y - rhVector.Y) < tolerance
                && Math.Abs(bhVector.Z - rhVector.Z) < tolerance;
        }

        /***************************************************/

        [Description("Checks whether a BHoM Vector is equal to a Rhino Vector3f within a given tolerance.")]
        [Input("bhVector", "The BHoM Vector to compare.")]
        [Input("rhVector", "The Rhino Vector3f to compare against.")]
        [Input("tolerance", "Distance tolerance used for comparison. Defaults to BHoM distance tolerance.")]
        [Output("result", "True if the vectors are equal within the given tolerance, false otherwise.")]
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

        [Description("Checks whether a BHoM Arc is equal to a Rhino Arc within a given tolerance.")]
        [Input("bhArc", "The BHoM Arc to compare.")]
        [Input("rhArc", "The Rhino Arc to compare against.")]
        [Input("tolerance", "Distance tolerance used for comparison. Defaults to BHoM distance tolerance.")]
        [Output("result", "True if the arcs are equal within the given tolerance, false otherwise.")]
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

        [Description("Checks whether a BHoM Arc is equal to a Rhino ArcCurve within a given tolerance.")]
        [Input("bhArc", "The BHoM Arc to compare.")]
        [Input("rhArc", "The Rhino ArcCurve to compare against.")]
        [Input("tolerance", "Distance tolerance used for comparison. Defaults to BHoM distance tolerance.")]
        [Output("result", "True if the arcs are equal within the given tolerance, false otherwise.")]
        public static bool IsEqual(this BHG.Arc bhArc, RHG.ArcCurve rhArc, double tolerance = BHG.Tolerance.Distance)
        {
            if (bhArc == null & rhArc == null)
                return true;

            RHG.Arc innerArc;
            rhArc.TryGetArc(out innerArc);
            return bhArc.IsEqual(innerArc, tolerance);
        }

        /***************************************************/

        [Description("Checks whether a BHoM Circle is equal to a Rhino Circle within a given tolerance.")]
        [Input("bhCircle", "The BHoM Circle to compare.")]
        [Input("rhCircle", "The Rhino Circle to compare against.")]
        [Input("tolerance", "Distance tolerance used for comparison. Defaults to BHoM distance tolerance.")]
        [Output("result", "True if the circles are equal within the given tolerance, false otherwise.")]
        public static bool IsEqual(this BHG.Circle bhCircle, RHG.Circle rhCircle, double tolerance = BHG.Tolerance.Distance)
        {
            if (bhCircle == null & rhCircle.Equals(default(RHG.Circle)))
                return true;

            return bhCircle.Centre.IsEqual(rhCircle.Center, tolerance)
                && bhCircle.Normal.IsEqual(rhCircle.Normal, tolerance)
                && Math.Abs(bhCircle.Radius - rhCircle.Radius) < tolerance;
        }

        /***************************************************/

        [Description("Checks whether a BHoM Line is equal to a Rhino Line within a given tolerance.")]
        [Input("bhLine", "The BHoM Line to compare.")]
        [Input("rhLine", "The Rhino Line to compare against.")]
        [Input("tolerance", "Distance tolerance used for comparison. Defaults to BHoM distance tolerance.")]
        [Output("result", "True if the lines are equal within the given tolerance, false otherwise.")]
        public static bool IsEqual(this BHG.Line bhLine, RHG.Line rhLine, double tolerance = BHG.Tolerance.Distance)
        {
            if (bhLine == null & rhLine == default(RHG.Line))
                return true;

            return bhLine.Start.IsEqual(rhLine.PointAt(0), tolerance)
                && bhLine.End.IsEqual(rhLine.PointAt(1), tolerance);
        }

        /***************************************************/

        [Description("Checks whether a BHoM Line is equal to a Rhino LineCurve within a given tolerance.")]
        [Input("bhLine", "The BHoM Line to compare.")]
        [Input("rhLine", "The Rhino LineCurve to compare against.")]
        [Input("tolerance", "Distance tolerance used for comparison. Defaults to BHoM distance tolerance.")]
        [Output("result", "True if the lines are equal within the given tolerance, false otherwise.")]
        public static bool IsEqual(this BHG.Line bhLine, RHG.LineCurve rhLine, double tolerance = BHG.Tolerance.Distance)
        {
            if (bhLine == null & rhLine == null)
                return true;

            return bhLine.IsEqual(rhLine.Line, tolerance);
        }

        /***************************************************/

        [Description("Checks whether a BHoM NurbsCurve is equal to a Rhino NurbsCurve within a given tolerance.")]
        [Input("bhCurve", "The BHoM NurbsCurve to compare.")]
        [Input("rhCurve", "The Rhino NurbsCurve to compare against.")]
        [Input("tolerance", "Distance tolerance used for comparison. Defaults to BHoM distance tolerance.")]
        [Output("result", "True if the curves are equal within the given tolerance, false otherwise.")]
        public static bool IsEqual(this BHG.NurbsCurve bhCurve, RHG.NurbsCurve rhCurve, double tolerance = BHG.Tolerance.Distance)
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

        [Description("Checks whether a BHoM PolyCurve is equal to a Rhino PolyCurve within a given tolerance.")]
        [Input("bhCurve", "The BHoM PolyCurve to compare.")]
        [Input("rhCurve", "The Rhino PolyCurve to compare against.")]
        [Input("tolerance", "Distance tolerance used for comparison. Defaults to BHoM distance tolerance.")]
        [Output("result", "True if the poly curves are equal within the given tolerance, false otherwise.")]
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

        [Description("Checks whether a BHoM Polyline is equal to a Rhino Polyline within a given tolerance.")]
        [Input("bhPolyline", "The BHoM Polyline to compare.")]
        [Input("rhPolyline", "The Rhino Polyline to compare against.")]
        [Input("tolerance", "Distance tolerance used for comparison. Defaults to BHoM distance tolerance.")]
        [Output("result", "True if the polylines are equal within the given tolerance, false otherwise.")]
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

        [Description("Checks whether a BHoM Polyline is equal to a Rhino PolylineCurve within a given tolerance.")]
        [Input("bhPolyline", "The BHoM Polyline to compare.")]
        [Input("rhPolylineCurve", "The Rhino PolylineCurve to compare against.")]
        [Input("tolerance", "Distance tolerance used for comparison. Defaults to BHoM distance tolerance.")]
        [Output("result", "True if the polylines are equal within the given tolerance, false otherwise.")]
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

        [Description("Checks whether a BHoM ICurve is equal to a Rhino Curve within a given tolerance. \n" +
            "Dispatches to the appropriate typed overload based on the runtime type of the BHoM curve.")]
        [Input("bhCurve", "The BHoM ICurve to compare.")]
        [Input("rhCurve", "The Rhino Curve to compare against.")]
        [Input("tolerance", "Distance tolerance used for comparison. Defaults to BHoM distance tolerance.")]
        [Output("result", "True if the curves are equal within the given tolerance, false otherwise.")]
        public static bool IIsEqual(this BHG.ICurve bhCurve, RHG.Curve rhCurve, double tolerance = BHG.Tolerance.Distance)
        {
            return IsEqual(bhCurve as dynamic, rhCurve as dynamic, tolerance);
        }

        /***************************************************/
    }
}
