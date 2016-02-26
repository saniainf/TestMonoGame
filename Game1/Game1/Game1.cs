using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        Rectangle window;
        SpriteFont arial;
        SpriteBatch spriteBatch;
        Texture2D brick;
        Vector2 location;
        Vector2 position;
        Vector2 direction;
        float speed, maxSpeed;
        float acc;
        float frc, airFrc;
        float airSpeed, maxAirSpeed, maxFallSpeed;
        float airAcc;
        float grav;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            window = this.Window.ClientBounds;
            arial = Content.Load<SpriteFont>("arial");
            brick = Content.Load<Texture2D>("brick");
            location = new Vector2(window.Width / 2 - brick.Width / 2, window.Height - brick.Height);
            //location = Vector2.Zero;
            direction = Vector2.Zero;
            speed = 0;
            maxSpeed = 600;
            acc = 1000;
            frc = 2000;
            airFrc = 500;
            airSpeed = 0;
            maxAirSpeed = 500;
            maxFallSpeed = 500;
            airAcc = 600;
            grav = 500;
        }


        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Left)) // жмем влево
            {
                if (direction.X == 0) // если направление не заданно то можно изменить
                    direction.X = -1;
                if (direction.X < 0) // если движемся влево 
                    speed = Math.Min(speed + acc * time, maxSpeed); // увеличиваем скорость
                else // если движемся вправо
                    if (onGround())
                        speed = Math.Max(speed - frc * time - acc * time, 0); // тормозим с ускорением на земле 
                    else
                        speed = Math.Max(speed - airFrc * time - acc * time, 0); //тормозим с ускорением в воздухе
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (direction.X == 0)
                    direction.X = 1;
                if (direction.X > 0)
                    speed = Math.Min(speed + acc * time, maxSpeed);
                else
                    if (onGround())
                        speed = Math.Max(speed - frc * time - acc * time, 0);
                    else
                        speed = Math.Max(speed - airFrc * time - acc * time, 0);
            }
            else // ни влево ни вправо, движемся по инерции
                if (onGround())
                    speed = Math.Max(speed - frc * time, 0); // тормозим на земле 
                else
                    speed = Math.Max(speed - airFrc * time, 0); // тормозим в воздухе

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                if (onGround())
                    speed = Math.Max(speed - frc * time - acc * time, 0); // тормозим с ускорением на земле
                else
                    speed = Math.Max(speed - airFrc * time - acc * time, 0); //тормозим с ускорением в воздухе

            if (Keyboard.GetState().IsKeyDown(Keys.Up)) // жмем вверх
            {
                if (direction.Y == 0) // если направление не заданно то можно изменить
                    direction.Y = -1;
                if (direction.Y < 0)
                    airSpeed = Math.Min(airSpeed + airAcc * time, maxAirSpeed); // если летим то ускоряемся
                else
                    airSpeed = Math.Max(airSpeed - airAcc * time, 0); // если падаем то тормозим падение
            }
            else if (!onGround()) // не жмем вверх
            {
                if (direction.Y < 0)
                    airSpeed = Math.Max(airSpeed - grav * time, 0); // если летим вверх то тормозим
                if (direction.Y == 0) // если скорость равна 0 то можно падать
                    direction.Y = 1;
                if (direction.Y > 0)
                    airSpeed = Math.Min(airSpeed + grav * time, maxFallSpeed); // если падаем, то увеличиваем скорость падения
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down) && !onGround())
            {
                if (direction.Y < 0)
                    airSpeed = Math.Max(airSpeed - grav * time - airAcc * time, 0); // если летим вверх то тормозим с ускорением
                if (direction.Y == 0) // если направление не заданно то можно изменить
                    direction.Y = 1;
                if (direction.Y > 0)
                    airSpeed = Math.Min(airSpeed + grav * time + airAcc * time, maxFallSpeed); // если падаем, то увеличиваем скорость падения с ускорением
            }

            if (speed == 0)
                direction.X = 0;
            if (airSpeed == 0)
                direction.Y = 0;

            location.X += direction.X * speed * time;
            location.Y += direction.Y * airSpeed * time;
            handleCollision();

            position.X = (float)Math.Round(location.X, 0);
            position.Y = (float)Math.Round(location.Y, 0);

            base.Update(gameTime);
        }

        private bool onGround()
        {
            if (location.Y >= window.Height - brick.Height)
                return true;
            else
                return false;
        }

        void handleCollision()
        {
            if (location.X < 0)
            {
                location.X = 0;
                speed = 0;
            }
            if (location.X + brick.Width > window.Width)
            {
                location.X = window.Width - brick.Width;
                speed = 0;
            }
            if (location.Y < 0)
            {
                location.Y = 0;
                airSpeed = 0;
            }
            if (location.Y + brick.Height > window.Height)
            {
                location.Y = window.Height - brick.Height;
                airSpeed = 0;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(brick, position, Color.White);
            spriteBatch.DrawString(arial, onGround().ToString(), new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(arial, "X: " + direction.X + ", Y: " + direction.Y, new Vector2(10, 30), Color.White);
            spriteBatch.DrawString(arial, speed.ToString(), new Vector2(10, 50), Color.White);
            spriteBatch.DrawString(arial, airSpeed.ToString(), new Vector2(10, 70), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
