using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Boids
{
    public class GameObject
    {
        public Vector2 position;
        public Vector2 velocity;
        public float radius;

        public GameObject()
        {
            position = Vector2.Zero;
            velocity = Vector2.Zero;
            radius = 10f;
        }

        public GameObject(Vector2 p_position)
        {
            position = p_position;
            radius = 10f;
        }

        protected virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public virtual void Unload()
        {

        }
    }
}
