/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2025, the respective contributors. All rights reserved.
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
using System.ComponentModel;

namespace BH.oM.Adapters.Rhinoceros.ViewCapture
{
    [Description("View capture settings class with control over the final size of the image in pixels.")]
    public class DimensionViewCaptureSettings : IViewCaptureSettings
    {
        /***************************************************/
        /**** Properties                                ****/
        /***************************************************/

        [Description("Width (in pixels) of the final image.")]
        public virtual int Width { get; set; }

        [Description("Height (in pixels) of the final image.")]
        public virtual int Height { get; set; }

        [Description("File format to be used. Defaults to png.")]
        public virtual string FileFormat { get; set; } = "png";

        [Description("")]
        public virtual bool ScaleScreenItems { get; set; }

        [Description("Controls if the world axes should be captured or not.")]
        public virtual bool DrawAxes { get; set; }

        [Description("Controls if the grid should be captured or not.")]
        public virtual bool DrawGrid { get; set; }

        [Description("Controls if the grid axes should be captured or not.")]
        public virtual bool DrawGridAxes { get; set; }

        [Description("Controls if the background should be transparent.")]
        public virtual bool TransparentBackground { get; set; }

        [Description("")]
        public virtual bool Preview { get; set; }

        /***************************************************/
    }
}


