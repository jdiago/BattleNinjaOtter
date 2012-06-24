using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BattleNinjaOtter
{
    class BadGuy
    {
        public Texture2D Texture;
        public Vector2 Position;
        public bool Active;
        public float moveSpeed;

        public int Width
        {
            get { return Texture.Width; }
        }

        public int Height
        {
            get { return Texture.Height; }
        }

        public void Initialize(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
            Active = true;
            moveSpeed = 6f;
        }

        public void Update(GameTime gameTime)
        {
            Position.X -= moveSpeed;

            if (Position.X < -Width)
            {
                Active = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
