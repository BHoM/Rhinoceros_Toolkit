using System;
using System.Collections.Generic;

namespace BH.Adapter.Rhinoceros
{
    public partial class RhinocerosAdapter
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public override bool Execute(string command, Dictionary<string, object> parameters = null, Dictionary<string, object> config = null)
        {
            throw new NotImplementedException(command + " is not a recognised command for the Rhino adapter.");
        }


        /***************************************************/
    }
}
