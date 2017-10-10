﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RHG = Rhino.Geometry;
using BHG = BH.oM.Geometry;

namespace BH.Adapter.Rhinoceros
{
    public static partial class Convert
    {
        #region Public Methods
        public static BHG.IBHoMGeometry FromRhino(this RHG.GeometryBase geometry)
        {
            return Convert.FromRhino(geometry as dynamic);
        }
        public static BHG.CompositeGeometry FromRhino(this List<RHG.GeometryBase> geometries)
        {
                return new BHG.CompositeGeometry(geometries.Select(x => x.FromRhino()));
        }

        #region 1D
        public static BHG.Point FromRhino(this RHG.Point3d rhinoPoint)
        {
            return new BHG.Point(rhinoPoint.X, rhinoPoint.Y, rhinoPoint.Z);
        }
        public static BHG.Point FromRhino(this RHG.Point3f rhinoPoint)
        {
            return new BHG.Point(rhinoPoint.X, rhinoPoint.Y, rhinoPoint.Z);
        }
        public static BHG.Point FromRhino(this RHG.Point rhinoPoint)
        {
            return new BHG.Point(rhinoPoint.Location.X, rhinoPoint.Location.Y, rhinoPoint.Location.Z);
        }
        public static BHG.Vector FromRhino(this RHG.Vector3d vector)
        {
            return new BHG.Vector(vector.X, vector.Y, vector.Z);
        }
        public static BHG.Vector FromRhino(this RHG.Vector3f vector)
        {
            return new BHG.Vector(vector.X, vector.Y, vector.Z);
        }
        #endregion

        #region 2D
        public static BHG.Arc FromRhino(this RHG.Arc arc)
        {
            return new BHG.Arc(arc.StartPoint.FromRhino(), arc.MidPoint.FromRhino(), arc.EndPoint.FromRhino());
        }
        public static BHG.Arc FromRhino(this RHG.ArcCurve arcCurve)
        {
            return new BHG.Arc(arcCurve.Arc.StartPoint.FromRhino(), arcCurve.Arc.MidPoint.FromRhino(), arcCurve.Arc.EndPoint.FromRhino());
        }
        public static BHG.Circle FromRhino(this RHG.Circle circle)
        {
            return new BHG.Circle(circle.Center.FromRhino(), circle.Normal.FromRhino(), circle.Radius);
        }
        public static BHG.Line FromRhino(this RHG.Line line)
        {
            return new BHG.Line(line.From.FromRhino(), line.To.FromRhino());
        }
        public static BHG.NurbCurve FromRhino(this RHG.Curve nurbCurve)
        {
            // Old Code is not used since the BHoM2.0 implementatation of NurbCurve lacks some fields
            #region Old Code
            //if (rCurve is R.ArcCurve && (rCurve as R.ArcCurve).AngleRadians < Math.PI * 2 / 3)
            //{
            //    R.Arc arc = (rCurve as R.ArcCurve).Arc;
            //    return new BHG.Arc(Convert(arc.StartPoint), Convert(arc.EndPoint), Convert(arc.MidPoint));
            //}
            //else if (rCurve is R.LineCurve)
            //{
            //    return new BHG.Line(Convert(rCurve.PointAtStart), Convert(rCurve.PointAtEnd));
            //}
            //else if (rCurve is R.PolylineCurve)
            //{
            //    R.PolylineCurve pl = (rCurve as R.PolylineCurve);
            //    List<R.Point3d> points = new List<R.Point3d>();
            //    for (int i = 0; i < pl.PointCount; i++)
            //    {
            //        points.Add(pl.Point(i));
            //    }
            //    return new BHG.Polyline(points.Select(x => Convert(x)).ToList());
            //}
            //else
            //{
            //    R.NurbsCurve nurbCurve = rCurve.ToNurbsCurve();
            //    int degree = nurbCurve.Degree;
            //    double[] knots = new double[nurbCurve.Knots.Count + 2];
            //    List<BHG.Point> points = new List<BHG.Point>();
            //    double[] weight = new double[nurbCurve.Points.Count];
            //    knots[0] = nurbCurve.Knots[0];
            //    knots[knots.Length - 1] = nurbCurve.Knots[nurbCurve.Knots.Count - 1];
            //    for (int i = 1; i < nurbCurve.Knots.Count + 1; i++)
            //    {
            //        knots[i] = nurbCurve.Knots[i - 1];

            //    }

            //    for (int i = 0; i < nurbCurve.Points.Count; i++)
            //    {
            //        points.Add(Convert(nurbCurve.Points[i].Location));
            //        weight[i] = nurbCurve.Points[i].Weight;
            //    }
            //    return new BHG.NurbCurve(points, knots, weight);
            //}
            #endregion                                  
            throw new NotImplementedException();    // TODO Rhino_Adapter conversion from NurbsCurve
        }
        public static BHG.Plane FromRhino(this RHG.Plane plane)
        {
            return new BHG.Plane(plane.Origin.FromRhino(), plane.Normal.FromRhino());
        }
        public static BHG.PolyCurve FromRhino(this RHG.PolyCurve polyCurve)
        {
            return new BHG.PolyCurve(polyCurve.Explode().Select(x => x.FromRhino()));
        }
        public static BHG.Polyline FromRhino(this RHG.Polyline polyline)
        {
            return new BHG.Polyline(polyline.Select(x => x.FromRhino()));
        }
        #endregion

        #region 3D
        public static BHG.BoundingBox FromRhino(this RHG.BoundingBox boundingBox)
        {
            return new BHG.BoundingBox(boundingBox.Min.FromRhino(), boundingBox.Max.FromRhino());
        }
        public static BHG.NurbSurface FromRhino(this RHG.Surface surface)
        {
            throw new NotImplementedException();    // TODO Rhino_Adapter conversion from Surface
        }
        public static BHG.NurbSurface FromRhino(this RHG.NurbsSurface surface)
        {
            throw new NotImplementedException();    // TODO Rhino_Adapter conversion from NurbsSurface
        }
        public static BHG.PolySurface FromRhino(this RHG.Brep brep)
        {
            return new BHG.PolySurface(brep.Surfaces.Select(x => x.FromRhino()));
        }
        public static BHG.Extrusion FromRhino(this RHG.Extrusion extrusion)
        {
            throw new NotImplementedException(); // TODO Rhino_Adapter conversion from Extrusion
        }
        public static BHG.Mesh FromRhino(this RHG.Mesh rMesh)
        {
            List<BHG.Point> vertices = rMesh.Vertices.ToList().Select(x => x.FromRhino()).ToList();
            List<RHG.MeshFace> rFaces = rMesh.Faces.ToList();
            List<BHG.Face> Faces = new List<BHG.Face>();
            for (int i = 0; i < rFaces.Count; i++)
            {
                if (rFaces[i].IsQuad)
                {
                    Faces.Add(new BHG.Face(rFaces[i].A, rFaces[i].B, rFaces[i].C, rFaces[i].D));
                }
                if (rFaces[i].IsTriangle)
                {
                    Faces.Add(new BHG.Face(rFaces[i].A, rFaces[i].B, rFaces[i].C));
                }
            }
            return new BHG.Mesh(vertices, Faces);
        }
        #endregion
        #endregion

        #region Unused Code not to waste
        //public static List<R.Surface> ExtrudeAlong(R.Curve section, R.Curve centreline, R.Plane sectionPlane)
        //{
        //    R.Vector3d globalUp = R.Vector3d.ZAxis;
        //    R.Vector3d localX = sectionPlane.XAxis;
        //    R.Curve[] baseCurves = centreline.DuplicateSegments();
        //    List<R.Surface> extrustions = new List<R.Surface>();
        //    if (baseCurves.Length == 0) baseCurves = new R.Curve[] { centreline };
        //    for (int i = 0; i < baseCurves.Length; i++)
        //    {
        //        R.Vector3d v = baseCurves[i].PointAtEnd - baseCurves[i].PointAtStart;
        //        R.Curve start = section.Duplicate() as R.Curve;
        //        if (v.IsParallelTo(globalUp) == 0)
        //        {
        //            R.Vector3d direction = sectionPlane.Normal;
        //            double angle = R.Vector3d.VectorAngle(v, direction);
        //            R.Transform alignPerpendicular = R.Transform.Rotation(-angle, R.Vector3d.CrossProduct(v, R.Vector3d.ZAxis), R.Point3d.Origin);
        //            localX.Transform(alignPerpendicular);
        //            direction.Transform(alignPerpendicular);
        //            double angleAxisAlign = R.Vector3d.VectorAngle(localX, R.Vector3d.CrossProduct(globalUp, v));
        //            if (localX * globalUp > 0) angleAxisAlign = -angleAxisAlign;
        //            R.Transform axisAlign = R.Transform.Rotation(angleAxisAlign, v, R.Point3d.Origin);
        //            R.Transform result = R.Transform.Translation(baseCurves[i].PointAtStart - R.Point3d.Origin) * axisAlign * alignPerpendicular;// * axisAlign *                

        //            start.Transform(result);
        //        }
        //        else
        //        {
        //            start.Translate(baseCurves[i].PointAtStart - R.Point3d.Origin);
        //        }
        //        extrustions.Add(R.Extrusion.CreateExtrusion(start, v));
        //    }
        //    return extrustions;
        //}
        #endregion
    }
}
