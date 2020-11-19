/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2020, the respective contributors. All rights reserved.
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
using System.Linq;
using RHG = Rhino.Geometry;
using BHG = BH.oM.Geometry;
using BH.Engine.Reflection;

namespace BH.Engine.Rhinoceros
{
    public static partial class Convert
    {
        /***************************************************/
        /**** Public Methods  - Interfaces              ****/
        /***************************************************/

        public static BHG.IGeometry IFromRhino(this RHG.GeometryBase geometry)
        {
            return (geometry == null) ? null : Convert.FromRhino(geometry as dynamic);
        }

        /***************************************************/

        public static BHG.IGeometry IFromRhino<T>(this Rhino.IEpsilonComparable<T> geometry)
        {
            return (geometry == null) ? null : Convert.FromRhino(geometry as dynamic);
        }

        /***************************************************/

        public static BHG.IGeometry IFromRhino(this object geometry)
        {
            return (geometry == null) ? null : Convert.FromRhino(geometry as dynamic);
        }


        /***************************************************/
        /**** Public Methods  - Vectors                 ****/
        /***************************************************/

        public static BHG.Point FromRhino(this RHG.Point3d rhinoPoint)
        {
            return new BHG.Point { X = rhinoPoint.X, Y = rhinoPoint.Y, Z = rhinoPoint.Z };
        }

        /***************************************************/

        public static BHG.Point FromRhino(this RHG.Point3f rhinoPoint)
        {
            return new BHG.Point { X = rhinoPoint.X, Y = rhinoPoint.Y, Z = rhinoPoint.Z };
        }

        /***************************************************/

        public static BHG.Point FromRhino(this RHG.Point rhinoPoint)
        {
            if (rhinoPoint == null) return null;

            return new BHG.Point { X = rhinoPoint.Location.X, Y = rhinoPoint.Location.Y, Z = rhinoPoint.Location.Z };
        }

        /***************************************************/

        public static BHG.Point FromRhino(this RHG.ControlPoint rhinoPoint)
        {
            return new BHG.Point { X = rhinoPoint.Location.X, Y = rhinoPoint.Location.Y, Z = rhinoPoint.Location.Z };
        }

        /***************************************************/

        public static BHG.Point FromRhino(this RHG.BrepVertex vertex)
        {
            return new BHG.Point { X = vertex.Location.X, Y = vertex.Location.Y, Z = vertex.Location.Z };
        }

        /***************************************************/

        public static BHG.Vector FromRhino(this RHG.Vector3d vector)
        {
            return new BHG.Vector { X = vector.X, Y = vector.Y, Z = vector.Z };
        }

        /***************************************************/

        public static BHG.Vector FromRhino(this RHG.Vector3f vector)
        {
            return new BHG.Vector { X = vector.X, Y = vector.Y, Z = vector.Z };
        }

        /***************************************************/

        public static BHG.CoordinateSystem.Cartesian FromRhino(this RHG.Plane plane)
        {
            return Geometry.Create.CartesianCoordinateSystem(plane.Origin.FromRhino(), plane.XAxis.FromRhino(), plane.YAxis.FromRhino());
        }

        /***************************************************/

        public static BHG.Quaternion FromRhino(this RHG.Quaternion quaternion)
        {
            return new BHG.Quaternion
            {
                X = quaternion.A,
                Y = quaternion.B,
                Z = quaternion.C,
                W = quaternion.D
            };
        }

        /***************************************************/

        public static BHG.TransformMatrix FromRhino(this RHG.Transform rhTrans)
        {
            BHG.TransformMatrix bhTrans = new BHG.TransformMatrix();
            bhTrans.Matrix[0, 0] = rhTrans[0, 0];
            bhTrans.Matrix[0, 1] = rhTrans[0, 1];
            bhTrans.Matrix[0, 2] = rhTrans[0, 2];
            bhTrans.Matrix[0, 3] = rhTrans[0, 3];

            bhTrans.Matrix[1, 0] = rhTrans[1, 0];
            bhTrans.Matrix[1, 1] = rhTrans[1, 1];
            bhTrans.Matrix[1, 2] = rhTrans[1, 2];
            bhTrans.Matrix[1, 3] = rhTrans[1, 3];

            bhTrans.Matrix[2, 0] = rhTrans[2, 0];
            bhTrans.Matrix[2, 1] = rhTrans[2, 1];
            bhTrans.Matrix[2, 2] = rhTrans[2, 2];
            bhTrans.Matrix[2, 3] = rhTrans[2, 3];

            bhTrans.Matrix[3, 0] = rhTrans[3, 0];
            bhTrans.Matrix[3, 1] = rhTrans[3, 1];
            bhTrans.Matrix[3, 2] = rhTrans[3, 2];
            bhTrans.Matrix[3, 3] = rhTrans[3, 3];

            return bhTrans;
        }


        /***************************************************/
        /**** Public Methods  - Curves                  ****/
        /***************************************************/

        public static BHG.Arc FromRhino(this RHG.Arc arc)
        {
            BHG.CoordinateSystem.Cartesian system = arc.Plane.FromRhino();

            return new BHG.Arc { CoordinateSystem = system, StartAngle = arc.StartAngle, EndAngle = arc.EndAngle, Radius = arc.Radius };
        }

        /***************************************************/

        public static BHG.ICurve FromRhino(this RHG.ArcCurve arcCurve)
        {
            if (arcCurve == null) return null;

            if (arcCurve.IsCompleteCircle)
            {
                RHG.Circle circle;
                arcCurve.TryGetCircle(out circle);
                return circle.FromRhino();
            }
            else
                return arcCurve.Arc.FromRhino();
        }

        /***************************************************/

        public static BHG.Circle FromRhino(this RHG.Circle circle)
        {
            return new BHG.Circle { Centre = circle.Center.FromRhino(), Normal = circle.Normal.FromRhino(), Radius = circle.Radius };
        }

        /***************************************************/

        public static BHG.Ellipse FromRhino(this RHG.Ellipse ellipse)
        {
            return new BHG.Ellipse
            {
                Centre = ellipse.Plane.Origin.FromRhino(),
                Axis1 = ellipse.Plane.XAxis.FromRhino(),
                Axis2 = ellipse.Plane.YAxis.FromRhino(),
                Radius1 = ellipse.Radius1,
                Radius2 = ellipse.Radius2
            };
        }

        /***************************************************/

        public static BHG.Line FromRhino(this RHG.Line line)
        {
            return new BHG.Line { Start = line.From.FromRhino(), End = line.To.FromRhino() };
        }

        /***************************************************/

        public static BHG.Line FromRhino(this RHG.LineCurve line)
        {
            if (line == null) return null;

            return new BHG.Line { Start = line.PointAtStart.FromRhino(), End = line.PointAtEnd.FromRhino() };
        }

        /***************************************************/

        public static BHG.ICurve FromRhino(this RHG.NurbsCurve rCurve)
        {
            if (rCurve == null) return null;

            if (rCurve.IsPolyline())
            {
                RHG.Polyline polyline;
                rCurve.TryGetPolyline(out polyline);
                return polyline.FromRhino();
            }

            if (rCurve.IsClosed && rCurve.IsEllipse())
            {
                RHG.Ellipse ellipse = new RHG.Ellipse();
                rCurve.TryGetEllipse(out ellipse);
                return ellipse.FromRhino();
            }

            IEnumerable<RHG.ControlPoint> rPoints = rCurve.Points;
            List<double> knots = rCurve.Knots.ToList();
            return new BHG.NurbsCurve
            {
                ControlPoints = rPoints.Select(x => x.FromRhino()).ToList(),
                Weights = rPoints.Select(x => x.Weight).ToList(),
                Knots = knots
            };
        }

        /***************************************************/

        public static BHG.ICurve FromRhino(this RHG.Curve rCurve)
        {
            if (rCurve == null) return null;

            Type curveType = rCurve.GetType();
            if (rCurve.IsLinear() && rCurve.SpanCount < 2)
            {
                return new BHG.Line { Start = rCurve.PointAtStart.FromRhino(), End = rCurve.PointAtEnd.FromRhino(), Infinite = false };
            }
            if (rCurve.IsCircle())
            {
                RHG.Circle circle = new RHG.Circle();
                rCurve.TryGetCircle(out circle);
                return circle.FromRhino();
            }
            else if (rCurve.IsArc() || typeof(RHG.ArcCurve).IsAssignableFrom(curveType))
            {
                RHG.Arc arc = new RHG.Arc();
                rCurve.TryGetArc(out arc);
                return arc.FromRhino();
            }
            else if (rCurve.IsPolyline() || typeof(RHG.PolylineCurve).IsAssignableFrom(curveType))
            {
                RHG.Polyline polyline = new RHG.Polyline();
                rCurve.TryGetPolyline(out polyline);
                return polyline.FromRhino();
            }
            else if (rCurve.IsClosed && rCurve.IsEllipse())
            {
                RHG.Ellipse ellipse = new RHG.Ellipse();
                rCurve.TryGetEllipse(out ellipse);
                return ellipse.FromRhino();
            }
            else if (rCurve is RHG.NurbsCurve)
            {
                return ((RHG.NurbsCurve)rCurve).FromRhino();
            }
            else if (rCurve is RHG.PolyCurve)
            {
                return ((RHG.PolyCurve)rCurve).FromRhino();  //The test of IsPolyline above is very important to make sure we can cast to PolyCurve here
            }
            else
            {
                return (rCurve.ToNurbsCurve()).FromRhino();
            }
        }

        /***************************************************/

        public static BHG.ICurve FromRhino(this RHG.PolyCurve polyCurve)
        {
            if (polyCurve == null) return null;

            polyCurve.RemoveNesting();
            if (polyCurve.IsPolyline())
            {
                RHG.Polyline polyline;
                polyCurve.TryGetPolyline(out polyline);
                return polyline.FromRhino();
            }
            else
                return new BHG.PolyCurve { Curves = polyCurve.Explode().Select(x => x.FromRhino()).ToList() };
        }

        /***************************************************/

        public static BHG.Polyline FromRhino(this RHG.Polyline polyline)
        {
            if (polyline == null) return null;

            return new BHG.Polyline { ControlPoints = polyline.Select(x => x.FromRhino()).ToList() };
        }

        /***************************************************/

        public static BHG.Polyline FromRhino(this RHG.PolylineCurve polyline)
        {
            if (polyline == null) return null;

            if (!polyline.IsPolyline()) { return null; }
            RHG.Polyline rPolyline; polyline.TryGetPolyline(out rPolyline);
            return rPolyline.FromRhino();
        }


        /***************************************************/
        /**** Public Methods  - Surfaces                ****/
        /***************************************************/

        public static BHG.BoundingBox FromRhino(this RHG.BoundingBox boundingBox)
        {
            return new BHG.BoundingBox { Min = boundingBox.Min.FromRhino(), Max = boundingBox.Max.FromRhino() };
        }

        /***************************************************/

        public static BHG.BoundingBox FromRhino(this RHG.Box box)
        {
            return box.BoundingBox.FromRhino();
        }

        /***************************************************/

        public static BHG.ISurface FromRhino(this RHG.Surface surface)
        {
            if (surface == null)
                return null;

            if (surface.IsPlanar(BHG.Tolerance.Distance))
            {
                BHG.ICurve externalEdge = RHG.Curve.JoinCurves(surface.ToBrep().DuplicateNakedEdgeCurves(true, false)).FirstOrDefault().FromRhino();
                return new BHG.PlanarSurface(externalEdge, new List<oM.Geometry.ICurve>());
            }

            return surface.ToNurbsSurface().FromRhino();
        }

        /***************************************************/

        public static BHG.ISurface FromRhino(this RHG.NurbsSurface surface)
        {
            if (surface == null)
                return null;

            if (surface.IsPlanar(BH.oM.Geometry.Tolerance.Distance))
            {
                BHG.ICurve externalEdge = RHG.Curve.JoinCurves(surface.ToBrep().DuplicateNakedEdgeCurves(true, false)).FirstOrDefault().FromRhino();
                return new BHG.PlanarSurface(externalEdge, new List<BHG.ICurve>());
            }

            BHG.NurbsSurface bhs = new BHG.NurbsSurface
            (
                surface.Points.Select(x => x.Location.FromRhino()).ToList(),
                surface.Points.Select(x => x.Weight).ToList(),
                surface.KnotsU.ToList(),
                surface.KnotsV.ToList(),
                surface.Degree(0),
                surface.Degree(1),
                new List<BHG.SurfaceTrim>(),
                new List<BHG.SurfaceTrim>()
            );

            return bhs;
        }

        /***************************************************/

        public static BHG.IGeometry FromRhino(this RHG.Brep brep)
        {
            if (brep == null)
                return null;

            string log;
            if (!brep.IsValidWithLog(out log))
            {
                Reflection.Compute.RecordError("Conversion failed, Rhino Brep is invalid: " + log);
                return null;
            }

            if (brep.Faces.Count == 0)
                return null;
            
            if (brep.IsSolid)
                return brep.ToBHoMSolid();
        
            if (brep.IsPlanarSurface())
            {
                BHG.ICurve externalEdge = RHG.Curve.JoinCurves(brep.DuplicateNakedEdgeCurves(true, false)).FirstOrDefault().FromRhino();
                List<BHG.ICurve> internalEdges = RHG.Curve.JoinCurves(brep.DuplicateNakedEdgeCurves(false, true)).Select(c => c.FromRhino()).ToList();
                return new BHG.PlanarSurface(externalEdge, internalEdges);
            }

            if (brep.Faces.Count == 1)
                return brep.Faces[0].FromRhino();

            // Default case - return open Polysurface
            return new BHG.PolySurface() { Surfaces = brep.Faces.Select(s => s.FromRhino()).ToList() };
        }

        /***************************************************/

        public static BHG.ISurface FromRhino(this RHG.BrepFace face)
        {
            if (face == null)
                return null;

            if (face.IsPlanar(BHG.Tolerance.Distance))
                return face.ToBHoMPlanarSurface();
            else
                return face.ToBHoMNurbsSurface();
        }

        /***************************************************/

        public static BHG.Extrusion FromRhino(this RHG.Extrusion extrusion)
        {
            if (extrusion == null) return null;

            extrusion.PathLineCurve();
            throw new NotImplementedException(); // TODO Rhino_Adapter conversion from Extrusion
        }


        /***************************************************/
        /**** Public Methods  - Mesh                    ****/
        /***************************************************/

        public static BHG.Mesh FromRhino(this RHG.Mesh rMesh)
        {
            if (rMesh == null) return null;

            List<BHG.Point> vertices = rMesh.Vertices.ToList().Select(x => x.FromRhino()).ToList();
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

        public static BHG.Face FromRhino(this RHG.MeshFace rFace)
        {

            BHG.Face face = new BHG.Face
            {
                A = rFace.A,
                B = rFace.B,
                C = rFace.C
            };

            if (rFace.IsQuad)
                face.D = rFace.D;

            return face;
        }

        /***************************************************/
        /**** Public Methods  - Solids                  ****/
        /***************************************************/

        public static BHG.Sphere FromRhino(this RHG.Sphere sphere)
        {
            return new BHG.Sphere { Centre = sphere.Center.FromRhino(), Radius = sphere.Radius };
        }

        /***************************************************/

        public static BHG.Torus FromRhino(this RHG.Torus torus)
        {
            return new BHG.Torus { Centre = torus.Plane.Origin.FromRhino(), Axis = torus.Plane.ZAxis.FromRhino(), RadiusMajor = torus.MajorRadius, RadiusMinor = torus.MinorRadius };
        }

        /***************************************************/

        public static BHG.Cone FromRhino(this RHG.Cone cone)
        {
            return new BHG.Cone { Centre = cone.BasePoint.FromRhino(), Axis = cone.Axis.FromRhino()*-1.0, Radius = cone.Radius, Height = cone.Height };
        }

        /***************************************************/

        public static BHG.Cylinder FromRhino(this RHG.Cylinder cylinder)
        {
            BHG.Point centre = cylinder.Center.FromRhino() + cylinder.Axis.FromRhino() * cylinder.Height1;
            return new BHG.Cylinder { Centre = centre, Axis = cylinder.Axis.FromRhino(), Height = cylinder.TotalHeight, Radius = cylinder.CircleAt(0.0).Radius };
        }


        /***************************************************/
        /**** Miscellanea                               ****/
        /***************************************************/

        public static BHG.CompositeGeometry FromRhino(this List<RHG.GeometryBase> geometries)
        {
            return new BHG.CompositeGeometry { Elements = geometries.Select(x => x.IFromRhino()).ToList() };
        }

        /***************************************************/

        public static BHG.SurfaceTrim FromRhino(this RHG.BrepLoop loop)
        {
            BHG.PolyCurve curve2d = new BHG.PolyCurve();
            BHG.PolyCurve curve3d = new BHG.PolyCurve();
            foreach (RHG.BrepTrim trim in loop.Trims)
            {
                curve2d.Curves.Add(trim.ToBHoMTrimCurve());
                RHG.Curve trim3d = loop.Face.Pushup(trim, BHG.Tolerance.Distance);
                curve3d.Curves.Add(trim3d.ToBHoMTrimCurve());
            }

            return new BHG.SurfaceTrim(curve3d, curve2d);
        }


        /***************************************************/
        /**** Private Methods                           ****/
        /***************************************************/


        private static BHG.ISolid ToBHoMSolid(this RHG.Brep brep)
        {
            RHG.Surface surface = brep.Surfaces.FirstOrDefault();
            switch (brep.Surfaces.Count)
            {
                case 1:
                    RHG.Sphere sphere;
                    if (surface.TryGetSphere(out sphere))
                        return sphere.FromRhino();
                    RHG.Torus torus;
                    if (surface.TryGetTorus(out torus))
                        return torus.FromRhino();
                    break;
                case 2:
                    RHG.Cone cone;
                    if (surface.TryGetCone(out cone))
                        return cone.FromRhino();
                    break;
                case 3:
                    RHG.Cylinder cylinder;
                    if (surface.TryGetCylinder(out cylinder))
                        return cylinder.FromRhino();
                    break;
            }

            return new BHG.BoundaryRepresentation(brep.Faces.Select(s => s.FromRhino()).ToList(), brep.GetVolume());
        }

        /***************************************************/

        private static BHG.PlanarSurface ToBHoMPlanarSurface(this RHG.BrepFace face)
        {
            RHG.Surface rhSurf = face.UnderlyingSurface();
            if (rhSurf == null)
                return null;

            BHG.PlanarSurface bhs = rhSurf.FromRhino() as BHG.PlanarSurface;
            List<BHG.ICurve> internalBoundaries = new List<BHG.ICurve>();
            BHG.ICurve externalBoundary = bhs.ExternalBoundary;
            foreach (RHG.BrepLoop loop in face.Loops)
            {
                if (loop.LoopType == RHG.BrepLoopType.Inner)
                    internalBoundaries.Add(new BHG.PolyCurve { Curves = loop.Trims.Select(x => rhSurf.Pushup(x, BHG.Tolerance.Distance).FromRhino()).ToList() });
                else if (loop.LoopType == RHG.BrepLoopType.Outer)
                    externalBoundary = new BHG.PolyCurve { Curves = loop.Trims.Select(x => rhSurf.Pushup(x, BHG.Tolerance.Distance).FromRhino()).ToList() };
            }

            return BH.Engine.Geometry.Create.PlanarSurface(externalBoundary, internalBoundaries);
        }

        /***************************************************/

        private static BHG.NurbsSurface ToBHoMNurbsSurface(this RHG.BrepFace face)
        {
            RHG.Surface rhSurf = face.UnderlyingSurface();
            if (rhSurf == null)
                return null;

            RHG.NurbsSurface rhNurbsSurf = rhSurf.ToNurbsSurface();
            List<BHG.SurfaceTrim> innerTrims = new List<BHG.SurfaceTrim>();
            List<BHG.SurfaceTrim> outerTrims = new List<BHG.SurfaceTrim>();

            foreach (RHG.BrepLoop loop in face.Loops)
            {
                if (loop.LoopType == RHG.BrepLoopType.Outer)
                    outerTrims.Add(loop.FromRhino());
                else
                    innerTrims.Add(loop.FromRhino());
            }

            return new BHG.NurbsSurface
            (
               rhNurbsSurf.Points.Select(x => x.Location.FromRhino()).ToList(),
               rhNurbsSurf.Points.Select(x => x.Weight).ToList(),
               rhNurbsSurf.KnotsU.ToList(),
               rhNurbsSurf.KnotsV.ToList(),
               rhNurbsSurf.Degree(0),
               rhNurbsSurf.Degree(1),
               innerTrims,
               outerTrims
            );
        }

        /***************************************************/

        private static BHG.ICurve ToBHoMTrimCurve(this RHG.Curve curve)
        {
            if (curve.IsArc())
            {
                RHG.Arc arc;
                curve.TryGetArc(out arc);
                return arc.FromRhino();
            }
            else
                return curve.FromRhino();
        }


        /***************************************************/
        /**** Fallback Methods                          ****/
        /***************************************************/

        public static BHG.IGeometry FromRhino(this object obj)
        {
            if (obj != null)
                Engine.Reflection.Compute.RecordError($"No conversion could be found between {obj.GetType().IToText()} and Rhino geometry.");

            return null;
        }

        /***************************************************/
    }
}
