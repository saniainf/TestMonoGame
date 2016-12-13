using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class PhysicsModule
    {
        public static PhysicsModule Instance
        {
            get { return instance ?? (instance = new PhysicsModule()); }
        }

        private static PhysicsModule instance;

        public void Update()
        {

        }
    }
}
