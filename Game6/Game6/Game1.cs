using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game6
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 
    enum State { newGame, game, miss, hit, blinkHit, blinkMiss };

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D textureSquare;
        Rectangle currentSquare;
        SpriteFont scoreFont;
        int score;
        Color[] squareColors = new Color[3] { Color.Red, Color.Green, Color.Black };
        Color squareColor;
        float iTime = 0.0f;
        int iCount = 0;
        Random rand = new Random();

        float missTime = 1.0f;
        float timeBlinkTick = 0.05f;
        float timeMissBlink = 0.5f;

        State currentState = State.newGame;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            squareColor = squareColors[1];
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            textureSquare = Content.Load<Texture2D>("square");
            scoreFont = Content.Load<SpriteFont>("arialbd");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (currentState)
            {
                case State.newGame:
                    newGameSquare(gameTime);
                    break;
                case State.game:
                    gameSquare(gameTime);
                    break;
                case State.hit:
                    hitSquare(gameTime);
                    break;
                case State.miss:
                    missSquare(gameTime);
                    break;
                case State.blinkHit:
                    blinkHitSquare(gameTime);
                    break;
                case State.blinkMiss:
                    blinkMissSquare(gameTime);
                    break;
            }
            base.Update(gameTime);
        }

        void newGameSquare(GameTime gameTime)
        {
            currentSquare = new Rectangle(
                rand.Next(0, this.Window.ClientBounds.Width - 25),
                rand.Next(0, this.Window.ClientBounds.Height - 25),
                textureSquare.Width,
                textureSquare.Height);

            currentState = State.game;
        }

        void gameSquare(GameTime gameTime)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                if (currentSquare.Contains(Mouse.GetState().Position))
                {
                    currentState = State.hit;
                    iTime = 0.0f;
                }
            iTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (iTime > missTime)
            {
                currentState = State.miss;
                iTime = 0.0f;
            }
        }

        void hitSquare(GameTime gameTime)
        {
            score += 10;
            currentState = State.blinkHit;
        }

        void missSquare(GameTime gameTime)
        {
            score -= 10;
            currentState = State.blinkMiss;
        }

        void blinkHitSquare(GameTime gameTime)
        {
            iTime = MathHelper.Min(timeBlinkTick, iTime + (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (iTime == timeBlinkTick)
            {
                if (squareColor == squareColors[1])
                    squareColor = squareColors[2];
                else
                {
                    iCount++;
                    squareColor = squareColors[1];
                }
                iTime = 0.0f;
            }
            if (iCount >= 3)
            {
                currentState = State.newGame;
                iCount = 0;
                iTime = 0.0f;
            }
        }

        void blinkMissSquare(GameTime gameTime)
        {
            squareColor = squareColors[0];
            iTime = MathHelper.Min(timeMissBlink, iTime + (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (iTime == timeMissBlink)
            {
                currentState = State.newGame;
                iTime = 0.0f;
                squareColor = squareColors[1];
            }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(textureSquare, currentSquare, squareColor);
            spriteBatch.DrawString(scoreFont, "Score: " + score.ToString(), new Vector2(20,20), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
