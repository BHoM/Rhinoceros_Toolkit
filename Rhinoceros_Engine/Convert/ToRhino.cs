﻿using System;
using System.Collections.Generic;
using System.Linq;
using RHG = Rhino.Geometry;
using BHG = BH.oM.Geometry;
using BH.Engine.Geometry;

namespace BH.Engine.Rhinoceros
{
    public static partial class Convert
    {
        /***************************************************/
        /**** Public Methods  - Interfaces              ****/
        /***************************************************/

        public static object IToRhino(this BHG.IGeometry geometry)
        {
            return (geometry == default(BHG.IGeometry)) ? null : Convert.ToRhino(geometry as dynamic);
        }

        /***************************************************/

        public static RHG.Curve IToRhino(this BHG.ICurve curve)
        {
            if (curve == null) return null;

            object result = Convert.ToRhino(curve as dynamic);
            if (result is RHG.Curve)
                return (RHG.Curve)result;
            else if (result is RHG.Arc)
                return new RHG.ArcCurve((RHG.Arc)result);
            else if (result is RHG.Circle)
                return new RHG.ArcCurve((RHG.Circle)result);
            else if (result is RHG.Ellipse)
                return ((RHG.Ellipse)result).ToNurbsCurve();
            else if (result is RHG.Line)
                return new RHG.LineCurve((RHG.Line)result);


            return Convert.ToRhino(curve as dynamic);
        }

        /***************************************************/

        public static RHG.Surface IToRhino(this BHG.ISurface surface)
        {
            return (surface == default(BHG.ISurface)) ? null : Convert.ToRhino(surface as dynamic);
        }


        /***************************************************/
        /**** Public Methods  - Vectors                 ****/
        /***************************************************/

        public static RHG.Point3d ToRhino(this BHG.Point point)
        {
            if (point == null) return default(RHG.Point3d);

            return new RHG.Point3d(point.X, point.Y, point.Z);
        }

        /***************************************************/

        public static RHG.Vector3d ToRhino(this BHG.Vector vector)
        {
            if (vector == null) return default(RHG.Vector3d);

            return new RHG.Vector3d(vector.X, vector.Y, vector.Z);
        }

        /***************************************************/

        public static RHG.Plane ToRhino(this BHG.Plane plane)
        {
            if (plane == null) return default(RHG.Plane);

            return new RHG.Plane(plane.Origin.ToRhino(), plane.Normal.ToRhino());
        }

        /***************************************************/

        public static RHG.Plane ToRhino(this BHG.CoordinateSystem coordinateSystem)
        {
            if (coordinateSystem == null) return default(RHG.Plane);

            return new RHG.Plane(coordinateSystem.Origin.ToRhino(), coordinateSystem.X.ToRhino(), coordinateSystem.Y.ToRhino());
        }

        /***************************************************/

        public static RHG.Quaternion ToRhino(this BHG.Quaternion quartenion)
        {
            return (quartenion == null) ? default(RHG.Quaternion) : new RHG.Quaternion(quartenion.X, quartenion.Y, quartenion.Z, quartenion.W);
        }

        /***************************************************/

        public static RHG.Transform ToRhino(this BHG.TransformMatrix bhTrans)
        {
            if (bhTrans == null) return default(RHG.Transform);

            RHG.Transform rhTrans = new RHG.Transform();
            rhTrans[0, 0] = bhTrans.Matrix[0, 0];
            rhTrans[0, 1] = bhTrans.Matrix[0, 1];
            rhTrans[0, 2] = bhTrans.Matrix[0, 2];
            rhTrans[0, 3] = bhTrans.Matrix[0, 3];

            rhTrans[1, 0] = bhTrans.Matrix[1, 0];
            rhTrans[1, 1] = bhTrans.Matrix[1, 1];
            rhTrans[1, 2] = bhTrans.Matrix[1, 2];
            rhTrans[1, 3] = bhTrans.Matrix[1, 3];

            rhTrans[2, 0] = bhTrans.Matrix[2, 0];
            rhTrans[2, 1] = bhTrans.Matrix[2, 1];
            rhTrans[2, 2] = bhTrans.Matrix[2, 2];
            rhTrans[2, 3] = bhTrans.Matrix[2, 3];

            rhTrans[3, 0] = bhTrans.Matrix[3, 0];
            rhTrans[3, 1] = bhTrans.Matrix[3, 1];
            rhTrans[3, 2] = bhTrans.Matrix[3, 2];
            rhTrans[3, 3] = bhTrans.Matrix[3, 3];

            return rhTrans;
        }


        /***************************************************/
        /**** Public Methods  - Curves                  ****/
        /***************************************************/

        public static RHG.Arc ToRhino(this BHG.Arc arc)
        {
            if (arc == null) return default(RHG.Arc);

            return new RHG.Arc()
            {
                Plane = arc.CoordinateSystem.ToRhino(),
                AngleDomain = new RHG.Interval(arc.StartAngle, arc.EndAngle),
                Radius = arc.Radius
            };
        }

        /***************************************************/

        public static RHG.Circle ToRhino(this BHG.Circle circle)
        {
            if (circle == null) return default(RHG.Circle);

            return new RHG.Circle(new RHG.Plane(circle.Centre.ToRhino(), circle.Normal.ToRhino()), circle.Radius);
        }

        /***************************************************/

        public static RHG.Ellipse ToRhino(this BHG.Ellipse ellipse)
        {
            if (ellipse == null) return default(RHG.Ellipse);

            RHG.Plane plane = new RHG.Plane(ellipse.Centre.ToRhino(), ellipse.Axis1.ToRhino(), ellipse.Axis2.ToRhino());
            return new RHG.Ellipse(plane, ellipse.Radius1, ellipse.Radius2);
        }

        /***************************************************/

        public static RHG.Line ToRhino(this BHG.Line line)
        {
            if (line == null) return default(RHG.Line);

            return new RHG.Line(line.Start.ToRhino(), line.End.ToRhino());
        }

        /***************************************************/

        public static RHG.NurbsCurve ToRhino(this BHG.NurbCurve bCurve)
        {
            if (bCurve == null) return null;

            List<double> knots = bCurve.Knots;
            List<double> weights = bCurve.Weights;
            List<BHG.Point> ctrlPts = bCurve.ControlPoints;

            RHG.NurbsCurve rCurve = new RHG.NurbsCurve(3, false, bCurve.Degree() + 1, ctrlPts.Count);

            int kLen = knots.Count;
            rCurve.Knots[0] = knots[0];
            rCurve.Knots[kLen - 1] = knots[kLen - 1];
            for (int i = 0; i < ctrlPts.Count; i++)
            {
                BHG.Point pt = ctrlPts[i];
                rCurve.Points.SetPoint(i, pt.X, pt.Y, pt.Z, weights[i]);
                rCurve.Knots[i + 1] = knots[i + 1];
            }
            return rCurve;
        }

        /***************************************************/

        public static RHG.PolyCurve ToRhino(this BHG.PolyCurve bPolyCurve)
        {
            if (bPolyCurve == null) return null;

            RHG.PolyCurve rPolycurve = new RHG.PolyCurve();
            bPolyCurve.Curves.ForEach(curve => rPolycurve.Append(curve.IToRhino()));
            return rPolycurve;
        }

        /***************************************************/

        public static RHG.PolylineCurve ToRhino(this BHG.Polyline polyline)
        {
            if (polyline == null) return null;

            return new RHG.PolylineCurve(polyline.ControlPoints.Select(x => x.ToRhino()));
        }


        /***************************************************/
        /**** Public Methods  - Surfaces                ****/
        /***************************************************/

        public static RHG.BoundingBox ToRhino(this BHG.BoundingBox boundingBox)
        {
            if (boundingBox == null) return default(RHG.BoundingBox);

            return new RHG.BoundingBox(boundingBox.Min.ToRhino(), boundingBox.Max.ToRhino());
        }

        /***************************************************/

        public static RHG.NurbsSurface ToRhino(this BHG.NurbSurface surface)
        {
            List<int> uvCount = surface.UVCount();
            List<int> degrees = surface.Degrees();
            return RHG.NurbsSurface.CreateFromPoints(surface.ControlPoints.Select(x => x.ToRhino()),
                uvCount[0], uvCount[1], degrees[0], degrees[1]);
        }

        /***************************************************/

        public static RHG.Brep ToRhino(this BHG.PolySurface polySurface)
        {
            RHG.Brep brep = new RHG.Brep();

            for (int i = 0; i < polySurface.Surfaces.Count; i++)
            {
                brep.AddSurface(polySurface.Surfaces[i].IToRhino());
            }

            return brep;
        }

        /***************************************************/

        public static RHG.Extrusion ToRhino(this BHG.Extrusion extrusion)
        {

            throw new NotImplementedException();    // TODO Rhino_Adapter conversion to Extrusion
        }


        /***************************************************/
        /**** Public Methods  - Mesh                    ****/
        /***************************************************/

        public static RHG.Mesh ToRhino(this BHG.Mesh mesh)
        {
            if (mesh == null) return null;

            List<RHG.Point3d> rVertices = mesh.Vertices.Select(x => x.ToRhino()).ToList();
            List<BHG.Face> faces = mesh.Faces;
            List<RHG.MeshFace> rFaces = new List<RHG.MeshFace>();
            for (int i = 0; i < faces.Count; i++)
            {
                if (faces[i].IsQuad())
                {
                    rFaces.Add(new RHG.MeshFace(faces[i].A, faces[i].B, faces[i].C, faces[i].D));
                }
                else
                {
                    rFaces.Add(new RHG.MeshFace(faces[i].A, faces[i].B, faces[i].C));
                }
            }
            RHG.Mesh rMesh = new RHG.Mesh();
            rMesh.Faces.AddFaces(rFaces);
            rMesh.Vertices.AddVertices(rVertices);
            return rMesh;
        }

        /***************************************************/

        public static RHG.MeshFace ToRhino(this BHG.Face rFace)
        {
            if (rFace == null) return default(RHG.MeshFace);

            if (rFace.IsQuad())
                return new RHG.MeshFace(rFace.A, rFace.B, rFace.C, rFace.D);
            else
                return new RHG.MeshFace(rFace.A, rFace.B, rFace.C);
        }


        /***************************************************/
        /**** Miscellanea                               ****/
        /***************************************************/

        public static List<object> ToRhino(this BHG.CompositeGeometry geometries)
        {
            if (geometries == null) return new List<object>();

            return geometries.Elements.Select(x => x.IToRhino()).ToList();
        }

        /***************************************************/
    }
}
