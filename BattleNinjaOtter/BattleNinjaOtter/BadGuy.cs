using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;

namespace BattleNinjaOtter
{
    class BadGuy
    {
        public Texture2D Texture;
        public Vector2 Position;
        public bool Active;
        public float moveSpeed;
        public int moveDirection;

        public int Width
        {
            get { return Texture.Width; }
        }

        public int Height
        {
            get { return Texture.Height; }
        }

        public BadGuy(int moveDirection)
        {
            this.moveDirection = moveDirection;
        }

        public void Initialize(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
            Active = true;
            moveSpeed = 8.0f;
        }

        public void Update(GameTime gameTime)
        {
            if (moveDirection == 0)
            {
                Position.X -= moveSpeed;

                if (Position.X < -Width)
                {
                    Active = false;
                }
            }
            else
            {
                Position.Y += moveSpeed;

                if (Position.Y > 500)
                {
                    Active = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
