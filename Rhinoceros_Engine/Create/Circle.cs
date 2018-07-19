using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Engine.Rhinoceros
{
    public static partial class Create
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public static Circle RandomCircle(Random random)
        {
            return new Circle(RandomPoint3d(random), RandomPoint3d(random), RandomPoint3d(random));
        }

        /***************************************************/

        public static Circle RandomCircle(int seed = 0)
        {
            return RandomCircle(new Random(seed));
        }

        /***************************************************/
    }
}
