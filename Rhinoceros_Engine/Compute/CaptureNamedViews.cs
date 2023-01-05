/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2023, the respective contributors. All rights reserved.
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

using BH.oM.Base;
using BH.oM.Base.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
using Rhino;
using Rhino.Display;
using System.Drawing;
using BH.oM.Rhinoceros.ViewCapture;
using System.Drawing.Imaging;

namespace BH.Engine.Rhinoceros
{
    public static partial class Compute
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        [Description("Captures all named views to files.")]
        [Input("active", "Toggle to activate. Toggle to true to capture the view port.")]
        [Input("folderPath", "Folder path to store the image in. To folder that the currently open rhino model is stored in will be used if nothing is provided.")]
        [Input("imageName", "Name of the image, without file ending. THe name of the image will be this name + _viewName. To update the file ending, please see the viewcapture settings. If nothing is provided, the named view name will be the full filename.")]
        [Input("namedViewFilter", "Optional filter of which named views that should be captured to file. All named views are captured if nothing is provided.")]
        [Input("settings", "Settings to control the view capture.")]
        [Output("success", "Returns true if the view capture was successful.")]
        public static bool CaptureNamedViews(bool active = false, string folderPath = "", string imageName = "", List<string> namedViewFilter = null, IViewCaptureSettings settings = null)
        {
            RhinoDoc doc = RhinoDoc.ActiveDoc;

            if (doc == null)
                return false;

            folderPath = ValidateFolderPath(folderPath, doc);
            if (folderPath == null)
                return false;

            if (!active)
                return false;

            bool success = true;

            settings = settings ?? new ScaleViewCaptureSettings();  //Default view capture settings

            for (int i = 0; i < doc.NamedViews.Count; i++)
            {
                string namedView = doc.NamedViews[i].Name;

                if (namedViewFilter != null && namedViewFilter.Count != 0)  //If named view filter provided
                    if (!namedViewFilter.Contains(namedView))   //Filter out items in the list. If no filter provided, assume all to be captured
                        continue;

                doc.NamedViews.Restore(i, doc.Views.ActiveView.ActiveViewport);
                string name;
                if (string.IsNullOrEmpty(imageName))
                    name = namedView;
                else
                    name = imageName + "_" + namedView;
                success &= CaptureActiveView(doc, settings, folderPath, name);

            }

            return success;
        }

        /***************************************************/
    }
}
