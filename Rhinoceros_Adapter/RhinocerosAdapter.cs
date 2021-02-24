
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
        [Description("Specify Rhinoceros file and properties for data transfer.")]
        [Input("fileSettings", "Input the file settings to get the file name and directory the Rhinoceros Adapter should use.")]
        [Output("adapter", "Adapter to Rhinoceros.")]
        public RhinocerosAdapter(BH.oM.Adapter.FileSettings fileSettings = null)
        {
            m_FilePaths = new List<string>();
            if (fileSettings == null)
            {
                BH.Engine.Reflection.Compute.RecordError("Please set the File Settings correctly to enable the Rhinoceros Adapter to work correctly.");
                return;
            }

            if (fileSettings.FileName == "" && fileSettings.Directory == "")
            {
                BH.Engine.Reflection.Compute.RecordError("Either provide a file name and directory to pull a single .3dm file, or a directory to pull multiple .3dm files.");
                return;
            }

            //no directory but full path provided in filename
            if(fileSettings.FileName != "" && fileSettings.Directory == "")
            {
                if (!Path.HasExtension(fileSettings.FileName) || Path.GetExtension(fileSettings.FileName) != ".3dm")
                {
                    BH.Engine.Reflection.Compute.RecordError("File name must contain a file .3dm extension.");
                    return;
                }
                else if(!File.Exists(fileSettings.FileName))
                {
                    BH.Engine.Reflection.Compute.RecordError("File does not exist.");
                    return;
                }
                else
                {
                    m_FilePaths.Add(fileSettings.GetFullFileName());
                }
                    
            }
            //check the directory
            else if (!Directory.Exists(fileSettings.Directory))
            {
                BH.Engine.Reflection.Compute.RecordError("Directory provided does not exist.");
                return;
            }
            else
            {
                string[] files = Directory.GetFiles(fileSettings.Directory, "*.3dm");
                if(files.Length == 0)
                {
                    BH.Engine.Reflection.Compute.RecordError("No .3dm files found in the directory.");
                    return;
                }
                else
                {
                    if (fileSettings.FileName != "" && !files.Any(f => f.Contains(Path.GetFileName(fileSettings.FileName))))
                    {
                        BH.Engine.Reflection.Compute.RecordError("File specified was not found in the directory.");
                        return;
                    }

                    else if (fileSettings.FileName != "" && files.Any(f => f.Contains(Path.GetFileName(fileSettings.FileName))))
                        m_FilePaths.Add(fileSettings.GetFullFileName());

                    else
                        m_FilePaths = files.ToList();
                }
            }

            _fileSettings = fileSettings;

        }

        private static BH.oM.Adapter.FileSettings _fileSettings { get; set; } = null;
        private static List<string> m_FilePaths { get; set; } = new List<string>();
    }
}
