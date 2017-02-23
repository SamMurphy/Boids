using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boids
{
    public class Boid : GameObject
    {
        public float Radius;
        Texture2D texture;

        public Boid(Vector2 p_position) : base(p_position)
        {
            Radius = 5f;
            texture = Game1.TextureDictionary["whiteRectangle"];
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.Chocolate, 0f, Vector2.Zero, new Vector2(Radius, Radius), SpriteEffects.None, 0f);
        }

        public void UpdateBoidMovement(GameTime gameTime, Vector2 target, float cohesion = 1, float avoidance = 1, float alignment = 1, float goal = 1)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 v1, v2, v3, v4;

            v1 = cohesion * applyCohesion(); // cohesion
            v2 = avoidance * applyAvoidance(); // avoidance
            v3 = alignment * applyAlignment(); // alignment
            v4 = goal * moveTowards(target); // target

            velocity = velocity + v1 + v2 + v3 + v4 + (10 * avoidWalls());
            velocity = Vector2.Clamp(velocity, new Vector2(-30, -30), new Vector2(30, 30));

            position += velocity * dt;
        }

        Vector2 applyCohesion()
        {
            Vector2 v = Vector2.Zero;

            foreach (Boid boid in Game1.Boids)
            {
                if (boid != this)
                {
                    v += boid.position;
                }
            }

            v = v / (float)(Game1.Boids.Count - 1);

            return (v - position) / 100f;
        }

        Vector2 applyAvoidance()
        {
            Vector2 v = Vector2.Zero;

            foreach (Boid boid in Game1.Boids)
            {
                if (boid != this)
                {
                    if (Vector2.Distance(boid.position, position) < boid.Radius * 2f)
                    {
                        v = v - (boid.position - position);
                    }
                }
            }

            return v;
        }

        Vector2 applyAlignment()
        {
            Vector2 v = Vector2.Zero;

            foreach (Boid boid in Game1.Boids)
            {
                if (boid != this)
                {
                    v = v + boid.velocity;
                }
            }

            v = v / (float)(Game1.Boids.Count - 1);

            return (v - velocity) / 8f;
        }

        Vector2 moveTowards(Vector2 target)
        {
            Vector2 v = (target - position);
            return v / 100f;
        }

        Vector2 avoidWalls()
        {
            Vector2 v = new Vector2();

            foreach (Wall wall in Game1.Walls)
            {
                float dot = Vector2.Dot(position, wall.position);
                    float force = 1f / (Vector2.DistanceSquared(position, wall.position) + 1);
                    Vector2 normal = new Vector2(1, 0);
                    v += normal * force;
            }

            return v;
        }
    }
}
