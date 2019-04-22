/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2018, the respective contributors. All rights reserved.
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
using BH.oM.Reflection.Attributes;

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

        public static RHG.GeometryBase IToRhino(this BHG.ISurface surface)
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

        public static RHG.Plane ToRhino(this BHG.CoordinateSystem.Cartesian coordinateSystem)
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

        public static RHG.NurbsCurve ToRhino(this BHG.NurbsCurve bCurve)
        {
            if (bCurve == null) return null;

            List<double> knots = bCurve.Knots;
            List<double> weights = bCurve.Weights;
            List<BHG.Point> ctrlPts = bCurve.ControlPoints;

            RHG.NurbsCurve rCurve = new RHG.NurbsCurve(3, false, bCurve.Degree() + 1, ctrlPts.Count);

            for (int i = 0; i < knots.Count; i++)
                rCurve.Knots[i] = knots[i];

            for (int i = 0; i < ctrlPts.Count; i++)
            {
                BHG.Point pt = ctrlPts[i];
                rCurve.Points.SetPoint(i, pt.X, pt.Y, pt.Z, weights[i]);
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

        public static RHG.NurbsSurface ToRhino(this BHG.NurbsSurface surface)
        {
            List<int> uvCount = surface.UVCount();
            List<int> degrees = surface.Degrees();
            return RHG.NurbsSurface.CreateFromPoints(surface.ControlPoints.Select(x => x.ToRhino()),
                uvCount[0], uvCount[1], degrees[0], degrees[1]);
        }

        /***************************************************/

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

        public static RHG.Brep ToRhino(this BHG.PlanarSurface planarSurface)
        {
            if (planarSurface == null || planarSurface.ExternalBoundary == null)
                return null;

            RHG.Curve externalCurve = planarSurface.ExternalBoundary.IToRhino();

            if (externalCurve == null || !externalCurve.IsPlanar())
                return null;

            List<RHG.Curve> rhCurves = new List<RHG.Curve>();
            if (planarSurface.InternalBoundaries != null)
            {
                rhCurves.AddRange(planarSurface.InternalBoundaries.Select(c => c.IToRhino()).Where(c => c.IsPlanar()).ToList());
                if (rhCurves.Count < planarSurface.InternalBoundaries.Count)
                {
                    int skipped = planarSurface.InternalBoundaries.Count - rhCurves.Count;
                    Reflection.Compute.RecordWarning($"{skipped} internal boundaries skipped due to a failed planarity test.");
                }
            }
            rhCurves.Add(externalCurve);

            RHG.Brep[] rhSurfaces = RHG.Brep.CreatePlanarBreps(rhCurves);
            if (rhSurfaces.Length > 1)
            {
                //If more than one surface is extracted, try boolean difference of the curves to generate the geometry
                List<RHG.Curve> inner = new List<RHG.Curve>(rhCurves);
                inner.RemoveAt(inner.Count - 1);

                RHG.Curve[] difference = RHG.Curve.CreateBooleanDifference(externalCurve, inner);

                //Internal and external edges fully overlap -> 0 edges -> empty Brep
                if (difference.Length == 0)
                    return new RHG.Brep();

                RHG.Brep[] rhSurfacesFromDifference = RHG.Brep.CreatePlanarBreps(difference);

                if (rhSurfacesFromDifference.Length > 1)
                {
                    Reflection.Compute.RecordWarning("Surface edges are not coplanar or their intersection is not empty." +
                                                     "The conversion to Rhino results into multiple Breps and only the first brep will be returned.");
                }
                else if(rhSurfacesFromDifference.Length == 1)
                {
                    Reflection.Compute.RecordWarning("The internal edges overlap with the external. Boolean intersection has been preformed to try to get out the correct geometry. Topology might have changed for the surface obejct");
                    return rhSurfacesFromDifference.FirstOrDefault();
                }
            }
            return rhSurfaces.FirstOrDefault();
        }

        /***************************************************/

        public static RHG.Brep ToRhino(this BHG.PolySurface polySurface)
        {
            return polySurface.Surfaces.ToRhino();
        }

        /***************************************************/

        public static RHG.Brep ToRhino(this BHG.BoundaryRepresentation boundaryRepresentation)
        {
            return boundaryRepresentation.Surfaces.ToList().ToRhino();
        }

        /***************************************************/

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

        [NotImplemented]
        public static RHG.Extrusion ToRhino(this BHG.Extrusion extrusion)
        {
            // TODO Rhino_Adapter conversion to Extrusion
            Engine.Reflection.Compute.RecordError("The operation is not implemented");
            return null;
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
        /**** Public Methods  - Solids                  ****/
        /***************************************************/

        public static RHG.Sphere ToRhino(this BHG.Sphere sphere)
        {
            if (sphere == null) return default(RHG.Sphere);

            return new RHG.Sphere(sphere.Centre.ToRhino(), sphere.Radius);
        }

        /***************************************************/

        public static RHG.Torus ToRhino(this BHG.Torus torus)
        {
            if (torus == null) return default(RHG.Torus);

            RHG.Plane plane = new RHG.Plane(torus.Centre.ToRhino(), torus.Axis.ToRhino());

            return new RHG.Torus(plane, torus.RadiusMajor, torus.RadiusMinor);
        }

        /***************************************************/

        public static RHG.Cylinder ToRhino(this BHG.Cylinder cylinder)
        {
            if (cylinder == null) return default(RHG.Cylinder);

            RHG.Plane plane = new RHG.Plane(cylinder.Centre.ToRhino(), cylinder.Axis.ToRhino());
            RHG.Circle circle = new RHG.Circle(plane, cylinder.Radius);

            return new RHG.Cylinder(circle,cylinder.Height);
        }

        /***************************************************/

        public static RHG.Cone ToRhino(this BHG.Cone cone)
        {
            if (cone == null) return default(RHG.Cone);

            RHG.Plane plane = new RHG.Plane(cone.Centre.ToRhino(), cone.Axis.ToRhino());
            
            return new RHG.Cone(plane, cone.Height, cone.Radius);
        }

        /***************************************************/

        public static RHG.Box ToRhino(this BHG.Cuboid cuboid)
        {
            if (cuboid == null) return default(RHG.Box);

            RHG.Interval ix = new RHG.Interval((cuboid.Length / -2.0), (cuboid.Length / 2.0));
            RHG.Interval iy = new RHG.Interval((cuboid.Depth / -2.0), (cuboid.Depth / 2.0));
            RHG.Interval iz = new RHG.Interval((cuboid.Height / -2.0), (cuboid.Height / 2.0));

            return new RHG.Box(cuboid.CoordinateSystem.ToRhino(),ix,iy,iz);
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
