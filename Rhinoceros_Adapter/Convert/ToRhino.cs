using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RHG = Rhino.Geometry;
using BHG = BH.oM.Geometry;
using Rhino;

namespace BH.Adapter.Rhinoceros
{
    public static partial class Convert
    {
        /***************************************************/
        /**** Public Methods  - Interfaces              ****/
        /***************************************************/

        public static RHG.GeometryBase IToRhino(this BHG.IBHoMGeometry geometry)
        {
            return Convert.ToRhino(geometry as dynamic);
        }

        public static RHG.Curve IToRhino(this BHG.ICurve geometry)
        {
            return Convert.ToRhino(geometry as dynamic);
        }

        /***************************************************/

        public static List<RHG.GeometryBase> ToRhino(this BHG.CompositeGeometry geometries)
        {
            return geometries.Elements.Select(x => x.IToRhino()).ToList();
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
        /**** Public Methods  - Curves                  ****/
        /***************************************************/

        public static RHG.ArcCurve ToRhino(this BHG.Arc arc)
        {
            return new RHG.ArcCurve(new RHG.Arc(arc.Start.ToRhino(), arc.Middle.ToRhino(), arc.End.ToRhino()));
        }

        /***************************************************/

        public static RHG.Circle ToRhino(this BHG.Circle circle)
        {
            return new RHG.Circle(new RHG.Plane(circle.Centre.ToRhino(), circle.Normal.ToRhino()), circle.Radius);
        }

        /***************************************************/

        public static RHG.Line ToRhino(this BHG.Line line)
        {
            return new RHG.Line(line.Start.ToRhino(), line.End.ToRhino());
        }

        /***************************************************/

        public static RHG.NurbsCurve ToRhino(this BHG.NurbCurve bCurve)
        {
            IEnumerable<RHG.Point3d> rPoints = bCurve.ControlPoints.Select(x => x.ToRhino());
            return RHG.Curve.CreateControlPointCurve(rPoints, bCurve.GetDegree()) as RHG.NurbsCurve;
        }

        /***************************************************/

        public static RHG.PolyCurve ToRhino(this BHG.PolyCurve polyCurve)
        {
            return RHG.Curve.JoinCurves(polyCurve.Curves.Select(x => x.IToRhino()) as IEnumerable<RHG.PolyCurve>)[0] as RHG.PolyCurve;
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

        public static RHG.Surface ToRhino(this BHG.NurbSurface surface)
        {
            return RHG.NurbsSurface.CreateFromPoints(surface.ControlPoints.Select(x => x.ToRhino()), surface.UKnots.Count, surface.VKnots.Count, surface.GetDegree(), surface.GetDegree());
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
    }
}
