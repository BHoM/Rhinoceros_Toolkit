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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.oM.Base;
using System.ComponentModel;

namespace BH.oM.Rhinoceros.ViewCapture
{
    [Description("Base itnerface for interface controling settings for view capture.")]
    public interface IViewCaptureSettings : IObject
    {
        /***************************************************/
        /**** Properties                                ****/
        /***************************************************/

        [Description("File format to be used.")]
        string FileFormat { get; set; }

        [Description("")]
        bool ScaleScreenItems { get; set; }

        [Description("Controls if the world axes should be captured or not.")]
        bool DrawAxes { get; set; }

        [Description("Controls if the grid should be captured or not.")]
        bool DrawGrid { get; set; }

        [Description("Controls if the grid axes should be captured or not.")]
        bool DrawGridAxes { get; set; }

        [Description("Controls if the background should be transparent.")]
        bool TransparentBackground { get; set; }

        [Description("")]
        bool Preview { get; set; }

        /***************************************************/

    }
}
