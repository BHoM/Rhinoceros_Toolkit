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

        [Description("Captures the currently active view to file.")]
        [Input("active", "Toggle to activate. Toggle to true to capture the view port.")]
        [Input("folderPath", "Folder path to store the image in. To folder that the currently open rhino model is stored in will be used if nothing is provided.")]
        [Input("imageName", "Name of the image, without file ending. To update the file ending, please see the viewcapture settings. The viewport name will be used if nothing is provided.")]
        [Input("settings", "Settings to control the view capture.")]
        [Output("success", "Returns true if the view capture was successful.")]
        public static bool CaptureView(bool active = false, string folderPath = "", string imageName = "", IViewCaptureSettings settings = null)
        {
            RhinoDoc doc;
            try
            {
                doc = RhinoDoc.ActiveDoc;
            }
            catch (Exception e)
            {
                string msg = "Failed to get the active rhino document. Exception thrown: " + e.Message;
                Base.Compute.RecordError(msg);
                return false;
            }

            if (doc == null)
                return false;

            folderPath = ValidateFolderPath(folderPath, doc);
            if (folderPath == null)
                return false;

            if (!active)
                return false;

            settings = settings ?? new ScaleViewCaptureSettings();  //Default view capture settings

            return CaptureActiveView(doc, settings, folderPath, imageName);
        }


        /***************************************************/
        /**** Private Methods                           ****/
        /***************************************************/

        [Description("")]
        [Input("", "")]
        [Output("", "")]
        private static bool CaptureActiveView(RhinoDoc doc, IViewCaptureSettings settings, string folderName, string imageName)
        {
            var view = doc.Views.ActiveView;

            if (string.IsNullOrWhiteSpace(imageName))
            {
                Engine.Base.Compute.RecordNote("No image name provided. Name of active viewport will be used.");
                imageName = view.ActiveViewport.Name;
            }

            ViewCapture viewCapture = settings.IViewCapture(view.ActiveViewport);

            if (viewCapture == null)
                return false;

            var bitmap = viewCapture.CaptureToBitmap(view);

            if (null != bitmap)
            {
                string fileEnding;
                ImageFormat imageFormat = settings.GetImageFormat(out fileEnding);
                if (imageFormat == null)
                    return false;

                var filename = Path.Combine(folderName, imageName + fileEnding);
                bitmap.Save(filename, imageFormat);
                return true;
            }

            return false;
        }

        /***************************************************/

        private static string ValidateFolderPath(string folderPath, RhinoDoc doc)
        {
            if (string.IsNullOrEmpty(folderPath))
            {
                folderPath = Path.GetDirectoryName(doc.Path);
                Engine.Base.Compute.RecordNote($"No path provided. Images will be saved in the same folder as the Open rhino model.");
            }

            if (!Directory.Exists(folderPath))
            {
                Engine.Base.Compute.RecordError($"Directory {folderPath} does not exist.");
                return null;
            }
            return folderPath;
        }

        /***************************************************/

        private static ImageFormat GetImageFormat(this IViewCaptureSettings settings, out string fileEnding)
        {
            string imageFormat = settings.FileFormat.ToUpper();

            switch (imageFormat)
            {
                case "BMP":
                    fileEnding = ".bmp";
                    return ImageFormat.Bmp;
                case "EMF":
                    fileEnding = ".emf";
                    return ImageFormat.Emf;
                case "WMF":
                    fileEnding = ".wmf";
                    return ImageFormat.Wmf;
                case "GIF":
                    fileEnding = ".gif";
                    return ImageFormat.Gif;
                case "JPG":
                case "JPEG":
                    fileEnding = ".jpg";
                    return ImageFormat.Jpeg;
                case "PNG":
                    fileEnding = ".png";
                    return ImageFormat.Png;
                case "TIFF":
                    fileEnding = ".tiff";
                    return ImageFormat.Tiff;
                case "EXIF":
                    fileEnding = ".exif";
                    return ImageFormat.Exif;
                case "ICON":
                    fileEnding = ".icon";
                    return ImageFormat.Icon;
                default:
                    Engine.Base.Compute.RecordError("Unknown image format.");
                    fileEnding = "";
                    return null;
            }
        }

        /***************************************************/

        private static ViewCapture IViewCapture(this IViewCaptureSettings settings, RhinoViewport viewport)
        {
            ViewCapture viewCapture = ViewCapture(settings as dynamic, viewport);
            viewCapture.ScaleScreenItems = settings.ScaleScreenItems;
            viewCapture.DrawAxes = settings.DrawAxes;
            viewCapture.DrawGrid = settings.DrawGrid;
            viewCapture.DrawGridAxes = settings.DrawGridAxes;
            viewCapture.TransparentBackground = settings.TransparentBackground;
            viewCapture.Preview = settings.Preview;
            return viewCapture;
        }

        /***************************************************/

        private static ViewCapture ViewCapture(this ScaleViewCaptureSettings settings, RhinoViewport viewport)
        {
            return new ViewCapture
            {
                Height = (int)Math.Round(viewport.Size.Height * settings.Scale),
                Width = (int)Math.Round(viewport.Size.Width * settings.Scale)
            };
        }

        /***************************************************/

        private static ViewCapture ViewCapture(this DimensionViewCaptureSettings settings, RhinoViewport viewport)
        {
            return new ViewCapture
            {
                Height = settings.Height,
                Width = settings.Width
            };
        }

        /***************************************************/
    }
}
