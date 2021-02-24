using BH.oM.Base;
using BH.oM.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.oM.Adapters.Rhinoceros
{
    public class BHoMRhinoObject : BHoMObject
    {
        /*******************************************/
        /**** Properties                        ****/
        /*******************************************/
        public virtual string Layer { get; set; } = "";

        public virtual IGeometry Geometry { get; set; } = null;

        public virtual Color LayerColour { get; set; } = new Color();

        public virtual Color ObjectColour { get; set; } = new Color();

        public virtual ColourSource ColourSource { get; set; } = ColourSource.ByLayer;
            
    }
}
