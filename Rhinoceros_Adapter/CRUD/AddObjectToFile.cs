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
    public partial class RhinocerosAdapter : BHoMAdapter
    {

        /***************************************************/
        /**** Public Methods  - Interfaces              ****/
        /***************************************************/
        public void IAddObjectToFile(object objectToAdd, ObjectAttributes atttributes)
        {
            AddObjectToFile(objectToAdd as dynamic, atttributes);
        }

        /***************************************************/
        public void AddObjectToFile(Curve objectToAdd, ObjectAttributes atttributes)
        {
            m_File3dm.Objects.AddCurve(objectToAdd, atttributes);
        }

        /***************************************************/
        public void AddObjectToFile(Arc objectToAdd, ObjectAttributes atttributes)
        {
            m_File3dm.Objects.AddArc(objectToAdd, atttributes);
        }

        /***************************************************/
        public void AddObjectToFile(Brep objectToAdd, ObjectAttributes atttributes)
        {
            m_File3dm.Objects.AddBrep(objectToAdd, atttributes);
        }

        /***************************************************/
        public void AddObjectToFile(Circle objectToAdd, ObjectAttributes atttributes)
        {
            m_File3dm.Objects.AddCircle(objectToAdd, atttributes);
        }

        /***************************************************/
        public void AddObjectToFile(Ellipse objectToAdd, ObjectAttributes atttributes)
        {
            m_File3dm.Objects.AddEllipse(objectToAdd, atttributes);
        }

        /***************************************************/
        public void AddObjectToFile(Extrusion objectToAdd, ObjectAttributes atttributes)
        {
            m_File3dm.Objects.AddExtrusion(objectToAdd, atttributes);
        }

        /***************************************************/
        public void AddObjectToFile(Line objectToAdd, ObjectAttributes atttributes)
        {
            m_File3dm.Objects.AddLine(objectToAdd, atttributes);
        }

        /***************************************************/
        public void AddObjectToFile(Mesh objectToAdd, ObjectAttributes atttributes)
        {
            m_File3dm.Objects.AddMesh(objectToAdd, atttributes);
        }

        /***************************************************/
        public void AddObjectToFile(Point3d objectToAdd, ObjectAttributes atttributes)
        {
            m_File3dm.Objects.AddPoint(objectToAdd, atttributes);
        }

        /***************************************************/
        public void AddObjectToFile(PointCloud objectToAdd, ObjectAttributes atttributes)
        {
            m_File3dm.Objects.AddPointCloud(objectToAdd, atttributes);
        }

        /***************************************************/
        public void AddObjectToFile(BoundingBox objectToAdd, ObjectAttributes atttributes)
        {
            AddObjectToFile(objectToAdd.ToBrep(), atttributes);
        }
        /***************************************************/
        public void AddObjectToFile(IEnumerable<Point3d> objectToAdd, ObjectAttributes atttributes)
        {
            m_File3dm.Objects.AddPoints(objectToAdd, atttributes);
        }

        /***************************************************/
        public void AddObjectToFile(Sphere objectToAdd, ObjectAttributes atttributes)
        {
            m_File3dm.Objects.AddSphere(objectToAdd, atttributes);
        }

        /***************************************************/
        public void AddObjectToFile(Surface objectToAdd, ObjectAttributes atttributes)
        {
            m_File3dm.Objects.AddSurface(objectToAdd, atttributes);
        }

        /***************************************************/
        public void AddObjectToFile(Text3d objectToAdd, ObjectAttributes atttributes)
        {
            m_File3dm.Objects.AddText(objectToAdd, atttributes);
        }

        /***************************************************/
        /**** Private Methods  - Fallback               ****/
        /***************************************************/
        private void AddObjectToFile(object objectToAdd, ObjectAttributes atttributes)
        {
            Engine.Reflection.Compute.RecordError("Could not add object of type : " + objectToAdd.GetType().ToString() + " to the Rhino file.");
            return;
        }
    }
}
