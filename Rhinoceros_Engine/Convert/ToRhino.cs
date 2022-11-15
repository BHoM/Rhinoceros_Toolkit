/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2022, the respective contributors. All rights reserved.
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
using BH.Engine.Geometry;
using BH.oM.Base.Attributes;
using System.Drawing;
using BH.oM.Base;
using Rhino.Display;
using BH.oM.Graphics;
using System.ComponentModel;

namespace BH.Engine.Rhinoceros
{
    public static partial class Convert
    {
        /***************************************************/
        /**** Public Methods  - Interfaces              ****/
        /***************************************************/

        [Description("Returns the Rhino version of the geometry.")]
        [Input("geometry", "Input geometry.")]
        [Output("rhGeom", "Rhino object.")]
        public static object IToRhino(this BHG.IGeometry geometry)
        {
            return (geometry == default(BHG.IGeometry)) ? null : Convert.ToRhino(geometry as dynamic);
        }

        /***************************************************/

        [Description("Returns the Rhino version of the object.")]
        [Input("render", "Input object.")]
        [Output("rhGeom", "Rhino object.")]
        public static object IToRhino(this IRender render)
        {
            return (render == default(IRender)) ? null : Convert.ToRhino(render as dynamic);
        }

        /***************************************************/

        [Description("Returns the Rhino version of the object.")]
        [Input("curve", "Input curve.")]
        [Output("rhGeom", "Rhino object.")]
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

        [Description("Returns the Rhino version of the surface.")]
        [Input("surface", "Input surface.")]
        [Output("rhGeom", "Rhino object.")]
        public static RHG.GeometryBase IToRhino(this BHG.ISurface surface)
        {
            return (surface == default(BHG.ISurface)) ? null : Convert.ToRhino(surface as dynamic);
        }


        /***************************************************/
        /**** Public Methods  - Vectors                 ****/
        /***************************************************/

        [Description("Returns the Rhino version of the point.")]
        [Input("point", "Input point.")]
        [Output("rhGeom", "Rhino point.")]
        public static RHG.Point3d ToRhino(this BHG.Point point)
        {
            if (point == null) return default(RHG.Point3d);

            return new RHG.Point3d(point.X, point.Y, point.Z);
        }

        /***************************************************/

        [Description("Returns the Rhino version of the vector.")]
        [Input("vector", "Input vector.")]
        [Output("rhGeom", "Rhino vector.")]
        public static RHG.Vector3d ToRhino(this BHG.Vector vector)
        {
            if (vector == null) return default(RHG.Vector3d);

            return new RHG.Vector3d(vector.X, vector.Y, vector.Z);
        }

        /***************************************************/

        [Description("Returns the Rhino version of the plane.")]
        [Input("plane", "Input plane.")]
        [Output("rhGeom", "Rhino plane.")]
        public static RHG.Plane ToRhino(this BHG.Plane plane)
        {
            if (plane == null) return default(RHG.Plane);

            return new RHG.Plane(plane.Origin.ToRhino(), plane.Normal.ToRhino());
        }

        /***************************************************/

        [Description("Returns the Rhino version of the coordinateSystem.")]
        [Input("coordinateSystem", "Input coordinateSystem.")]
        [Output("rhGeom", "Rhino plane.")]
        public static RHG.Plane ToRhino(this BHG.CoordinateSystem.Cartesian coordinateSystem)
        {
            if (coordinateSystem == null) return default(RHG.Plane);

            return new RHG.Plane(coordinateSystem.Origin.ToRhino(), coordinateSystem.X.ToRhino(), coordinateSystem.Y.ToRhino());
        }

        /***************************************************/

        [Description("Returns the Rhino version of the quartenion.")]
        [Input("quartenion", "Input quartenion.")]
        [Output("rhGeom", "Rhino quartenion.")]
        public static RHG.Quaternion ToRhino(this BHG.Quaternion quartenion)
        {
            return (quartenion == null) ? default(RHG.Quaternion) : new RHG.Quaternion(quartenion.X, quartenion.Y, quartenion.Z, quartenion.W);
        }

        /***************************************************/

        [Description("Returns the Rhino version of the Transformation matrix.")]
        [Input("bhTrans", "Input TransformMatrix.")]
        [Output("rhGeom", "Rhino TransformMatrix.")]
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

        [Description("Returns the Rhino version of the arc.")]
        [Input("arc", "Input arc.")]
        [Output("rhGeom", "Rhino arc.")]
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

        [Description("Returns the Rhino version of the circle.")]
        [Input("circle", "Input circle.")]
        [Output("rhGeom", "Rhino circle.")]
        public static RHG.Circle ToRhino(this BHG.Circle circle)
        {
            if (circle == null) return default(RHG.Circle);

            return new RHG.Circle(new RHG.Plane(circle.Centre.ToRhino(), circle.Normal.ToRhino()), circle.Radius);
        }

        /***************************************************/

        [Description("Returns the Rhino version of the ellipse.")]
        [Input("ellipse", "Input ellipse.")]
        [Output("rhGeom", "Rhino ellipse.")]
        public static RHG.Ellipse ToRhino(this BHG.Ellipse ellipse)
        {
            if (ellipse == null) return default(RHG.Ellipse);

            RHG.Plane plane = new RHG.Plane(ellipse.Centre.ToRhino(), ellipse.Axis1.ToRhino(), ellipse.Axis2.ToRhino());
            return new RHG.Ellipse(plane, ellipse.Radius1, ellipse.Radius2);
        }

        /***************************************************/

        [Description("Returns the Rhino version of the line.")]
        [Input("line", "Input line.")]
        [Output("rhGeom", "Rhino line.")]
        public static RHG.Line ToRhino(this BHG.Line line)
        {
            if (line == null) return default(RHG.Line);

            return new RHG.Line(line.Start.ToRhino(), line.End.ToRhino());
        }

        /***************************************************/

        [PreviousVersion("6.0", "BH.Engine.Rhinoceros.Convert.ToRhino6(BH.oM.Geometry.NurbsCurve)")]
        [Description("Returns the Rhino version of the NurbsCurve.")]
        [Input("bCurve", "Input NurbsCurve.")]
        [Output("rhGeom", "Rhino NurbsCurve.")]
        public static RHG.NurbsCurve ToRhino(this BHG.NurbsCurve bCurve)
        {
            if (bCurve == null)
                return null;

            int version = Rhino.RhinoApp.Version.Major;

            //Difference in how weights are handled for NurbsCurve that was introduced in Version 6.
            //Filtering out special case for supported version Rhino5, and using new version for 6 and above.
            if(version < 5)
                throw new NotImplementedException($"Rhino {Rhino.RhinoApp.Version.ToString()} version not supported.");
            if (version == 5)
                return ToRhino5(bCurve);


            List<double> knots = bCurve.Knots;
            List<double> weights = bCurve.Weights;
            List<BHG.Point> ctrlPts = bCurve.ControlPoints;

            RHG.NurbsCurve rCurve = new RHG.NurbsCurve(3, false, bCurve.Degree() + 1, ctrlPts.Count);

            for (int i = 0; i < knots.Count; i++)
                rCurve.Knots[i] = knots[i];

            for (int i = 0; i < ctrlPts.Count; i++)
            {
                BHG.Point pt = ctrlPts[i] * weights[i];
                rCurve.Points.SetPoint(i, pt.X, pt.Y, pt.Z, weights[i]);
            }

            return rCurve;
        }

        /***************************************************/

        [Description("Returns the Rhino version of the PolyCurve.")]
        [Input("bPolyCurve", "Input PolyCurve.")]
        [Output("rhGeom", "Rhino PolyCurve.")]
        public static RHG.PolyCurve ToRhino(this BHG.PolyCurve bPolyCurve)
        {
            if (bPolyCurve == null)
                return null;

            //Curve is checked that it is a joined curve and only converted if that is the case.
            //Appending disjoined curves to a single Rhino polycurves ensures it is joined and by doing so changes the geometry.
            //Rhino Join is throwing access violation exceptions in some cases, leading to a full Rhino crash, that cannot be caught by a try-catch.
            //For this reason, the BHoM Join is used instead.
            if (BH.Engine.Geometry.Compute.IJoin(bPolyCurve.Curves).Count > 1)
                return null;    //Not returning error message here as that will lead to confusing messages on any method returning a disjointed Polycurve deemed valid in BHoM.

            IEnumerable<RHG.Curve> parts = bPolyCurve.Curves.Select(x => x.IToRhino());

            if (parts.Any(x => x == null))
                return null;

            RHG.PolyCurve rPolycurve = new RHG.PolyCurve();
            foreach (RHG.Curve curve in parts)
                rPolycurve.Append(curve);
            return rPolycurve;
        }

        /***************************************************/

        [Description("Returns the Rhino version of the polyline.")]
        [Input("polyline", "Input polyline.")]
        [Output("rhGeom", "Rhino polyline.")]
        public static RHG.PolylineCurve ToRhino(this BHG.Polyline polyline)
        {
            if (polyline == null) return null;

            return new RHG.PolylineCurve(polyline.ControlPoints.Select(x => x.ToRhino()));
        }


        /***************************************************/
        /**** Public Methods  - Surfaces                ****/
        /***************************************************/

        [Description("Returns the Rhino version of the boundingBox.")]
        [Input("boundingBox", "Input boundingBox.")]
        [Output("rhGeom", "Rhino boundingBox.")]
        public static RHG.BoundingBox ToRhino(this BHG.BoundingBox boundingBox)
        {
            if (boundingBox == null) return default(RHG.BoundingBox);

            return new RHG.BoundingBox(boundingBox.Min.ToRhino(), boundingBox.Max.ToRhino());
        }

        /***************************************************/

        [Description("Returns the Rhino version of the loft.")]
        [Input("loft", "Input loft.")]
        [Output("rhGeom", "Rhino Surface.")]
        public static RHG.Surface ToRhino(this BHG.Loft loft)
        {
            if (loft == null)
            {
                return null;
            }

            List<BHG.ICurve> bCurves = loft.Curves;
            if (bCurves.Count <= 1)
            {
                return null;
            }

            List<RHG.Curve> rCurves = new List<RHG.Curve>();
            for (int i = 0; i < bCurves.Count; i++)
            {
                rCurves.Add(bCurves[i].IToRhino());
            }
            bool isClosed = bCurves[0].IIsEqual(bCurves[bCurves.Count - 1]);
            RHG.Brep rloft = RHG.Brep.CreateFromLoft(rCurves, RHG.Point3d.Unset, RHG.Point3d.Unset, RHG.LoftType.Normal, isClosed).FirstOrDefault();

            return rloft?.Surfaces.FirstOrDefault();
        }

        /***************************************************/


        [Description("Returns the Rhino version of the pipe.")]
        [Input("pipe", "Input pipe.")]
        [Output("rhGeom", "Rhino Brep.")]
        public static RHG.Brep ToRhino(this BHG.Pipe pipe)
        {
            if (pipe == null)
            {
                return null;
            }
            else if (pipe.Centreline == null)
            {
                return null;
            }

            RHG.Curve rRail = pipe.Centreline.IToRhino();
            if (rRail == null)
            {
                return null;
            }

            RHG.PipeCapMode cap = (RHG.PipeCapMode)(pipe.Capped ? 1 : 0);
            bool fitRail = pipe.Centreline is BHG.PolyCurve;

            RHG.Brep[] rPipes = RHG.Brep.CreatePipe(rRail, pipe.Radius, false, cap, fitRail, BHG.Tolerance.Distance, BHG.Tolerance.Angle);

            return rPipes.FirstOrDefault();
        }

        /***************************************************/

        [Description("Returns the Rhino version of the planarSurface.")]
        [Input("planarSurface", "Input planarSurface.")]
        [Output("rhGeom", "Rhino Brep.")]
        public static RHG.Brep ToRhino(this BHG.PlanarSurface planarSurface)
        {
            if (planarSurface == null || planarSurface.ExternalBoundary == null)
                return null;

            RHG.Curve externalCurve = planarSurface.ExternalBoundary.IToRhino();
            if (externalCurve == null || !externalCurve.IsPlanar(BHG.Tolerance.Distance))
                return null;

            List<RHG.Curve> rhCurves = new List<RHG.Curve>();
            if (planarSurface.InternalBoundaries != null)
            {
                rhCurves.AddRange(planarSurface.InternalBoundaries.Select(c => c.IToRhino()).Where(c => c.IsPlanar(BHG.Tolerance.Distance)).ToList());
                if (rhCurves.Count < planarSurface.InternalBoundaries.Count)
                {
                    int skipped = planarSurface.InternalBoundaries.Count - rhCurves.Count;
                    Base.Compute.RecordWarning($"{skipped} internal boundaries skipped due to a failed planarity test.");
                }
            }
            rhCurves.Add(externalCurve);

            RHG.Brep[] rhSurfaces = RHG.Brep.CreatePlanarBreps(rhCurves);
            if (rhSurfaces == null)
                return null;

            if (rhSurfaces.Length > 1)
            {
                //If more than one surface is extracted, try boolean difference of the curves to generate the geometry
                List<RHG.Curve> inner = new List<RHG.Curve>(rhCurves);
                inner.RemoveAt(inner.Count - 1);

                RHG.Curve[] difference = RHG.Curve.CreateBooleanDifference(externalCurve, inner);
                //Internal and external edges fully overlap -> 0 edges -> empty Brep
                if (difference == null || difference.Length == 0)
                    return null;

                RHG.Brep[] rhSurfacesFromDifference = RHG.Brep.CreatePlanarBreps(difference);
                if (rhSurfacesFromDifference == null)
                    return null;

                if (rhSurfacesFromDifference.Length > 1)
                {
                    Base.Compute.RecordWarning("Surface edges are not coplanar or their intersection is not empty." +
                                                     "The conversion to Rhino results into multiple Breps and only the first brep will be returned.");
                }
                else if (rhSurfacesFromDifference.Length == 1)
                {
                    Base.Compute.RecordWarning("The internal edges overlap with the external." +
                        "Boolean intersection has been used to try to get out the correct geometry." +
                        "Topology might have changed for the surface obejct");
                    return rhSurfacesFromDifference.FirstOrDefault();
                }
            }
            return rhSurfaces.FirstOrDefault();
        }

        /***************************************************/

        [Description("Returns the Rhino version of the surface.")]
        [Input("surface", "Input surface.")]
        [Output("rhGeom", "Rhino GeometryBase.")]
        public static RHG.GeometryBase ToRhino(this BHG.NurbsSurface surface)
        {
            if (surface == null)
                return null;

            List<int> uvCount = surface.UVCount();
            RHG.NurbsSurface rhSurface = RHG.NurbsSurface.Create(3, true, surface.UDegree + 1, surface.VDegree + 1, uvCount[0], uvCount[1]);
            for (int i = 0; i < rhSurface.KnotsU.Count; i++)
                rhSurface.KnotsU[i] = surface.UKnots[i];
            for (int i = 0; i < rhSurface.KnotsV.Count; i++)
                rhSurface.KnotsV[i] = surface.VKnots[i];
            for (int i = 0; i < uvCount[0]; i++)
                for (int j = 0; j < uvCount[1]; j++)
                    rhSurface.Points.SetControlPoint(i, j, new RHG.ControlPoint(surface.ControlPoints[j + (uvCount[1] * i)].ToRhino(), surface.Weights[j + (uvCount[1] * i)]));

            if (!rhSurface.IsValid)
                return null;

            if (surface.OuterTrims.Count == 0 && surface.InnerTrims.Count == 0)
                return rhSurface;
            else
            {
                RHG.Brep brep = new RHG.Brep();
                int srf = brep.AddSurface(rhSurface);
                RHG.BrepFace face = brep.Faces.Add(srf);

                foreach (BHG.SurfaceTrim trim in surface.OuterTrims)
                {
                    brep.AddBrepTrim(face, trim, RHG.BrepLoopType.Outer);
                }

                foreach (BHG.SurfaceTrim trim in surface.InnerTrims)
                {
                    brep.AddBrepTrim(face, trim, RHG.BrepLoopType.Inner);
                }

                return brep.IsValid ? brep : null;
            }
        }

        /***************************************************/

        [Description("Returns the Rhino version of the polySurface.")]
        [Input("polySurface", "Input polySurface.")]
        [Output("rhGeom", "Rhino Brep.")]
        public static RHG.Brep ToRhino(this BHG.PolySurface polySurface)
        {
            return polySurface.Surfaces.ToRhino();
        }

        /***************************************************/

        [Description("Returns the Rhino version of the boundaryRepresentation.")]
        [Input("boundaryRepresentation", "Input boundaryRepresentation.")]
        [Output("rhGeom", "Rhino Brep.")]
        public static RHG.Brep ToRhino(this BHG.BoundaryRepresentation boundaryRepresentation)
        {
            return boundaryRepresentation.Surfaces.ToList().ToRhino();
        }

        /***************************************************/

        [Description("Returns the Rhino version of the surfaces.")]
        [Input("surfaces", "Input surfaces.")]
        [Output("rhGeom", "Rhino Brep.")]
        public static RHG.Brep ToRhino(this List<BHG.ISurface> surfaces)
        {
            RHG.Brep brep = new RHG.Brep();

            for (int i = 0; i < surfaces.Count; i++)
            {
                RHG.GeometryBase geo = surfaces[i].IToRhino();
                if (geo is RHG.Surface) brep.AddSurface((RHG.Surface)geo);
                else if (geo is RHG.Brep) brep.Append((RHG.Brep)geo);
            }
            brep.JoinNakedEdges(BHG.Tolerance.Distance);

            return brep;
        }

        /***************************************************/

        [Description("Returns the Rhino version of the extrusion.")]
        [Input("extrusion", "Input extrusion.")]
        [Output("rhGeom", "Rhino Surface.")]
        public static Rhino.Geometry.Surface ToRhino(this BHG.Extrusion extrusion)
        {
            if (!extrusion.Curve.IIsPlanar())
            {
                BH.Engine.Base.Compute.RecordError("The provided BHoM Extrusion has a base curve that is not planar.");
                return null;
            }

            var planarCurve = extrusion.Curve.IToRhino();

            RHG.Plane curvePlane;
            planarCurve.TryGetPlane(out curvePlane);

            double angle = RHG.Vector3d.VectorAngle(curvePlane.Normal, extrusion.Direction.ToRhino());

            double tolerance = 0.001;

            if (angle < tolerance || (2 * Math.PI - tolerance < angle && angle < 2 * Math.PI + tolerance))
            {
                // It can be represented by a Rhino extrusion (which enforces perpendicularity btw Curve plane and Vector)

                double extrHeight = extrusion.Direction.Length();

                if (angle > Math.PI)
                    extrHeight = -extrHeight;

                RHG.Extrusion extr = Rhino.Geometry.Extrusion.Create(planarCurve, extrHeight, extrusion.Capped);

                return extr;
            }

            // Otherwise, provide a Sweep to cover extrusion with a base curve that is not orthogonal to the extr direction

            // Create a Line to be the sweep rail. Use centroid/mid-point of base curve as start point.
            RHG.Point3d centrePoint;
            if (planarCurve.IsClosed)
            {
                var areaProp = Rhino.Geometry.AreaMassProperties.Compute(planarCurve);
                centrePoint = areaProp.Centroid;
            }
            else
                centrePoint = planarCurve.PointAt(0.5);

            RHG.Point3d endPoint = centrePoint + extrusion.Direction.ToRhino();
            var rail = new RHG.LineCurve(centrePoint, endPoint);

            var joinedSweep = new RHG.SweepOneRail()
                .PerformSweep(rail, planarCurve)
                .Aggregate((b1, b2) => { b1.Join(b2, tolerance, true); return b1; });

            if (joinedSweep.IsSurface)
                return joinedSweep.Surfaces[0];

            BH.Engine.Base.Compute.RecordError("Could not convert this BHoM Extrusion to a Rhino Surface. The extrusion direction is not perpendicular to the base curve, and the base curve is too complex for a Sweep to return a valid Surface.");


            return null;
        }


        /***************************************************/
        /**** Public Methods  - Mesh                    ****/
        /***************************************************/

        [Description("Returns the Rhino version of the mesh.")]
        [Input("mesh", "Input mesh.")]
        [Output("rhGeom", "Rhino mesh.")]
        public static RHG.Mesh ToRhino(this BHG.Mesh mesh)
        {
            if (mesh == null) return null;

            List<RHG.Point3d> rVertices = mesh.Vertices.Select(x => x.ToRhino()).ToList();
            List<BHG.Face> faces = mesh.Faces;
            List<RHG.MeshFace> rFaces = new List<RHG.MeshFace>();
            int nbVertices = rVertices.Count;
            for (int i = 0; i < faces.Count; i++)
            {
                BHG.Face face = faces[i];
                if (face.IsQuad())
                {

                    if (face.A < nbVertices && face.B < nbVertices && face.C < nbVertices && face.D < nbVertices)
                        rFaces.Add(new RHG.MeshFace(face.A, face.B, face.C, face.D));
                    else
                        Base.Compute.RecordWarning("Mesh face [" + face.A + ", " + face.B + ", " + face.C + ", " + face.D + "] could not be created due to corresponding vertices missing");
                }
                else
                {
                    if (face.A < nbVertices && face.B < nbVertices && face.C < nbVertices)
                        rFaces.Add(new RHG.MeshFace(face.A, face.B, face.C));
                    else
                        Base.Compute.RecordWarning("Mesh face [" + face.A + ", " + face.B + ", " + face.C + "] could not be created due to corresponding vertices missing");
                }
            }
            RHG.Mesh rMesh = new RHG.Mesh();
            rMesh.Faces.AddFaces(rFaces);
            rMesh.Vertices.AddVertices(rVertices);
            return rMesh;
        }

        /***************************************************/

        [Description("Returns the Rhino version of the Mesh Face.")]
        [Input("rFace", "Input mesh face.")]
        [Output("rhGeom", "Rhino MeshFace.")]
        public static RHG.MeshFace ToRhino(this BHG.Face rFace)
        {
            if (rFace == null) return default(RHG.MeshFace);

            if (rFace.IsQuad())
                return new RHG.MeshFace(rFace.A, rFace.B, rFace.C, rFace.D);
            else
                return new RHG.MeshFace(rFace.A, rFace.B, rFace.C);
        }

        /***************************************************/

        [Description("Returns the Rhino version of the RenderMesh.")]
        [Input("mesh", "Input RenderMesh.")]
        [Output("rhGeom", "Rhino Mesh.")]
        public static RHG.Mesh ToRhino(this BH.oM.Graphics.RenderMesh mesh)
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
            Color[] colors = mesh.Vertices.Select(x => x.Colour).ToArray();
            rMesh.VertexColors.SetColors(colors);
            return rMesh;
        }

        /***************************************************/

        [Description("Returns the Rhino version of the mesh3d.")]
        [Input("mesh3d", "Input mesh3d.")]
        [Output("rhGeom", "Rhino Mesh.")]
        public static RHG.Mesh ToRhino(this BHG.Mesh3D mesh3d)
        {
            return mesh3d?.ToMesh().ToRhino();
        }

        /***************************************************/

        [Description("Returns the Rhino version of the cellrelation.")]
        [Input("cellrelation", "Input cellrelation.")]
        [Output("rhGeom", "Rhino GeometryBase.")]
        public static RHG.GeometryBase ToRhino(this BHG.CellRelation cellrelation)
        {
            // No Rhino equivalant and nothing meaningful to convert to.
            return null;
        }

        /***************************************************/
        /**** Public Methods  - Solids                  ****/
        /***************************************************/

        [Description("Returns the Rhino version of the sphere.")]
        [Input("sphere", "Input sphere.")]
        [Output("rhGeom", "Rhino sphere.")]
        public static RHG.Sphere ToRhino(this BHG.Sphere sphere)
        {
            if (sphere == null) return default(RHG.Sphere);

            return new RHG.Sphere(sphere.Centre.ToRhino(), sphere.Radius);
        }

        /***************************************************/

        [Description("Returns the Rhino version of the torus.")]
        [Input("torus", "Input torus.")]
        [Output("rhGeom", "Rhino torus.")]
        public static RHG.Torus ToRhino(this BHG.Torus torus)
        {
            if (torus == null) return default(RHG.Torus);

            if (torus.RadiusMajor <= torus.RadiusMinor)
            {
                Base.Compute.RecordError("Major Radius less than or equal to Minor Radius. Conversion to Rhino Torus failed.");
                return RHG.Torus.Unset;
            }

            RHG.Plane plane = new RHG.Plane(torus.Centre.ToRhino(), torus.Axis.ToRhino());

            return new RHG.Torus(plane, torus.RadiusMajor, torus.RadiusMinor);
        }

        /***************************************************/

        [Description("Returns the Rhino version of the cylinder.")]
        [Input("cylinder", "Input cylinder.")]
        [Output("rhGeom", "Rhino cylinder.")]
        public static RHG.Cylinder ToRhino(this BHG.Cylinder cylinder)
        {
            if (cylinder == null) return default(RHG.Cylinder);

            RHG.Plane plane = new RHG.Plane(cylinder.Centre.ToRhino(), cylinder.Axis.ToRhino());
            RHG.Circle circle = new RHG.Circle(plane, cylinder.Radius);

            return new RHG.Cylinder(circle, cylinder.Height);
        }

        /***************************************************/

        [Description("Returns the Rhino version of the cone.")]
        [Input("cone", "Input cone.")]
        [Output("rhGeom", "Rhino cone.")]
        public static RHG.Cone ToRhino(this BHG.Cone cone)
        {
            if (cone == null) return default(RHG.Cone);

            BHG.Vector axis = cone.Axis * -1.0;
            RHG.Plane plane = new RHG.Plane((cone.Centre + cone.Axis.Normalise() * cone.Height).ToRhino(), axis.ToRhino());

            return new RHG.Cone(plane, cone.Height, cone.Radius);
        }

        /***************************************************/

        [Description("Returns the Rhino version of the cuboid.")]
        [Input("cuboid", "Input cuboid.")]
        [Output("rhGeom", "Rhino cuboid.")]
        public static RHG.Box ToRhino(this BHG.Cuboid cuboid)
        {
            if (cuboid == null) return default(RHG.Box);

            RHG.Interval ix = new RHG.Interval((cuboid.Length / -2.0), (cuboid.Length / 2.0));
            RHG.Interval iy = new RHG.Interval((cuboid.Depth / -2.0), (cuboid.Depth / 2.0));
            RHG.Interval iz = new RHG.Interval((cuboid.Height / -2.0), (cuboid.Height / 2.0));

            return new RHG.Box(cuboid.CoordinateSystem.ToRhino(), ix, iy, iz);
        }

        /***************************************************/
        /**** Public Methods - IRender                  ****/
        /***************************************************/

        [Description("Returns the Rhino version of the renderText.")]
        [Input("renderText", "Input renderText.")]
        [Output("rhGeom", "Rhino renderGeo.")]
        public static Text3d ToRhino(this RenderText renderText)
        {
            if (renderText == null) return null;

            RHG.Vector3d xdir = (RHG.Vector3d)renderText.Cartesian.X.IToRhino();
            RHG.Vector3d ydir = (RHG.Vector3d)renderText.Cartesian.Y.IToRhino();
            RHG.Point3d pos = (RHG.Point3d)renderText.Cartesian.Origin.IToRhino();
            RHG.Plane textPlane = new RHG.Plane(pos, xdir, ydir);
            Text3d text3D = new Text3d(renderText.Text, textPlane, renderText.Height);

            if (renderText.FontName.Contains("Italic"))
                text3D.Italic = true;

            if (renderText.FontName.Contains("Bold"))
                text3D.Bold = true;

            text3D.FontFace = renderText.FontName.Replace("Italic", "").Replace("Bold", "").Trim();

            return text3D;
        }

        /***************************************************/


        [Description("Returns the Rhino version of the renderGeo.")]
        [Input("renderGeo", "Input renderGeo.")]
        [Output("rhGeom", "Rhino object.")]
        public static object ToRhino(this RenderGeometry renderGeo)
        {
            if (renderGeo.Geometry == null) return null;

            return ToRhino(renderGeo.Geometry as dynamic);
        }

        /***************************************************/


        [Description("Returns the Rhino version of the inner Curve of the RenderCurve.")]
        [Input("renderCurve", "Input RenderCurve.")]
        [Output("rhGeom", "Rhino object.")]
        public static object ToRhino(this RenderCurve renderCurve)
        {
            if (renderCurve?.Curve == null) return null;

            return ToRhino(renderCurve.Curve as dynamic);
        }

        /***************************************************/

        [Description("Returns the Rhino version of the Texture.")]
        [Input("texture", "Input BHoM Texture.")]
        [Output("rhMat", "The Rhino display material.")]
        public static DisplayMaterial ToRhino(this BH.oM.Graphics.Texture texture)
        {
            if (texture == null)
                return null;

            DisplayMaterial material = new DisplayMaterial(texture.Diffuse, texture.Specular, texture.Ambient, texture.Emission, texture.Shine, texture.Transparency);
            if (!string.IsNullOrWhiteSpace(texture.BitmapTexture) && System.IO.File.Exists(texture.BitmapTexture))
            {
                material.SetBitmapTexture(texture.BitmapTexture, true);
            }
            return material;
        }

        /***************************************************/
        /**** Miscellanea                               ****/
        /***************************************************/

        [Description("Returns the Rhino version of the CompositeGeometry.")]
        [Input("geometries", "Input CompositeGeometry.")]
        [Output("rhGeom", "Rhino object.")]
        public static List<object> ToRhino(this BHG.CompositeGeometry geometries)
        {
            if (geometries == null) return new List<object>();

            return geometries.Elements.Select(x => x.IToRhino()).ToList();
        }

        /***************************************************/
        /**** Private methods                           ****/
        /***************************************************/

        private static void AddBrepTrim(this RHG.Brep brep, RHG.BrepFace face, BHG.SurfaceTrim trim, RHG.BrepLoopType loopType)
        {
            RHG.BrepLoop loop = brep.Loops.Add(loopType, face);
            List<BHG.ICurve> subParts3d = trim.Curve3d.ISubParts().ToList();
            List<BHG.ICurve> subParts2d = trim.Curve2d.ISubParts().ToList();

            double rhinoTolerance = Query.DocumentTolerance();

            for (int i = 0; i < subParts3d.Count; i++)
            {
                BHG.ICurve bhc = subParts3d[i];
                RHG.Curve rhc = bhc.IToRhino();

                int startId = brep.AddVertex(rhc.PointAtStart);
                int endId = brep.AddVertex(rhc.PointAtEnd);

                RHG.BrepTrim rhTrim;
                RHG.Curve rhc2d = subParts2d[i].IToRhino();
                rhc2d.ChangeDimension(2);
                int crv2d = brep.Curves2D.Add(rhc2d);
                if (rhc.IsValid)
                {
                    bool rev3d = true;
                    RHG.BrepEdge edge = null;
                    foreach (RHG.BrepEdge e in brep.Edges)
                    {
                        if (e.StartVertex.VertexIndex == endId && e.EndVertex.VertexIndex == startId && rhc.IsSameEdge(e))
                        {
                            edge = e;
                            break;
                        }
                    }

                    if (edge == null)
                    {
                        int crv = brep.Curves3D.Add(rhc);
                        edge = brep.Edges.Add(startId, endId, crv, rhinoTolerance);
                        rev3d = false;
                    }

                    rhTrim = brep.Trims.Add(edge, rev3d, loop, crv2d);
                }
                else
                    rhTrim = brep.Trims.AddSingularTrim(brep.Vertices[startId], loop, RHG.IsoStatus.None, crv2d);

                rhTrim.SetTolerances(rhinoTolerance, rhinoTolerance);

                //TODO: In Rhino 6 this could be replaced with Surface.IsIsoParametric(Curve)
                RHG.Point3d start = rhc2d.PointAtStart;
                RHG.Point3d end = rhc2d.PointAtEnd;

                if (rhc2d.IsLinear())
                {
                    if (Math.Abs(start.X - end.X) <= rhinoTolerance)
                    {
                        RHG.Interval domainU = brep.Surfaces[face.SurfaceIndex].Domain(0);
                        if (Math.Abs(start.X - domainU.Min) <= rhinoTolerance)
                            rhTrim.IsoStatus = RHG.IsoStatus.West;
                        else if (Math.Abs(start.X - domainU.Max) <= rhinoTolerance)
                            rhTrim.IsoStatus = RHG.IsoStatus.East;
                        else
                            rhTrim.IsoStatus = RHG.IsoStatus.X;
                    }
                    else if (Math.Abs(start.Y - end.Y) <= rhinoTolerance)
                    {
                        RHG.Interval domainV = brep.Surfaces[face.SurfaceIndex].Domain(1);
                        if (Math.Abs(start.Y - domainV.Min) <= rhinoTolerance)
                            rhTrim.IsoStatus = RHG.IsoStatus.South;
                        else if (Math.Abs(start.Y - domainV.Max) <= rhinoTolerance)
                            rhTrim.IsoStatus = RHG.IsoStatus.North;
                        else
                            rhTrim.IsoStatus = RHG.IsoStatus.Y;
                    }
                }
            }
        }

        /***************************************************/

        private static int AddVertex(this RHG.Brep brep, RHG.Point3d point)
        {
            int id = -1;
            for (int j = 0; j < brep.Vertices.Count; j++)
            {
                if (point.DistanceTo(brep.Vertices[j].Location) <= BHG.Tolerance.Distance)
                {
                    id = j;
                    break;
                }
            }

            if (id == -1)
            {
                brep.Vertices.Add(point, BHG.Tolerance.Distance);
                id = brep.Vertices.Count - 1;
            }

            return id;
        }

        /***************************************************/

        private static bool IsSameEdge(this RHG.Curve curve, RHG.BrepEdge edge)
        {
            double tolerance = BHG.Tolerance.Distance;
            RHG.Curve edgeCurve = edge.DuplicateCurve();
            edgeCurve.Reverse();

            RHG.BoundingBox bb1 = curve.GetBoundingBox(false);
            RHG.BoundingBox bb2 = edgeCurve.GetBoundingBox(false);
            if (bb1.Min.DistanceTo(bb2.Min) > tolerance || bb1.Max.DistanceTo(bb2.Max) > tolerance)
                return false;

            int frameCount = 100;
            RHG.Point3d[] frames1, frames2;
            curve.DivideByCount(frameCount, false, out frames1);
            edgeCurve.DivideByCount(frameCount, false, out frames2);

            return Enumerable.Range(0, frameCount - 1).All(i => frames1[i].DistanceTo(frames2[i]) <= tolerance);
        }

        /***************************************************/

        private static RHG.Point3d ToRhino(this BH.oM.Graphics.RenderPoint point)
        {
            if (point == null) return default(RHG.Point3d);

            return new RHG.Point3d(point.Point.X, point.Point.Y, point.Point.Z);
        }

        /***************************************************/
        /**** Private Methods - Fallback                ****/
        /***************************************************/

        private static object ToRhino(this IObject obj)
        {
            return null;
        }

    }
}



