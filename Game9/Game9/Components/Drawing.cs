﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game9
{
    class Drawing : IComponent
    {
        public Texture2D Sprite { get; set; }

        public Drawing()
        {
            //Sprite = new Texture2D(GameRoot.ThisGameGraphicsDevice, 0, 0);
        }

        public void Initialize()
        {

        }

        public void Update()
        {

        }

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
    }
}
