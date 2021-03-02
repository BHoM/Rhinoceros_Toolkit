
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

using BH.Engine.Adapter;
using BH.oM.Reflection.Attributes;
using Rhino.FileIO;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Adapter.Rhinoceros
{
    public partial class RhinocerosAdapter : BHoMAdapter
    {
        /***************************************************/
        /**** Public constructors                       ****/
        /***************************************************/

        [Description("Specify Rhinoceros file(s) and properties for data transfer.")]
        [Input("fileSettings", "File settings the Rhinoceros Adapter should use.")]
        [Output("adapter", "Adapter to Rhinoceros.")]
        public RhinocerosAdapter(BH.oM.Adapter.FileSettings fileSettings)
        {
            m_Filepath = fileSettings.GetFullFileName();

            if (Path.GetExtension(m_Filepath) != ".3dm")
                BH.Engine.Reflection.Compute.RecordWarning("Rhinoceros Adapter can only operate on files with extension *.3dm.");

            if (File.Exists(m_Filepath))
                m_File3dm = File3dm.Read(m_Filepath);
            else
                m_File3dm = new File3dm();
        }

        /***************************************************/
        /**** Private fields                            ****/
        /***************************************************/
        private File3dm m_File3dm { get; set; }

        private string m_Filepath { get; set; }
    }
}
