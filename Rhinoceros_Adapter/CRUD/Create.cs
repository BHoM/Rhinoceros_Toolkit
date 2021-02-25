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
using System.Text;
using System.Threading.Tasks;
using BH.oM.Base;
using BH.oM.Adapter;
using BH.oM.Geometry;
using BH.oM.Adapters.Rhinoceros;
using BH.Engine.Reflection;
using BH.oM.Reflection;
using BH.Engine.Rhinoceros;
using Rhino.FileIO;
using Rhino.DocObjects;
using BH.Engine.Adapter;
using System.IO;

namespace BH.Adapter.Rhinoceros
{
    public partial class RhinocerosAdapter : BHoMAdapter
    {
   
        protected override bool ICreate<T>(IEnumerable<T> objects, ActionConfig actionConfig = null)
        {
			bool success = true;

            if (actionConfig == null)
            {
                BH.Engine.Reflection.Compute.RecordError("Please provide configuration settings to push to a Rhinoceros file.");
                return false;
            }

            RhinocerosConfig config = actionConfig as RhinocerosConfig;
            if (config == null)
            {
                BH.Engine.Reflection.Compute.RecordError("Please provide valid a RhinocerosConfig object for pushing to a Rhinoceros file.");
                return false;
            }

            if(!OKToCreateFile())
                return false;

            success = CreateRhinoceros(objects, config);

            return success;  
        }

        /***************************************************/

        private bool CreateRhinoceros<T>(IEnumerable<T> objects, RhinocerosConfig config)
        {
            bool success = true;
            File3dm file3Dm = new File3dm();
            List<Layer> layers = new List<Layer>();
            Layer layer1 = new Layer();
            layer1.Name = "default";
            layers.Add(layer1);
            Dictionary<object, ObjectAttributes> objectDict = new Dictionary<object, ObjectAttributes>();

            foreach (T obj in objects)
            {
                ObjectAttributes attributes = new ObjectAttributes();
                object rhinoGeometry = null;

                if (obj is IGeometry)
                {
                    attributes.LayerIndex = 0;
                    rhinoGeometry = BH.Engine.Rhinoceros.Convert.ToRhino(obj as dynamic);
                    objectDict.Add(rhinoGeometry, attributes);
                }

                else if(obj is BHoMRhinoObject)
                {
                    BHoMRhinoObject bhomRhino = obj as BHoMRhinoObject;
                    int layerIndex = layers.FindIndex(l => l.Name == bhomRhino.Layer);

                    if(layerIndex == -1)
                    {
                        Layer layer = new Layer();
                        layer.Name = bhomRhino.Layer;
                        layer.Color = bhomRhino.LayerColour;
                        layers.Add(layer);
                        layerIndex = layers.Count - 1;
                    }

                    attributes.LayerIndex = layerIndex;

                    if (bhomRhino.ColourSource == ColourSource.ByLayer)
                        attributes.ColorSource = Rhino.DocObjects.ObjectColorSource.ColorFromLayer;
                    else
                    {
                        attributes.ColorSource = Rhino.DocObjects.ObjectColorSource.ColorFromObject;
                        attributes.ObjectColor = bhomRhino.ObjectColour;
                    }
                        
                    rhinoGeometry = BH.Engine.Rhinoceros.Convert.ToRhino(bhomRhino.Geometry as dynamic);

                    objectDict.Add(rhinoGeometry, attributes);
                }
                else
                {
                    BH.Engine.Reflection.Compute.RecordError("Unable to write objects of type: " + obj.GetType().ToString());
                }
            }

            AddLayers(layers, file3Dm);
            AddObjects(objectDict, file3Dm);

            file3Dm.Polish();
            file3Dm.Write(m_RhinoceroSettings.GetFullFileName() , config.Version);

            return success;
        }


        /***************************************************/

        private static void AddLayers(List<Layer> layers, File3dm file3Dm)
        {
            foreach(Layer layer in layers)
            {
                file3Dm.Layers.Add(layer);
            }
        }

        /***************************************************/

        private static void AddObjects(Dictionary<object, ObjectAttributes> objectDict, File3dm file3Dm)
        {
            foreach(var objAtt in objectDict)
            {
                Convert.IAddObjectToFile(objAtt.Key, file3Dm, objAtt.Value);
            }
        }

        /***************************************************/

        private static bool OKToCreateFile()
        {
            if (File.Exists(m_RhinoceroSettings.GetFullFileName()))
            {
                BH.Engine.Reflection.Compute.RecordError("File exists. Adding to file or overwriting not yet implemented. Specify a new file name.");
                return false;
            }
            //check the directory
            if (!Directory.Exists(m_RhinoceroSettings.Directory))
            {
                BH.Engine.Reflection.Compute.RecordError("Directory provided does not exist.");
                return false;
            }
            return true;
        }

        /***************************************************/
        protected bool Create(IBHoMObject obj)
        { 
		   BH.Engine.Reflection.Compute.RecordError("No specific Create method found for {obj.GetType().Name}.");
		   return false;
        }
    }
}
