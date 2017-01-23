using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class BallInput : IBehavior
    {
        public bool IsRemove
        {
            get
            {
                return false;
            }
            set
            {

            }
        }

        Entity rootEntity;

        public BallInput(Entity rootEntity)
        {
            this.rootEntity = rootEntity;
        }

        public void Update()
        {
            if (Input.WasKeyPressed(Keys.X))
            {
                DrawComponent dc = rootEntity.GetComponent<DrawComponent>() as DrawComponent;
                dc.PauseAnimation("turretRotation");
            }

            if (Input.WasKeyPressed(Keys.Z))
            {
                DrawComponent dc = rootEntity.GetComponent<DrawComponent>() as DrawComponent;
                dc.PlayAnimation("turretRotation");
            }
        }
    }
}
