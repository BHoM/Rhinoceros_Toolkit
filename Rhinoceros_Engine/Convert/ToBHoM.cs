using System;
using System.Collections.Generic;
using System.Linq;
using RHG = Rhino.Geometry;
using BHG = BH.oM.Geometry;

namespace BH.Engine.Rhinoceros
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

        public static BHG.IBHoMGeometry IToBHoM<T>(this Rhino.IEpsilonComparable<T> geometry)
        {
            return Convert.ToBHoM(geometry as dynamic);
        }


        /***************************************************/
        /**** Public Methods  - Vectors                 ****/
        /***************************************************/

        public static BHG.Point ToBHoM(this RHG.Point3d rhinoPoint)
        {
            return new BHG.Point { X = rhinoPoint.X, Y = rhinoPoint.Y, Z = rhinoPoint.Z };
        }

        /***************************************************/

        public static BHG.Point ToBHoM(this RHG.Point3f rhinoPoint)
        {
            return new BHG.Point { X = rhinoPoint.X, Y = rhinoPoint.Y, Z = rhinoPoint.Z };
        }

        /***************************************************/

        public static BHG.Point ToBHoM(this RHG.Point rhinoPoint)
        {
            return new BHG.Point { X = rhinoPoint.Location.X, Y = rhinoPoint.Location.Y, Z = rhinoPoint.Location.Z };
        }

        /***************************************************/

        public static BHG.Point ToBHoM(this RHG.ControlPoint rhinoPoint)
        {
            return new BHG.Point { X = rhinoPoint.Location.X, Y = rhinoPoint.Location.Y, Z = rhinoPoint.Location.Z };
        }

        /***************************************************/

        public static BHG.Vector ToBHoM(this RHG.Vector3d vector)
        {
            return new BHG.Vector { X = vector.X, Y = vector.Y, Z = vector.Z };
        }

        /***************************************************/

        public static BHG.Vector ToBHoM(this RHG.Vector3f vector)
        {
            return new BHG.Vector { X = vector.X, Y = vector.Y, Z = vector.Z };
        }

        /***************************************************/

        public static BHG.Plane ToBHoM(this RHG.Plane plane)
        {
            return new BHG.Plane { Origin = plane.Origin.ToBHoM(), Normal = plane.Normal.ToBHoM() };
        }

        /***************************************************/
        /**** Public Methods  - Curves                  ****/
        /***************************************************/

        public static BHG.Arc ToBHoM(this RHG.Arc arc)
        {
            return new BHG.Arc { Start = arc.StartPoint.ToBHoM(), Middle = arc.MidPoint.ToBHoM(), End = arc.EndPoint.ToBHoM() };
        }

        /***************************************************/

        public static BHG.Arc ToBHoM(this RHG.ArcCurve arcCurve)
        {
            return new BHG.Arc { Start = arcCurve.Arc.StartPoint.ToBHoM(), Middle = arcCurve.Arc.MidPoint.ToBHoM(), End = arcCurve.Arc.EndPoint.ToBHoM() };
        }

        /***************************************************/

        public static BHG.Circle ToBHoM(this RHG.Circle circle)
        {
            return new BHG.Circle { Centre = circle.Center.ToBHoM(), Normal = circle.Normal.ToBHoM(), Radius = circle.Radius };
        }

        /***************************************************/

        public static BHG.Ellipse ToBHoM(this RHG.Ellipse ellipse)
        {
            return new BHG.Ellipse
            {
                Centre = ellipse.Plane.Origin.ToBHoM(),
                Axis1 = ellipse.Plane.XAxis.ToBHoM(),
                Axis2 = ellipse.Plane.YAxis.ToBHoM(),
                Radius1 = ellipse.Radius1,
                Radius2 = ellipse.Radius2
            };
        }

        /***************************************************/

        public static BHG.Line ToBHoM(this RHG.Line line)
        {
            return new BHG.Line { Start = line.From.ToBHoM(), End = line.To.ToBHoM() };
        }

        /***************************************************/

        public static BHG.Line ToBHoM(this RHG.LineCurve line)
        {
            return new BHG.Line { Start = line.PointAtStart.ToBHoM(), End = line.PointAtEnd.ToBHoM() };
        }

        public static BHG.NurbCurve ToBHoM(this RHG.NurbsCurve rCurve)
        {
            IEnumerable<RHG.ControlPoint> rPoints = rCurve.Points;
            List<double> knots = rCurve.Knots.ToList();          
            return new BHG.NurbCurve
            {
                ControlPoints = rPoints.Select(x => x.ToBHoM()).ToList(),
                Weights = rPoints.Select(x => x.Weight).ToList(),
                Knots = knots
            };
        }

        /***************************************************/

        public static BHG.ICurve ToBHoM(this RHG.Curve rCurve)
        {
            Type curveType = rCurve.GetType();
            if (rCurve.IsCircle())
            {
                RHG.Circle circle = new RHG.Circle();
                rCurve.TryGetCircle(out circle);
                return circle.ToBHoM();
            }
            else if (rCurve.IsArc() || typeof(RHG.ArcCurve).IsAssignableFrom(curveType))
            {
                RHG.Arc arc = new RHG.Arc();
                rCurve.TryGetArc(out arc);
                return arc.ToBHoM();
            }
            else if (rCurve.IsPolyline() || typeof(RHG.PolylineCurve).IsAssignableFrom(curveType))
            {
                RHG.Polyline polyline = new RHG.Polyline();
                rCurve.TryGetPolyline(out polyline);
                return polyline.ToBHoM();
            }
            else if (rCurve.IsEllipse())
            {
                RHG.Ellipse ellipse = new RHG.Ellipse();
                rCurve.TryGetEllipse(out ellipse);
                return ellipse.ToBHoM();
            }
            else if (rCurve is RHG.NurbsCurve)
            {
                return ((RHG.NurbsCurve)rCurve).ToBHoM();
            }
            else if (rCurve is RHG.PolyCurve)
            {
                return ((RHG.PolyCurve)rCurve).ToBHoM();
            }
            else
            {
                return (rCurve.ToNurbsCurve()).ToBHoM();
            }
        }

        /***************************************************/

        public static BHG.PolyCurve ToBHoM(this RHG.PolyCurve polyCurve)
        {
            polyCurve.RemoveNesting();
            return new BHG.PolyCurve { Curves = polyCurve.Explode().Select(x => x.ToBHoM()).ToList() };
        }

        /***************************************************/

        public static BHG.Polyline ToBHoM(this RHG.Polyline polyline)
        {
            return new BHG.Polyline { ControlPoints = polyline.Select(x => x.ToBHoM()).ToList() };
        }

        /***************************************************/

        public static BHG.Polyline ToBHoM(this RHG.PolylineCurve polyline)
        {
            if (!polyline.IsPolyline()) { return null; }
            RHG.Polyline rPolyline; polyline.TryGetPolyline(out rPolyline);
            return rPolyline.ToBHoM();
        }

        /***************************************************/
        /**** Public Methods  - Surfaces                ****/
        /***************************************************/

        public static BHG.BoundingBox ToBHoM(this RHG.BoundingBox boundingBox)
        {
            return new BHG.BoundingBox { Min = boundingBox.Min.ToBHoM(), Max = boundingBox.Max.ToBHoM() };
        }

        /***************************************************/

        public static BHG.BoundingBox ToBHoM(this RHG.Box box)
        {
            return box.BoundingBox.ToBHoM();
        }

        /***************************************************/

        public static BHG.NurbSurface ToBHoM(this RHG.Surface surface)
        {
            return surface.ToNurbsSurface().ToBHoM();
        }

        /***************************************************/

        public static BHG.NurbSurface ToBHoM(this RHG.NurbsSurface surface)
        {
            return new BHG.NurbSurface
            {
                ControlPoints = surface.Points.Select(x => x.Location.ToBHoM()).ToList(),
                Weights = surface.Points.Select(x => x.Weight).ToList(),
                UKnots = surface.KnotsU.ToList(),
                VKnots = surface.KnotsV.ToList()
            };
        }

        /***************************************************/

        public static BHG.ISurface ToBHoM(this RHG.Brep brep)
        {
            if (brep.IsSurface)
                return brep.Faces[0].ToBHoM();
            return null;
        }

        /***************************************************/

        public static BHG.Extrusion ToBHoM(this RHG.Extrusion extrusion)
        {
            extrusion.PathLineCurve();
            throw new NotImplementedException(); // TODO Rhino_Adapter conversion from Extrusion
        }

        /***************************************************/
        /**** Public Methods  - Mesh                    ****/
        /***************************************************/

        public static BHG.Mesh ToBHoM(this RHG.Mesh rMesh)
        {
            List<BHG.Point> vertices = rMesh.Vertices.ToList().Select(x => x.ToBHoM()).ToList();
            List<RHG.MeshFace> rFaces = rMesh.Faces.ToList();
            List<BHG.Face> faces = new List<BHG.Face>();
            for (int i = 0; i < rFaces.Count; i++)
            {
                if (rFaces[i].IsQuad)
                {
                    faces.Add(new BHG.Face { A = rFaces[i].A, B = rFaces[i].B, C = rFaces[i].C, D = rFaces[i].D });
                }
                if (rFaces[i].IsTriangle)
                {
                    faces.Add(new BHG.Face { A = rFaces[i].A, B = rFaces[i].B, C = rFaces[i].C });
                }
            }
            return new BHG.Mesh { Vertices = vertices, Faces = faces };
        }


        /***************************************************/
        /**** Miscellanea                               ****/
        /***************************************************/

        public static BHG.CompositeGeometry ToBHoM(this List<RHG.GeometryBase> geometries)
        {
            return new BHG.CompositeGeometry { Elements = geometries.Select(x => x.IToBHoM()).ToList() };
        }

        /***************************************************/
    }
}
