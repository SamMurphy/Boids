using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Boids
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        

        static public List<GameObject> Objects = new List<GameObject>();
        static public List<Boid> Boids = new List<Boid>();
        static public List<Wall> Walls = new List<Wall>();
        static public Dictionary<string, Texture2D> TextureDictionary = new Dictionary<string, Texture2D>();

        public Vector2 Target;

        int cohesion = 1;
        int avoidance = 12;
        int alignment = 1;
        int goal = 10;

        int numberOfBoids = 500;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Window.Title = Target.X + ", " + Target.Y;

            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Add Textures
            Texture2D whiteRectangle = new Texture2D(GraphicsDevice, 1, 1);
            whiteRectangle.SetData(new[] { Color.White });
            TextureDictionary.Add("whiteRectangle", whiteRectangle);

            // Add boids
            System.Random rand = new System.Random();
            for (int i = 0; i < numberOfBoids; i++)
            {
                Boid b = new Boid(new Vector2(rand.Next(0, graphics.PreferredBackBufferWidth), rand.Next(0, graphics.PreferredBackBufferHeight)));
                Objects.Add(b);
                Boids.Add(b);
            }

            Wall w = new Wall(new Vector2(100, 100), 5, 200);
            Objects.Add(w);
            Walls.Add(w);

            // Init Parameters
            Target = new Vector2(0, 0);


            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            foreach (Texture2D texture in TextureDictionary.Values)
            {
                texture.Dispose();
            }
            TextureDictionary.Clear();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState mouse = Mouse.GetState();
            KeyboardState key = Keyboard.GetState();

            if (mouse.RightButton == ButtonState.Pressed) Target = new Vector2(mouse.X, mouse.Y);
            if (key.IsKeyDown(Keys.Q)) cohesion = -1;
            if (key.IsKeyDown(Keys.W)) cohesion = 1;
            if (key.IsKeyDown(Keys.E)) goal += 1;
            if (key.IsKeyDown(Keys.R)) goal -= 1;

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (Boid b in Boids)
            {
                b.UpdateBoidMovement(gameTime, Target, cohesion, avoidance, alignment, goal);
            }

            Window.Title = Target.X + ", " + Target.Y + " " + goal + " " + cohesion;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // TODO: Add your drawing code here
            foreach (GameObject obj in Objects)
            {
                obj.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
