using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boids
{
    public class Wall : GameObject
    {
        public float Height;
        public float Width;

        public Texture2D Texture;
        public Rectangle rect;

        public Wall(Vector2 position, float width, float height) : base(position)
        {
            Width = width;
            Height = height;

            Texture = Game1.TextureDictionary["whiteRectangle"];

            rect = new Rectangle((int)position.X, (int)position.Y, (int)width, (int)height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, rect, Color.Black);
        }
    }
}
