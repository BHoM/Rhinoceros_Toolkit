using System;
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
            return (curve == null) ? null : Convert.ToRhino(curve as dynamic);
        }

        /***************************************************/

        public static RHG.Surface IToRhino(this BHG.ISurface surface)
        {
            return (surface == null) ? null : Convert.ToRhino(surface as dynamic);
        }


        /***************************************************/
        /**** Public Methods  - Vectors                 ****/
        /***************************************************/

        public static RHG.Point3d ToRhino(this BHG.Point point)
        {
            return new RHG.Point3d(point.X, point.Y, point.Z);
        }

        /***************************************************/

        public static RHG.Vector3d ToRhino(this BHG.Vector vector)
        {
            return new RHG.Vector3d(vector.X, vector.Y, vector.Z);
        }

        /***************************************************/

        public static RHG.Plane ToRhino(this BHG.Plane plane)
        {
            return new RHG.Plane(plane.Origin.ToRhino(), plane.Normal.ToRhino());
        }

        /***************************************************/

        public static RHG.Plane ToRhino(this BHG.CoordinateSystem coordinateSystem)
        {
            return new RHG.Plane(coordinateSystem.Origin.ToRhino(), coordinateSystem.X.ToRhino(), coordinateSystem.Y.ToRhino());
        }

        /***************************************************/

        public static RHG.Transform ToRhino(this BHG.TransformMatrix bhTrans)
        {
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

        public static RHG.ArcCurve ToRhino(this BHG.Arc arc)
        {
            return new RHG.ArcCurve(new RHG.Arc(arc.CoordinateSystem.ToRhino(), arc.Radius, arc.Angle));
        }

        /***************************************************/

        public static RHG.Circle ToRhino(this BHG.Circle circle)
        {
            return new RHG.Circle(new RHG.Plane(circle.Centre.ToRhino(), circle.Normal.ToRhino()), circle.Radius);
        }

        /***************************************************/

        public static RHG.Ellipse ToRhino(this BHG.Ellipse ellipse)
        {
            RHG.Plane plane = new RHG.Plane(ellipse.Centre.ToRhino(), ellipse.Axis1.ToRhino(), ellipse.Axis2.ToRhino());
            return new RHG.Ellipse(plane, ellipse.Radius1, ellipse.Radius2);
        }

        /***************************************************/

        public static RHG.LineCurve ToRhino(this BHG.Line line)
        {
            return new RHG.LineCurve(line.Start.ToRhino(), line.End.ToRhino());
        }

        /***************************************************/

        public static RHG.NurbsCurve ToRhino(this BHG.NurbCurve bCurve)
        {
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
            RHG.PolyCurve rPolycurve = new RHG.PolyCurve();
            bPolyCurve.Curves.ForEach(curve => rPolycurve.Append(curve.IToRhino()));
            return rPolycurve;
        }

        /***************************************************/

        public static RHG.PolylineCurve ToRhino(this BHG.Polyline polyline)
        {
            return new RHG.PolylineCurve(polyline.ControlPoints.Select(x => x.ToRhino()));
        }


        /***************************************************/
        /**** Public Methods  - Surfaces                ****/
        /***************************************************/

        public static RHG.BoundingBox ToRhino(this BHG.BoundingBox boundingBox)
        {
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
            throw new NotImplementedException();    // TODO Rhino_Adapter conversion to Brep. Problems with the Brep() access level
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
        /**** Miscellanea                               ****/
        /***************************************************/

        public static List<object> ToRhino(this BHG.CompositeGeometry geometries)
        {
            return geometries.Elements.Select(x => x.IToRhino()).ToList();
        }

        /***************************************************/
    }
}
