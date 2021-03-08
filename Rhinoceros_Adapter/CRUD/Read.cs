﻿/*
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.oM.Base;
using BH.oM.Adapter;
using Rhino.FileIO;
using System.IO;
using Rhino.DocObjects;
using BHR = BH.oM.Adapters.Rhinoceros;
using BH.Engine.Rhinoceros;
using BH.Engine.Adapter;
using BH.Engine.Reflection;

namespace BH.Adapter.Rhinoceros
{
    public partial class RhinocerosAdapter : BHoMAdapter
    {
        /***************************************************/
        /**** Adapter overload method                   ****/
        /***************************************************/

        protected override IEnumerable<IBHoMObject> IRead(Type type, IList ids = null, ActionConfig actionConfig = null)
        {
            List<IBHoMObject> objects = new List<IBHoMObject>();

            if (!File.Exists(m_Filepath))
            {
                BH.Engine.Reflection.Compute.RecordError("File does not exist. Check filename and directory have been correctly specified.");
                return objects;
            }
            if (Path.GetExtension(m_Filepath) != ".3dm")
            {
                BH.Engine.Reflection.Compute.RecordError("Rhinoceros Adapter can only operate on files with extension *.3dm.");
                return objects;
            }

            //TODO handle object filtering     
            objects = Read3dm();
            return objects;
        }

        /***************************************************/
        /**** Private methods                           ****/
        /***************************************************/

        private List<IBHoMObject> Read3dm()
        {
            m_File3dm = File3dm.Read(m_Filepath);

            List<IBHoMObject> objects = new List<IBHoMObject>();

            foreach (File3dmObject item in m_File3dm.Objects)
            {
                BHR.RhinoObject rhinoObject = new BHR.RhinoObject();

                rhinoObject.Layer = m_File3dm.Layers[item.Attributes.LayerIndex].FromRhino();

                rhinoObject.ObjectColour = item.Attributes.ObjectColor;

                rhinoObject.ColourSource = item.Attributes.ColorSource.FromRhino();

                rhinoObject.Geometry = BH.Engine.Rhinoceros.Convert.IFromRhino(item.Geometry);

                objects.Add(rhinoObject);
            }
            return objects;
        }

        
    }
}
