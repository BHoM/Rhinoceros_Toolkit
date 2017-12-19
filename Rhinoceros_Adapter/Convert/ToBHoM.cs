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

        public static BHG.IBHoMGeometry ToBHoM(this RHG.GeometryBase geometry)
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

        public static BHG.Point ToBHoM(this RHG.ControlPoint rhinoPoint)
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

        public static BHG.Plane ToBHoM(this RHG.Plane plane)
        {
            return new BHG.Plane(plane.Origin.ToBHoM(), plane.Normal.ToBHoM());
        }

        /***************************************************/
        /**** Public Methods  - Curves                  ****/
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

        public static BHG.Line ToBHoM(this RHG.LineCurve line)
        {
            return new BHG.Line(line.PointAtStart.ToBHoM(), line.PointAtEnd.ToBHoM());
        }

        public static BHG.NurbCurve ToBHoM(this RHG.NurbsCurve rCurve)
        {
            IEnumerable<RHG.ControlPoint> rPoints = rCurve.Points;
            List<double> knots = rCurve.Knots.ToList();
            knots.Insert(0, knots.First());
            knots.Add(knots.Last());

            return new BHG.NurbCurve(rPoints.Select(x => x.ToBHoM()), rPoints.Select(x => x.Weight), knots);
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
            return new BHG.PolyCurve(polyCurve.Explode().Select(x => x.ToBHoM()));
        }

        /***************************************************/

        public static BHG.Polyline ToBHoM(this RHG.Polyline polyline)
        {
            return new BHG.Polyline(polyline.Select(x => x.ToBHoM()));
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
            return new BHG.BoundingBox(boundingBox.Min.ToBHoM(), boundingBox.Max.ToBHoM());
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
            List<BHG.Point> controlPts = surface.Points.Select(x => x.Location.ToBHoM()).ToList();
            List<double> weights = surface.Points.Select(x => x.Weight).ToList();
            List<double> uKnots = surface.KnotsU.ToList();
            List<double> vKnots = surface.KnotsV.ToList();

            return new BHG.NurbSurface(controlPts, weights, uKnots, vKnots);
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


        /***************************************************/
        /**** Miscellanea                               ****/
        /***************************************************/

        public static BHG.CompositeGeometry ToBHoM(this List<RHG.GeometryBase> geometries)
        {
            return new BHG.CompositeGeometry(geometries.Select(x => x.ToBHoM()));
        }
    }
}
