/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2022, the respective contributors. All rights reserved.
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

using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.DocObjects;
using Rhino.Collections;
using System.ComponentModel;
using BH.oM.Base.Attributes;

namespace BH.Engine.Rhinoceros
{
    public static partial class Create
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        [Description("Capture a Rhino Viewport as a .jpg image and save to a specific filepath")]
        [Input("filepath", "Location for the image to be saved")]
        [Input("name", "Name for the image to be saved in the specified location")]
        [Input("active", "Activate to save image using the provided settings")]
        public static void ViewCapture(string filepath, string name, bool active)
        {       
            string file =
              '"' + filepath + System.IO.Path.DirectorySeparatorChar + name + ".jpg" + '"';
            string command = "-_ViewCaptureToFile " + file + " Scale=2 _Enter";

            if (active)
                BH.Engine.Base.Compute.RecordNote($"{filepath}" + $"{name}" + ".jpg");
                Rhino.RhinoApp.RunScript(command, false);
        }

        /***************************************************/
    }
}