using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Adapter.Rhinoceros
{
    public partial class RhinocerosAdapter
    {
        public override bool Execute(string command, Dictionary<string, object> parameters = null, Dictionary<string, string> config = null)
        {
            throw new NotImplementedException(command + " is not a recognised command for the Rhino adapter.");
        }
    }
}
