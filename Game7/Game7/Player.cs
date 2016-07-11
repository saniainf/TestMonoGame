using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game7
{
    class Player
    {
        private Flags playerFlag;

        public Player()
        {
            playerFlag = new Flags();
        }

        public void Update(GameTime gameTime)
        {
            // установка флагов
            playerFlag.Push(FlagTypes.Move, true);

            // действия
            bool r = playerFlag.Pop(FlagTypes.Move);

            // очистка флагов
            playerFlag.Clean();
        }
    }
}
