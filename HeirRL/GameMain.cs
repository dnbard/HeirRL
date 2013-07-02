#region Using Statements
using System;
using System.Collections.Generic;
using HeirRL.Scenes;
using HeirRL.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace HeirRL
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameMain : Game
    {
        GraphicsDeviceManager graphics;
        internal SpriteBatch _spriteBatch;
        public TimeSpan GameTime = TimeSpan.FromMilliseconds(0);

        public Vector2 Viewport
        {
            get
            {
                return new Vector2(graphics.PreferredBackBufferWidth, 
                    graphics.PreferredBackBufferHeight);
            }
        }

        public GameMain()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Window.SetPosition(new Point(0, 0));
            Window.SetResolution(graphics, new Point(1024, 768));
            //graphics.PreferredBackBufferHeight = 768;
            //graphics.PreferredBackBufferWidth = 1024;
            //graphics.IsFullScreen = false;
            //graphics.ApplyChanges();
        }
        
        protected override void Initialize()
        {
            base.Initialize();
            GraphicsDevice.Clear(Color.Black);
            Window.Title = Database.Service.GetApplicationName();
            Window.AllowUserResizing = false;

            SceneManager.Current = new SceneLevel();
            Components.Add(MouseManager.Instance);
            Components.Add(KeyboardManager.Instance);
        }
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        protected override void Update(GameTime gameTime)
        {
            GameTime = gameTime.TotalGameTime;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.F4))
            {
                graphics.IsFullScreen = !graphics.IsFullScreen;
                graphics.ApplyChanges();
            }

            base.Update(gameTime);
            SceneManager.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SceneManager.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
