/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2021, the respective contributors. All rights reserved.
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

using Rhino.Display;
using Rhino.DocObjects;
using Rhino.FileIO;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Adapter.Rhinoceros
{
    public static partial class Convert 
    {
        /***************************************************/
        /**** Public Methods  - Interfaces              ****/
        /***************************************************/
        public static void IAddObjectToFile(object objectToAdd, File3dm file3Dm, ObjectAttributes atttributes)
        {
            AddObjectToFile(objectToAdd as dynamic, file3Dm, atttributes);
        }

        /***************************************************/
        public static void AddObjectToFile(Curve objectToAdd, File3dm file3Dm, ObjectAttributes atttributes)
        {
            file3Dm.Objects.AddCurve(objectToAdd, atttributes);
        }

        /***************************************************/
        public static void AddObjectToFile(Arc objectToAdd, File3dm file3Dm, ObjectAttributes atttributes)
        {
            file3Dm.Objects.AddArc(objectToAdd, atttributes);
        }

        /***************************************************/
        public static void AddObjectToFile(Brep objectToAdd, File3dm file3Dm, ObjectAttributes atttributes)
        {
            file3Dm.Objects.AddBrep(objectToAdd, atttributes);
        }

        /***************************************************/
        public static void AddObjectToFile(Circle objectToAdd, File3dm file3Dm, ObjectAttributes atttributes)
        {
            file3Dm.Objects.AddCircle(objectToAdd, atttributes);
        }

        /***************************************************/
        public static void AddObjectToFile(Ellipse objectToAdd, File3dm file3Dm, ObjectAttributes atttributes)
        {
            file3Dm.Objects.AddEllipse(objectToAdd, atttributes);
        }

        /***************************************************/
        public static void AddObjectToFile(Extrusion objectToAdd, File3dm file3Dm, ObjectAttributes atttributes)
        {
            file3Dm.Objects.AddExtrusion(objectToAdd, atttributes);
        }

        /***************************************************/
        public static void AddObjectToFile(Line objectToAdd, File3dm file3Dm, ObjectAttributes atttributes)
        {
            file3Dm.Objects.AddLine(objectToAdd, atttributes);
        }

        /***************************************************/
        public static void AddObjectToFile(Mesh objectToAdd, File3dm file3Dm, ObjectAttributes atttributes)
        {
            file3Dm.Objects.AddMesh(objectToAdd, atttributes);
        }

        /***************************************************/
        public static void AddObjectToFile(Point3d objectToAdd, File3dm file3Dm, ObjectAttributes atttributes)
        {
            file3Dm.Objects.AddPoint(objectToAdd, atttributes);
        }

        /***************************************************/
        public static void AddObjectToFile(PointCloud objectToAdd, File3dm file3Dm, ObjectAttributes atttributes)
        {
            file3Dm.Objects.AddPointCloud(objectToAdd, atttributes);
        }

        /***************************************************/
        public static void AddObjectToFile(IEnumerable<Point3d> objectToAdd, File3dm file3Dm, ObjectAttributes atttributes)
        {
            file3Dm.Objects.AddPoints(objectToAdd, atttributes);
        }

        /***************************************************/
        public static void AddObjectToFile(Sphere objectToAdd, File3dm file3Dm, ObjectAttributes atttributes)
        {
            file3Dm.Objects.AddSphere(objectToAdd, atttributes);
        }

        /***************************************************/
        public static void AddObjectToFile(Surface objectToAdd, File3dm file3Dm, ObjectAttributes atttributes)
        {
            file3Dm.Objects.AddSurface(objectToAdd, atttributes);
        }

        /***************************************************/
        public static void AddObjectToFile(Text3d objectToAdd, File3dm file3Dm, ObjectAttributes atttributes)
        {
            file3Dm.Objects.AddText(objectToAdd, atttributes);
        }

        /***************************************************/
        /**** Private Methods  - Fallback               ****/
        /***************************************************/
        private static void AddObjectToFile(object objectToAdd, File3dm file3Dm, ObjectAttributes atttributes)
        {
            return;
        }
    }
}
