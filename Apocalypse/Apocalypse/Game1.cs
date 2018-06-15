using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Apocalypse {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ApocalypeEngine : Game {
        // Monogame stuff
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Core stuff:
        private CoreEngine.TextureLib textures;
        private CoreEngine.CollisionDetection.CollisionManager collisionManager;

        // Static stuff
        public static Random rnd;

        // Game stuff:
        CoreEngine.Scene curScene = null;

        /// <summary>
        /// Creates a new isntance of ApocalypseEngine:
        /// </summary>
        public ApocalypeEngine() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1334;
            graphics.PreferredBackBufferHeight = 750;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // Load managers:
            textures = new CoreEngine.TextureLib();
            collisionManager = new CoreEngine.CollisionDetection.CollisionManager(new Rectangle(0, 0, 1334, 750), true);
            ApocalypeEngine.rnd = new Random();

            // super thing..
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load in content: 
            textures.AddTexture("background", Content.Load<Texture2D>("background"));
            textures.AddTexture("blueprojectile", Content.Load<Texture2D>("blueprojectile"));
            textures.AddTexture("bluesprite", Content.Load<Texture2D>("bluesprite"));
            textures.AddTexture("redprojectile", Content.Load<Texture2D>("redprojectile"));
            textures.AddTexture("redsprite", Content.Load<Texture2D>("redsprite"));
            textures.AddTexture("damage", Content.Load<Texture2D>("damage"));
            textures.AddTexture("deadsprite", Content.Load<Texture2D>("deadsprite"));

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // SetScene
            if ( curScene == null) {
                curScene = new Scenes.BattleScene();
            }
            
            // TODO: Add your update logic here
            if (curScene != null) {
                foreach (CoreEngine.GameObject gO in curScene.sceneObjects) {
                    gO.Update( gameTime );
                }
            }

            curScene.Update();
            collisionManager.Update(curScene.sceneObjects);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Start drawing
            spriteBatch.Begin();

            // Add all gameobjects from our current scene.
            if (curScene != null) {
                curScene.ReorderObjects();

                foreach (CoreEngine.GameObject gO in curScene.sceneObjects) {
                    gO.Draw(spriteBatch);
                }
            }

            // collisionManager.Draw(spriteBatch, this.GraphicsDevice);

            // Stop drawing
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
