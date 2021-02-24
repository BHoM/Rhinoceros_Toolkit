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

namespace BH.Adapter.Rhinoceros
{
    public partial class RhinocerosAdapter : BHoMAdapter
    {
   
        protected override bool ICreate<T>(IEnumerable<T> objects, ActionConfig actionConfig = null)
        {
			bool success = true;

            success = CreateRhinoceros(objects);

            return success;  
        }

        private bool CreateRhinoceros<T>(IEnumerable<T> objects)
        {
            bool success = true;
            File3dm file3Dm = new File3dm();
            foreach (T obj in objects)
            {
                ObjectAttributes attributes = new ObjectAttributes();
                object rhinoGeometry = null;
                if (obj is IGeometry)
                {
                    rhinoGeometry = BH.Engine.Rhinoceros.Convert.ToRhino(obj as dynamic);
                }
                else if(obj is BHoMRhinoObject)
                {
                    BHoMRhinoObject bhomRhino = obj as BHoMRhinoObject;
                    rhinoGeometry = BH.Engine.Rhinoceros.Convert.ToRhino(bhomRhino.Geometry as dynamic);
                }
                else
                {
                    BH.Engine.Reflection.Compute.RecordError("Unable to write objects of type: " + obj.GetType().ToString());
                }
            }
            file3Dm.Polish();
            file3Dm.Write("", 6);

            return success;
        }


        /***************************************************/

        

        // Fallback case. If no specific Create is found, here we should handle what happens then.
        protected bool Create(IBHoMObject obj)
        { 
		   BH.Engine.Reflection.Compute.RecordError("No specific Create method found for {obj.GetType().Name}.");
		   return false;
        }
    }
}
