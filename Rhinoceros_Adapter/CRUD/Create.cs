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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.oM.Base;
using BH.oM.Adapter;
using BH.oM.Geometry;
using BHR = BH.oM.Adapters.Rhinoceros;
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

            BHR.RhinocerosConfig config = actionConfig as BHR.RhinocerosConfig;
            if (config == null)
            {
                BH.Engine.Reflection.Compute.RecordError("Please provide a valid RhinocerosConfig object for pushing to a Rhinoceros file.");
                return false;
            }

            success = Create(objects as dynamic, config);

            return success;  
        }

        /***************************************************/

        public bool Create(List<BHR.RhinoObject> rhinoObjects, BHR.RhinocerosConfig config)
        {
            bool success = true;
            
            List<Layer> layers = new List<Layer>();
            Dictionary<object, ObjectAttributes> objectDict = new Dictionary<object, ObjectAttributes>();
            ObjectAttributes attributes = new ObjectAttributes();

            foreach (BHR.RhinoObject bhomRhino in rhinoObjects)
            {
                attributes = new ObjectAttributes();
                object rhinoGeometry = null;

                int layerIndex = layers.FindIndex(l => l.Name == bhomRhino.Layer.Name);

                if (layerIndex == -1)
                {
                    layers.Add(bhomRhino.Layer.ToRhino());
                    layerIndex = layers.Count - 1;
                }

                attributes.LayerIndex = layerIndex;
                attributes.ObjectColor = bhomRhino.ObjectColour;
                attributes.ColorSource = bhomRhino.ColourSource.ToRhino();

                rhinoGeometry = BH.Engine.Rhinoceros.Convert.ToRhino(bhomRhino.Geometry as dynamic);

                objectDict.Add(rhinoGeometry, attributes);

            }

            AddLayers(layers);
            AddObjects(objectDict);

            m_File3dm.Polish();

            return m_File3dm.Write(m_Filepath, config.Version);
        }
        /***************************************************/

        private bool Create(IEnumerable<oM.Adapter.ObjectWrapper> objects, BHR.RhinocerosConfig config)
        {
            ObjectAttributes attributes = new ObjectAttributes();
            attributes.LayerIndex = 0;

            foreach (ObjectWrapper wrapper in objects)
            {
                if(wrapper.WrappedObject is IGeometry)
                    IAddObjectToFile(BH.Engine.Rhinoceros.Convert.ToRhino(wrapper.WrappedObject as dynamic), attributes);
            }

            m_File3dm.Polish();
            return m_File3dm.Write(m_Filepath, config.Version);
        }


        /***************************************************/

        private void AddLayers(List<Layer> layers)
        {
            foreach(Layer layer in layers)
            {
                m_File3dm.Layers.Add(layer);
            }
        }

        /***************************************************/

        private void AddObjects(Dictionary<object, ObjectAttributes> objectDict)
        {
            foreach(var objAtt in objectDict)
            {
                IAddObjectToFile(objAtt.Key, objAtt.Value);
            }
        }

        /***************************************************/
        protected bool Create(IBHoMObject obj)
        { 
		   BH.Engine.Reflection.Compute.RecordError($"No specific Create method found for {obj.GetType().Name}.");
		   return false;
        }
    }
}
