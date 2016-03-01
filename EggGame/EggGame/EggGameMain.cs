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
        static public Rectangle ScreenRectangle { get; set; }

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Rectangle screenRectangle;
        private GOManager goManager;
        private bool keyPress;
        public EggGameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            
            screenRectangle = this.Window.ClientBounds;
            ScreenRectangle = screenRectangle;
            goManager = new GOManager(Content);
            keyPress = false;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            goManager.LoadContent();
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !keyPress)
            {
                //goManager.AddBall();
                goManager.ballTest();
                keyPress = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Space) && keyPress)
                keyPress = false;
            goManager.Update(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            goManager.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }
    }
}
