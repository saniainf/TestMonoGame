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

        private List<SimpleEntity> tmpEntitys;

        public EntityManager()
        {
            tmpEntitys = new List<SimpleEntity>();
            Ball b = new Ball();
            tmpEntitys.Add(b);
        }

        public void Update()
        {

        }
    }
}
