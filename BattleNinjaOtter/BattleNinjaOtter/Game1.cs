using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BattleNinjaOtter
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        HeroBlock hero;
        float heroMoveSpeed;
        
        List<BadGuy> badGuys;
        Texture2D badGuyTexture;
        TimeSpan badGuysSpawnTime;
        TimeSpan previousSpawnTime;

        Random rand;

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 640;

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
            hero = new HeroBlock();
            heroMoveSpeed = 8.0f;

            badGuys = new List<BadGuy>();
            previousSpawnTime = TimeSpan.Zero;
            badGuysSpawnTime = TimeSpan.FromSeconds(1.0f);

            rand = new Random();

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

            var playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);

            hero.Initialize(Content.Load<Texture2D>("block"), playerPosition);

            badGuyTexture = Content.Load<Texture2D>("badBlock");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void UpdatePlayer(GameTime gameTime)
        {
            if(currentKeyboardState.IsKeyDown(Keys.Left))
            {
                hero.Position.X -= heroMoveSpeed;
            }

            if(currentKeyboardState.IsKeyDown(Keys.Right))
            {
                hero.Position.X += heroMoveSpeed;
            }

            if(currentKeyboardState.IsKeyDown(Keys.Up))
            {
                hero.Position.Y -= heroMoveSpeed;
            }

            if(currentKeyboardState.IsKeyDown(Keys.Down))
            {
                hero.Position.Y += heroMoveSpeed;
            }

            // Make sure that the player does not go out of bounds
            hero.Position.X = MathHelper.Clamp(hero.Position.X, 0, GraphicsDevice.Viewport.Width - hero.Width);
            hero.Position.Y = MathHelper.Clamp(hero.Position.Y, 0, GraphicsDevice.Viewport.Height - hero.Height);
        }

        private void UpdateBadGuys(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - previousSpawnTime > badGuysSpawnTime)
            {
                previousSpawnTime = gameTime.TotalGameTime;

                AddBadGuy();
            }

            for (int i = badGuys.Count - 1; i >= 0; i--)
            {
                badGuys[i].Update(gameTime);
                if (badGuys[i].Active == false)
                {
                    badGuys.RemoveAt(i);
                }
            }
        }

        private void AddBadGuy()
        {
            var randPosition = new Vector2(GraphicsDevice.Viewport.Width + badGuyTexture.Width / 2, rand.Next(0, GraphicsDevice.Viewport.Height - badGuyTexture.Height));

            var newBadGuy = new BadGuy();

            newBadGuy.Initialize(badGuyTexture, randPosition);

            badGuys.Add(newBadGuy);
        }

        private void UpdateCollision()
        {
            Rectangle rectangle1;
            Rectangle rectangle2;

            rectangle1 = new Rectangle((int)hero.Position.X, 
                (int)hero.Position.Y, 
                hero.Width, 
                hero.Height);

            for (int i = 0; i < badGuys.Count; i++)
            {
                rectangle2 = new Rectangle((int)badGuys[i].Position.X,
                    (int)badGuys[i].Position.Y,
                    badGuys[i].Width,
                    badGuys[i].Height);

                if (rectangle1.Intersects(rectangle2))
                {
                    hero.Active = false;
                }
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            
            UpdatePlayer(gameTime);
            UpdateBadGuys(gameTime);
            UpdateCollision();


        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            hero.Draw(spriteBatch);

            for (int i = 0; i < badGuys.Count; i++)
            {
                badGuys[i].Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
