using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class TestModule
    {
        public static TestModule Instance
        {
            get { return instance ?? (instance = new TestModule()); }
        }

        private static TestModule instance;

        public TestModule()
        {
            EntityManager.Instance.AddEntity(new Ball());
        }

        public void Update()
        {
            if (Input.WasKeyPressed(Keys.A))
            {
                for (int i = 1; i < 50; i++)
                    EntityManager.Instance.AddEntity(new Ball());
            }

            if (Input.WasKeyPressed(Keys.X))
            {
                for (int j = 1; j < 10; j++)
                {
                    int i = GameRoot.Rnd.Next(EntityManager.Instance.Entities.Count);
                    if (EntityManager.Instance.Entities.Count > 0)
                        EntityManager.Instance.Entities[i].IsRemove = true;
                }
            }
        }
    }
}
