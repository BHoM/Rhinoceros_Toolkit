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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.oM.Base;
using BH.oM.Adapter;
using Rhino.FileIO;
using System.IO;
using BH.oM.Adapters.Rhinoceros;
using BH.Engine.Rhinoceros;
using BH.Engine.Adapter;

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
            FindFilesToRead();
            objects = Read3dm(m_FilePaths);
            return objects;
        }

        /***************************************************/

        private static List<IBHoMObject> Read3dm(string filePath)
        {
            List<IBHoMObject> objects = new List<IBHoMObject>();
            File3dm file3Dm = File3dm.Read(filePath);
            string name = Path.GetFileName(filePath);

            foreach (File3dmObject item in file3Dm.Objects)
            {
                Rhino.DocObjects.Layer layer = file3Dm.Layers[item.Attributes.LayerIndex];
                BHoMRhinoObject rhinoObject = new BHoMRhinoObject();
                rhinoObject.Layer = name + "::" + layer.Name;
                rhinoObject.LayerColour = layer.Color;
                rhinoObject.ObjectColour = item.Attributes.ObjectColor;

                if (item.Attributes.ColorSource == Rhino.DocObjects.ObjectColorSource.ColorFromLayer)
                    rhinoObject.ColourSource = ColourSource.ByLayer;

                else
                    rhinoObject.ColourSource = ColourSource.ByObject;

                rhinoObject.Geometry = BH.Engine.Rhinoceros.Convert.IFromRhino(item.Geometry);
                objects.Add(rhinoObject);
            }
            return objects;
        }

        /***************************************************/

        private List<IBHoMObject> Read3dm(List<string> filePaths)
        {
            List<IBHoMObject> objects = new List<IBHoMObject>();

            foreach (string path in filePaths)
                objects.AddRange(Read3dm(path));

            return objects;
        }

        /***************************************************/

        private static void FindFilesToRead()
        {
            if (_fileSettings.FileName == "" && _fileSettings.Directory == "")
            {
                BH.Engine.Reflection.Compute.RecordError("Either provide a file name and directory to pull a single .3dm file, or a directory to pull multiple .3dm files.");
                return;
            }

            //no directory but full path provided in filename
            if (_fileSettings.FileName != "" && _fileSettings.Directory == "")
            {
                if (!Path.HasExtension(_fileSettings.FileName) || Path.GetExtension(_fileSettings.FileName) != ".3dm")
                {
                    BH.Engine.Reflection.Compute.RecordError("File name must contain a file .3dm extension.");
                    return;
                }
                else if (!File.Exists(_fileSettings.FileName))
                {
                    BH.Engine.Reflection.Compute.RecordError("File does not exist.");
                    return;
                }
                else
                {
                    m_FilePaths.Add(_fileSettings.GetFullFileName());
                }

            }
            //check the directory
            else if (!Directory.Exists(_fileSettings.Directory))
            {
                BH.Engine.Reflection.Compute.RecordError("Directory provided does not exist.");
                return;
            }
            else
            {
                string[] files = Directory.GetFiles(_fileSettings.Directory, "*.3dm");
                if (files.Length == 0)
                {
                    BH.Engine.Reflection.Compute.RecordError("No .3dm files found in the directory.");
                    return;
                }
                else
                {
                    if (_fileSettings.FileName != "" && !files.Any(f => f.Contains(Path.GetFileName(_fileSettings.FileName))))
                    {
                        BH.Engine.Reflection.Compute.RecordError("File specified was not found in the directory.");
                        return;
                    }

                    else if (_fileSettings.FileName != "" && files.Any(f => f.Contains(Path.GetFileName(_fileSettings.FileName))))
                        m_FilePaths.Add(_fileSettings.GetFullFileName());

                    else
                        m_FilePaths = files.ToList();
                }
            }
        }
    }
}
