using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BattleNinjaOtter
{
    class HeroBlock
    {
        public Texture2D Texture;
        public Vector2 Position;
        public bool Active;

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
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
