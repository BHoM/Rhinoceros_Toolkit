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
using System.Linq;
using RHG = Rhino.Geometry;
using BHG = BH.oM.Geometry;
using BH.Engine.Base;
using BH.oM.Base.Attributes;
using System.ComponentModel;

namespace BH.Engine.Adapters.Rhinoceros
{
    public static partial class Convert
    {
        /***************************************************/
        /**** Public Methods  - Interfaces              ****/
        /***************************************************/

        [Description("Converts a Rhino GeometryBase object to its BHoM IGeometry equivalent.")]
        [Input("geometry", "Rhino GeometryBase to convert.")]
        [Output("geometry", "BHoM IGeometry equivalent.")]
        public static BHG.IGeometry IFromRhino(this RHG.GeometryBase geometry)
        {
            return (geometry == null) ? null : Convert.FromRhino(geometry as dynamic);
        }

        /***************************************************/

        [Description("Converts a Rhino IEpsilonComparable geometry object to its BHoM IGeometry equivalent.")]
        [Input("geometry", "Rhino IEpsilonComparable geometry to convert.")]
        [Output("geometry", "BHoM IGeometry equivalent.")]
        public static BHG.IGeometry IFromRhino<T>(this Rhino.IEpsilonComparable<T> geometry)
        {
            return (geometry == null) ? null : Convert.FromRhino(geometry as dynamic);
        }

        /***************************************************/

        [Description("Converts a Rhino geometry object to its BHoM IGeometry equivalent.")]
        [Input("geometry", "Rhino geometry object to convert.")]
        [Output("geometry", "BHoM IGeometry equivalent.")]
        public static BHG.IGeometry IFromRhino(this object geometry)
        {
            return (geometry == null) ? null : Convert.FromRhino(geometry as dynamic);
        }


        /***************************************************/
        /**** Public Methods  - Vectors                 ****/
        /***************************************************/

        [Description("Converts a Rhino Point3d to a BHoM Point.")]
        [Input("rhinoPoint", "Rhino Point3d to convert.")]
        [Output("point", "BHoM Point equivalent.")]
        public static BHG.Point FromRhino(this RHG.Point3d rhinoPoint)
        {
            return new BHG.Point { X = rhinoPoint.X, Y = rhinoPoint.Y, Z = rhinoPoint.Z };
        }

        /***************************************************/

        [Description("Converts a Rhino Point3f to a BHoM Point.")]
        [Input("rhinoPoint", "Rhino Point3f to convert.")]
        [Output("point", "BHoM Point equivalent.")]
        public static BHG.Point FromRhino(this RHG.Point3f rhinoPoint)
        {
            return new BHG.Point { X = rhinoPoint.X, Y = rhinoPoint.Y, Z = rhinoPoint.Z };
        }

        /***************************************************/

        [Description("Converts a Rhino Point to a BHoM Point.")]
        [Input("rhinoPoint", "Rhino Point to convert.")]
        [Output("point", "BHoM Point equivalent.")]
        public static BHG.Point FromRhino(this RHG.Point rhinoPoint)
        {
            if (rhinoPoint == null) return null;

            return new BHG.Point { X = rhinoPoint.Location.X, Y = rhinoPoint.Location.Y, Z = rhinoPoint.Location.Z };
        }

        /***************************************************/

        [Description("Converts a Rhino ControlPoint to a BHoM Point.")]
        [Input("rhinoPoint", "Rhino ControlPoint to convert.")]
        [Output("point", "BHoM Point equivalent.")]
        public static BHG.Point FromRhino(this RHG.ControlPoint rhinoPoint)
        {
            return new BHG.Point { X = rhinoPoint.Location.X, Y = rhinoPoint.Location.Y, Z = rhinoPoint.Location.Z };
        }

        /***************************************************/

        [Description("Converts a Rhino BrepVertex to a BHoM Point.")]
        [Input("vertex", "Rhino BrepVertex to convert.")]
        [Output("point", "BHoM Point equivalent.")]
        public static BHG.Point FromRhino(this RHG.BrepVertex vertex)
        {
            return new BHG.Point { X = vertex.Location.X, Y = vertex.Location.Y, Z = vertex.Location.Z };
        }

        /***************************************************/

        [Description("Converts a Rhino Vector3d to a BHoM Vector.")]
        [Input("vector", "Rhino Vector3d to convert.")]
        [Output("vector", "BHoM Vector equivalent.")]
        public static BHG.Vector FromRhino(this RHG.Vector3d vector)
        {
            return new BHG.Vector { X = vector.X, Y = vector.Y, Z = vector.Z };
        }

        /***************************************************/

        [Description("Converts a Rhino Vector3f to a BHoM Vector.")]
        [Input("vector", "Rhino Vector3f to convert.")]
        [Output("vector", "BHoM Vector equivalent.")]
        public static BHG.Vector FromRhino(this RHG.Vector3f vector)
        {
            return new BHG.Vector { X = vector.X, Y = vector.Y, Z = vector.Z };
        }

        /***************************************************/

        [Description("Converts a Rhino Plane to a BHoM Cartesian coordinate system.")]
        [Input("plane", "Rhino Plane to convert.")]
        [Output("coordinateSystem", "BHoM Cartesian coordinate system equivalent.")]
        public static BHG.CoordinateSystem.Cartesian FromRhino(this RHG.Plane plane)
        {
            return Geometry.Create.CartesianCoordinateSystem(plane.Origin.FromRhino(), plane.XAxis.FromRhino(), plane.YAxis.FromRhino());
        }

        /***************************************************/

        [Description("Converts a Rhino Quaternion to a BHoM Quaternion.")]
        [Input("quaternion", "Rhino Quaternion to convert.")]
        [Output("quaternion", "BHoM Quaternion equivalent.")]
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

        [Description("Converts a Rhino Transform to a BHoM TransformMatrix.")]
        [Input("rhTrans", "Rhino Transform to convert.")]
        [Output("transformMatrix", "BHoM TransformMatrix equivalent.")]
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

        [Description("Converts a Rhino Arc to a BHoM Arc.")]
        [Input("arc", "Rhino Arc to convert.")]
        [Output("arc", "BHoM Arc equivalent.")]
        public static BHG.Arc FromRhino(this RHG.Arc arc)
        {
            BHG.CoordinateSystem.Cartesian system = arc.Plane.FromRhino();

            return new BHG.Arc { CoordinateSystem = system, StartAngle = arc.StartAngle, EndAngle = arc.EndAngle, Radius = arc.Radius };
        }

        /***************************************************/

        [Description("Converts a Rhino ArcCurve to a BHoM ICurve (Arc or Circle).")]
        [Input("arcCurve", "Rhino ArcCurve to convert.")]
        [Output("curve", "BHoM ICurve equivalent.")]
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

        [Description("Converts a Rhino Circle to a BHoM Circle.")]
        [Input("circle", "Rhino Circle to convert.")]
        [Output("circle", "BHoM Circle equivalent.")]
        public static BHG.Circle FromRhino(this RHG.Circle circle)
        {
            return new BHG.Circle { Centre = circle.Center.FromRhino(), Normal = circle.Normal.FromRhino(), Radius = circle.Radius };
        }

        /***************************************************/

        [Description("Converts a Rhino Ellipse to a BHoM Ellipse.")]
        [Input("ellipse", "Rhino Ellipse to convert.")]
        [Output("ellipse", "BHoM Ellipse equivalent.")]
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

        [Description("Converts a Rhino Line to a BHoM Line.")]
        [Input("line", "Rhino Line to convert.")]
        [Output("line", "BHoM Line equivalent.")]
        public static BHG.Line FromRhino(this RHG.Line line)
        {
            return new BHG.Line { Start = line.From.FromRhino(), End = line.To.FromRhino() };
        }

        /***************************************************/

        [Description("Converts a Rhino LineCurve to a BHoM Line.")]
        [Input("line", "Rhino LineCurve to convert.")]
        [Output("line", "BHoM Line equivalent.")]
        public static BHG.Line FromRhino(this RHG.LineCurve line)
        {
            if (line == null) return null;

            return new BHG.Line { Start = line.PointAtStart.FromRhino(), End = line.PointAtEnd.FromRhino() };
        }

        /***************************************************/

        [Description("Converts a Rhino NurbsCurve to a BHoM ICurve.")]
        [Input("rCurve", "Rhino NurbsCurve to convert.")]
        [Output("curve", "BHoM ICurve equivalent.")]
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

        [Description("Converts a Rhino Curve to its BHoM ICurve equivalent, dispatching to the most specific type.")]
        [Input("rCurve", "Rhino Curve to convert.")]
        [Output("curve", "BHoM ICurve equivalent.")]
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

        [Description("Converts a Rhino PolyCurve to a BHoM ICurve.")]
        [Input("polyCurve", "Rhino PolyCurve to convert.")]
        [Output("curve", "BHoM ICurve equivalent.")]
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

        [Description("Converts a Rhino Polyline to a BHoM Polyline.")]
        [Input("polyline", "Rhino Polyline to convert.")]
        [Output("polyline", "BHoM Polyline equivalent.")]
        public static BHG.Polyline FromRhino(this RHG.Polyline polyline)
        {
            if (polyline == null) return null;

            return new BHG.Polyline { ControlPoints = polyline.Select(x => x.FromRhino()).ToList() };
        }

        /***************************************************/

        [Description("Converts a Rhino PolylineCurve to a BHoM Polyline.")]
        [Input("polyline", "Rhino PolylineCurve to convert.")]
        [Output("polyline", "BHoM Polyline equivalent.")]
        public static BHG.Polyline FromRhino(this RHG.PolylineCurve polyline)
        {
            if (polyline == null) return null;

            if (!polyline.IsPolyline()) { return null; }
            RHG.Polyline rPolyline; polyline.TryGetPolyline(out rPolyline);
            return rPolyline.FromRhino();
        }

        /***************************************************/

        private static BHG.Polyline FromRhino(this RHG.Rectangle3d rectangle)
        {
            return FromRhino(rectangle.ToPolyline());
        }


        /***************************************************/
        /**** Public Methods  - Surfaces                ****/
        /***************************************************/

        [Description("Converts a Rhino BoundingBox to a BHoM BoundingBox.")]
        [Input("boundingBox", "Rhino BoundingBox to convert.")]
        [Output("boundingBox", "BHoM BoundingBox equivalent.")]
        public static BHG.BoundingBox FromRhino(this RHG.BoundingBox boundingBox)
        {
            return new BHG.BoundingBox { Min = boundingBox.Min.FromRhino(), Max = boundingBox.Max.FromRhino() };
        }

        /***************************************************/

        [Description("Converts a Rhino Box to a BHoM BoundingBox.")]
        [Input("box", "Rhino Box to convert.")]
        [Output("boundingBox", "BHoM BoundingBox equivalent.")]
        public static BHG.BoundingBox FromRhino(this RHG.Box box)
        {
            return box.BoundingBox.FromRhino();
        }

        /***************************************************/

        [Description("Converts a Rhino Surface to a BHoM ISurface.")]
        [Input("surface", "Rhino Surface to convert.")]
        [Output("surface", "BHoM ISurface equivalent.")]
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

        [Description("Converts a Rhino NurbsSurface to a BHoM ISurface.")]
        [Input("surface", "Rhino NurbsSurface to convert.")]
        [Output("surface", "BHoM ISurface equivalent.")]
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

        [Description("Converts a Rhino Brep to its BHoM IGeometry equivalent.")]
        [Input("brep", "Rhino Brep to convert.")]
        [Output("geometry", "BHoM IGeometry equivalent.")]
        public static BHG.IGeometry FromRhino(this RHG.Brep brep)
        {
            if (brep == null)
                return null;

            string log;
            if (!brep.IsValidWithLog(out log))
            {
                Base.Compute.RecordError("Conversion failed, Rhino Brep is invalid: " + log);
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

        [Description("Converts a Rhino BrepFace to a BHoM ISurface.")]
        [Input("face", "Rhino BrepFace to convert.")]
        [Output("surface", "BHoM ISurface equivalent.")]
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

        [Description("Converts a Rhino Extrusion to its BHoM IGeometry equivalent.")]
        [Input("extrusion", "Rhino Extrusion to convert.")]
        [Output("geometry", "BHoM IGeometry equivalent.")]
        public static BHG.IGeometry FromRhino(this RHG.Extrusion extrusion)
        {
            if (extrusion == null) return null;

            RHG.LineCurve line = extrusion.PathLineCurve();
            BHG.Vector extrVec = BH.Engine.Geometry.Create.Vector(line.PointAtStart.FromRhino(), line.PointAtEnd.FromRhino());

            List<BHG.Extrusion> extrs = new List<BHG.Extrusion>();

            // Exploits the fact that GetWireframe returns first the "profile" curves of the extrusion.
            var profileCurves = extrusion.GetWireframe();
            for (int i = 0; i < extrusion.ProfileCount; i++)
            {

                var profileConverted = profileCurves.ElementAt(i).FromRhino();
                BHG.Extrusion extr = BH.Engine.Geometry.Create.Extrusion(profileConverted, extrVec, extrusion.IsCappedAtBottom && extrusion.IsCappedAtTop);

                extrs.Add(extr);
            }


            if (extrs.Count == 1)
                return extrs[0];

            if (extrs.Count > 1)
                return new BH.oM.Geometry.CompositeGeometry() { Elements = extrs.OfType<BH.oM.Geometry.IGeometry>().ToList() };

            BH.Engine.Base.Compute.RecordError("Could not convert the extrusion.");
            return null;
        }


        /***************************************************/
        /**** Public Methods  - Mesh                    ****/
        /***************************************************/

        [Description("Converts a Rhino Mesh to a BHoM Mesh.")]
        [Input("rMesh", "Rhino Mesh to convert.")]
        [Output("mesh", "BHoM Mesh equivalent.")]
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

        [Description("Converts a Rhino MeshFace to a BHoM Face.")]
        [Input("rFace", "Rhino MeshFace to convert.")]
        [Output("face", "BHoM Face equivalent.")]
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

        [Description("Converts a Rhino Sphere to a BHoM Sphere.")]
        [Input("sphere", "Rhino Sphere to convert.")]
        [Output("sphere", "BHoM Sphere equivalent.")]
        public static BHG.Sphere FromRhino(this RHG.Sphere sphere)
        {
            return new BHG.Sphere { Centre = sphere.Center.FromRhino(), Radius = sphere.Radius };
        }

        /***************************************************/

        [Description("Converts a Rhino Torus to a BHoM Torus.")]
        [Input("torus", "Rhino Torus to convert.")]
        [Output("torus", "BHoM Torus equivalent.")]
        public static BHG.Torus FromRhino(this RHG.Torus torus)
        {
            return new BHG.Torus { Centre = torus.Plane.Origin.FromRhino(), Axis = torus.Plane.ZAxis.FromRhino(), RadiusMajor = torus.MajorRadius, RadiusMinor = torus.MinorRadius };
        }

        /***************************************************/

        [Description("Converts a Rhino Cone to a BHoM Cone.")]
        [Input("cone", "Rhino Cone to convert.")]
        [Output("cone", "BHoM Cone equivalent.")]
        public static BHG.Cone FromRhino(this RHG.Cone cone)
        {
            return new BHG.Cone { Centre = cone.BasePoint.FromRhino(), Axis = cone.Axis.FromRhino() * -1.0, Radius = cone.Radius, Height = cone.Height };
        }

        /***************************************************/

        [Description("Converts a Rhino Cylinder to a BHoM Cylinder.")]
        [Input("cylinder", "Rhino Cylinder to convert.")]
        [Output("cylinder", "BHoM Cylinder equivalent.")]
        public static BHG.Cylinder FromRhino(this RHG.Cylinder cylinder)
        {
            BHG.Point centre = cylinder.Center.FromRhino() + cylinder.Axis.FromRhino() * cylinder.Height1;
            return new BHG.Cylinder { Centre = centre, Axis = cylinder.Axis.FromRhino(), Height = cylinder.TotalHeight, Radius = cylinder.CircleAt(0.0).Radius };
        }


        /***************************************************/
        /**** Miscellanea                               ****/
        /***************************************************/

        [Description("Converts a list of Rhino GeometryBase objects to a BHoM CompositeGeometry.")]
        [Input("geometries", "List of Rhino GeometryBase objects to convert.")]
        [Output("compositeGeometry", "BHoM CompositeGeometry equivalent.")]
        public static BHG.CompositeGeometry FromRhino(this List<RHG.GeometryBase> geometries)
        {
            return new BHG.CompositeGeometry { Elements = geometries.Select(x => x.IFromRhino()).ToList() };
        }

        /***************************************************/

        [Description("Converts a Rhino BrepLoop to a BHoM SurfaceTrim.")]
        [Input("loop", "Rhino BrepLoop to convert.")]
        [Output("surfaceTrim", "BHoM SurfaceTrim equivalent.")]
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

        [Description("Fallback conversion method for objects that do not have a specific FromRhino overload.")]
        [Input("obj", "Object to attempt conversion from.")]
        [Output("geometry", "BHoM IGeometry if the object is already a BHoM geometry, otherwise null.")]
        public static BHG.IGeometry FromRhino(this object obj)
        {
            BHG.IGeometry geom = obj as BHG.IGeometry;

            if (geom != null)
                return geom;

            if (obj != null)
                Engine.Base.Compute.RecordError($"No conversion could be found between {obj.GetType().IToText()} and Rhino geometry.");

            return null;
        }

        /***************************************************/
    }
}






