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
        static public int WindowWidth { get; set; }
        static public int WindowHeight { get; set; }

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Rectangle screenRectangle;
        private List<Egg> eggs = new List<Egg>();
        private Paddle paddle;

        public EggGameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            screenRectangle = this.Window.ClientBounds;
            WindowWidth = screenRectangle.Width;
            WindowHeight = screenRectangle.Height;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            eggs.Add(new Egg(Content, 50f));
            paddle = new Paddle(Content);
        }
        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                eggs.Add(new Egg(Content, 50f));
            paddle.Update(gameTime);
            foreach (Egg egg in eggs)
                egg.Update(gameTime, paddle);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            paddle.Draw(gameTime, spriteBatch);
            foreach (Egg egg in eggs)
                egg.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }
    }
}
