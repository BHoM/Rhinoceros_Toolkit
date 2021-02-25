using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BH.oM.Adapter;
using BH.oM.Base;

namespace BH.oM.Adapters.Rhinoceros
{
    [Description("Define configuration settings for pushing and pulling Rhinoceros files using the Rhinoceros Adapter.")]
    public class RhinocerosConfig : ActionConfig
    {
        [Description("Define the version of Rhinoceros the Adapter should be operating with.")]
        public virtual int Version { get; set; } = 6;
    }
}
