using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace EggGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class EggGameMain : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle screenRectangle;
        List<Egg> eggs = new List<Egg>();

        public EggGameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            screenRectangle = this.Window.ClientBounds;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            eggs.Add(new Egg(Content, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height));
        }
        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                eggs.Add(new Egg(Content, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height));
            foreach (Egg egg in eggs)
                egg.Update(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            foreach (Egg egg in eggs)
                egg.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }
    }
}
