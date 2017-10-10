using BH.Adapter.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Adapter.Rhinoceros
{
    public partial class RhinocerosAdapter
    {
        public override int UpdateProperty(FilterQuery filter, string property, object newValue, Dictionary<string, string> config = null)
        {
            throw new NotImplementedException();
        }
    }
}
