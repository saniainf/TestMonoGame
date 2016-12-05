using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class EntityManager
    {
        private static EntityManager instance;
        public static EntityManager Instance
        {
            get { return instance ?? (instance = new EntityManager()); }
        }

        public void Update()
        {

        }
    }
}
