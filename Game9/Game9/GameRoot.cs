using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game9
{
    public class GameRoot : Game
    {
        public static GameRoot Instance { get; private set; }
        public static GameTime ThisGameTime { get; private set; }
        public static ContentManager ThisGameContent { get; private set; }
        public static Rectangle Screen;
        public static Random Rnd;

        private GraphicsDeviceManager graphics;

        public GameRoot()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            GameRoot.Instance = this;
            GameRoot.ThisGameContent = Content;
            GameRoot.Rnd = new Random();
        }

        protected override void Initialize()
        {
            base.Initialize();
            Screen = this.Window.ClientBounds;
        }

        protected override void LoadContent()
        {
            SceneManager.Instance.Initialize();
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            GameRoot.ThisGameTime = gameTime;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Input.Update();
            PhysicsManager.Instance.Update();
            TestModule.Instance.Update();
            EntityManager.Instance.Update();
            DrawManager.Instance.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            DrawManager.Instance.Draw();
            base.Draw(gameTime);
        }
    }
}
