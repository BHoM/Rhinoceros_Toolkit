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

using BH.oM.Data.Requests;
using BH.oM.Adapter;
using BH.oM.Base;
using BH.Engine.Reflection;
using System.IO;

namespace BH.Adapter.Rhinoceros
{
    public partial class RhinocerosAdapter : BHoMAdapter
    {
        public override List<object> Push(IEnumerable<object> objects, String tag = "", PushType pushType = PushType.AdapterDefault, ActionConfig actionConfig = null)
        {
            //overriding Push to force PushType.CreateOnly and creation of new single Rhino.FileIO.File3dm where all objects will be written
            if (pushType!= PushType.CreateOnly)
                BH.Engine.Reflection.Compute.RecordWarning("RhinocerosAdapter is configured to as a PushType.CreateOnly adapter. All objects are pushed to a new file.");
            
            pushType = PushType.CreateOnly;

            if (Path.GetExtension(m_Filepath) != ".3dm")
                return new List<object>();
             
            m_File3dm = new Rhino.FileIO.File3dm();
            return base.Push(objects, tag, pushType, actionConfig);
        }
    }
}

