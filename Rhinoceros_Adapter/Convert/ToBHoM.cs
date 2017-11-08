using System;
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
        /***************************************************/
        /**** Public Methods  - Interfaces              ****/
        /***************************************************/

        public static BHG.IBHoMGeometry IToBHoM(this RHG.GeometryBase geometry)
        {
            return Convert.ToBHoM(geometry as dynamic);
        }

        /***************************************************/

        public static BHG.CompositeGeometry ToBHoM(this List<RHG.GeometryBase> geometries)
        {
            //return new BHG.CompositeGeometry(geometries.Select(x => x.ToBHoM()));
            throw new NotImplementedException();
        }

        /***************************************************/
        /**** Public Methods  - 1D                      ****/
        /***************************************************/

        public static BHG.Point ToBHoM(this RHG.Point3d rhinoPoint)
        {
            return new BHG.Point(rhinoPoint.X, rhinoPoint.Y, rhinoPoint.Z);
        }

        /***************************************************/

        public static BHG.Point ToBHoM(this RHG.Point3f rhinoPoint)
        {
            return new BHG.Point(rhinoPoint.X, rhinoPoint.Y, rhinoPoint.Z);
        }

        /***************************************************/

        public static BHG.Point ToBHoM(this RHG.Point rhinoPoint)
        {
            return new BHG.Point(rhinoPoint.Location.X, rhinoPoint.Location.Y, rhinoPoint.Location.Z);
        }

        /***************************************************/

        public static BHG.Vector ToBHoM(this RHG.Vector3d vector)
        {
            return new BHG.Vector(vector.X, vector.Y, vector.Z);
        }

        /***************************************************/

        public static BHG.Vector ToBHoM(this RHG.Vector3f vector)
        {
            return new BHG.Vector(vector.X, vector.Y, vector.Z);
        }

        /***************************************************/
        /**** Public Methods  - 1D                      ****/
        /***************************************************/

        public static BHG.Arc ToBHoM(this RHG.Arc arc)
        {
            return new BHG.Arc(arc.StartPoint.ToBHoM(), arc.MidPoint.ToBHoM(), arc.EndPoint.ToBHoM());
        }

        /***************************************************/

        public static BHG.Arc ToBHoM(this RHG.ArcCurve arcCurve)
        {
            return new BHG.Arc(arcCurve.Arc.StartPoint.ToBHoM(), arcCurve.Arc.MidPoint.ToBHoM(), arcCurve.Arc.EndPoint.ToBHoM());
        }

        /***************************************************/

        public static BHG.Circle ToBHoM(this RHG.Circle circle)
        {
            return new BHG.Circle(circle.Center.ToBHoM(), circle.Normal.ToBHoM(), circle.Radius);
        }

        /***************************************************/

        public static BHG.Ellipse ToBHoM(this RHG.Ellipse ellipse)
        {
            return new BHG.Ellipse(ellipse.Plane.Origin.ToBHoM(), ellipse.Plane.XAxis.ToBHoM(), ellipse.Plane.YAxis.ToBHoM(), ellipse.Radius1, ellipse.Radius2);
        }

        /***************************************************/

        public static BHG.Line ToBHoM(this RHG.Line line)
        {
            return new BHG.Line(line.From.ToBHoM(), line.To.ToBHoM());
        }

        /***************************************************/

        public static BHG.ICurve ToBHoM(this RHG.Curve rCurve)
        {
            if (rCurve.IsArc())
            {
                RHG.Arc arc = new RHG.Arc();
                rCurve.TryGetArc(out arc);
                return arc.ToBHoM();
            }
            else if (rCurve.IsPolyline())
            {
                RHG.Polyline polyline = new RHG.Polyline();
                rCurve.TryGetPolyline(out polyline);
                return polyline.ToBHoM();
            }
            else if (rCurve.IsCircle())
            {
                RHG.Circle circle = new RHG.Circle();
                rCurve.TryGetCircle(out circle);
                return circle.ToBHoM();
            }
            else if (rCurve.IsEllipse())
            {
                RHG.Ellipse ellipse = new RHG.Ellipse();
                rCurve.TryGetEllipse(out ellipse);
                return ellipse.ToBHoM();
            }
            else
            {
                RHG.NurbsCurve nurbCrv = rCurve.ToNurbsCurve();

                List<BHG.Point> controlPts = new List<BHG.Point>();
                double[] knots = new double[nurbCrv.Knots.Count + 2];
                double[] weight = new double[nurbCrv.Points.Count];

                knots[0] = nurbCrv.Knots[0];
                knots[knots.Length - 1] = nurbCrv.Knots[nurbCrv.Knots.Count - 1];

                for (int i = 1; i < nurbCrv.Knots.Count + 1; i++)
                {
                    knots[i] = nurbCrv.Knots[i - 1];
                }

                for (int i = 0; i < nurbCrv.Points.Count; i++)
                {
                    controlPts.Add(nurbCrv.Points[i].Location.ToBHoM());
                    weight[i] = nurbCrv.Points[i].Weight;
                }

                return new BHG.NurbCurve(controlPts, weight, knots);
            }
        }

        /***************************************************/

        public static BHG.Plane ToBHoM(this RHG.Plane plane)
        {
            return new BHG.Plane(plane.Origin.ToBHoM(), plane.Normal.ToBHoM());
        }

        /***************************************************/

        public static BHG.PolyCurve ToBHoM(this RHG.PolyCurve polyCurve)
        {
            return new BHG.PolyCurve(polyCurve.Explode().Select(x => x.ToBHoM()));
        }

        /***************************************************/

        public static BHG.Polyline ToBHoM(this RHG.Polyline polyline)
        {
            return new BHG.Polyline(polyline.Select(x => x.ToBHoM()));
        }

        /***************************************************/
        /**** Public Methods  - 1D                      ****/
        /***************************************************/

        public static BHG.BoundingBox ToBHoM(this RHG.BoundingBox boundingBox)
        {
            return new BHG.BoundingBox(boundingBox.Min.ToBHoM(), boundingBox.Max.ToBHoM());
        }

        /***************************************************/

        public static BHG.NurbSurface ToBHoM(this RHG.Surface surface)
        {
            throw new NotImplementedException();    // TODO Rhino_Adapter conversion from Surface
        }

        /***************************************************/

        public static BHG.NurbSurface ToBHoM(this RHG.NurbsSurface surface)
        {
            throw new NotImplementedException();    // TODO Rhino_Adapter conversion from NurbsSurface
        }

        /***************************************************/

        public static BHG.PolySurface ToBHoM(this RHG.Brep brep)
        {
            return new BHG.PolySurface(brep.Surfaces.Select(x => x.ToBHoM()));
        }

        /***************************************************/

        public static BHG.Extrusion ToBHoM(this RHG.Extrusion extrusion)
        {
            throw new NotImplementedException(); // TODO Rhino_Adapter conversion from Extrusion
        }

        /***************************************************/

        public static BHG.Mesh ToBHoM(this RHG.Mesh rMesh)
        {
            List<BHG.Point> vertices = rMesh.Vertices.ToList().Select(x => x.ToBHoM()).ToList();
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

        // Unused Code not to waste

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
    }
}
