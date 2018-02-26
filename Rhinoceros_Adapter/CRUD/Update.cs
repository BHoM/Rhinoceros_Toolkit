using BH.oM.DataManipulation.Queries;
using System;
using System.Collections.Generic;

namespace BH.Adapter.Rhinoceros
{
    public partial class RhinocerosAdapter
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public override int UpdateProperty(FilterQuery filter, string property, object newValue, Dictionary<string, object> config = null)
        {
            throw new NotImplementedException();
        }


        /***************************************************/
    }
}
