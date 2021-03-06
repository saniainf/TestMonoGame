﻿using Microsoft.Xna.Framework.Input;
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
            EntityManager.Instance.AddEntity(new Turret());
        }

        public void Update()
        {
            if (Input.WasKeyPressed(Keys.A))
            {
                for (int i = 1; i < 5; i++)
                    EntityManager.Instance.AddEntity(new Turret());
            }
        }
    }
}
