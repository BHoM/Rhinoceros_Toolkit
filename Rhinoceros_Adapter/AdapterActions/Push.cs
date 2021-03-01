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
using System.Reflection;
using System.IO;
using Rhino.FileIO;

namespace BH.Adapter.Rhinoceros
{
    public partial class RhinocerosAdapter : BHoMAdapter
    {
        public override List<object> Push(IEnumerable<object> objects, String tag = "", PushType pushType = PushType.AdapterDefault, ActionConfig actionConfig = null)
        {
            //overriding to ensure the creation of a single destination file in a valid location
            if(m_FilePaths.Count == 0)
            {
                BH.Engine.Reflection.Compute.RecordError("No file(s) has been specified.");
                return new List<object>();
            }

            if (File.Exists(m_FilePaths[0]))
            {
                BH.Engine.Reflection.Compute.RecordError("File exists. Appending to a file or overwriting is not yet implemented. Specify a new file name.");
                return new List<object>();
            }

            string dir = Path.GetDirectoryName(m_FilePaths[0]);

            if (dir == "\\" || !Directory.Exists(dir))
            {
                BH.Engine.Reflection.Compute.RecordError("The directory cannot be found, check the path is correct.");
                return new List<object>();
            }

            //create a new file
            m_File3dm = new File3dm();

            return base.Push(objects, tag, pushType, actionConfig);
        }
    }
}

