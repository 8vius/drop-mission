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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using DropMission.Entidades;

namespace DropMission
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState previousKeyboardState = Keyboard.GetState();
        Player player1;
        private FPS fps;
        private InputHandler input;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            player1 = new Player();

            input = new InputHandler(this, player1);
            Components.Add(input);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
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
           
            // TODO: use this.Content to load your game content here
            player1.SpriteCaminar = Content.Load<Texture2D>("Sprites//Player//walk");
            player1.SpriteSaltar = Content.Load<Texture2D>("Sprites//Player//jump");
            player1.arma.SpriteArma = Content.Load<Texture2D>("Sprites//Weapon//M16");

            List<Bala> balas = new List<Bala>();

            for (int i = 0; i < 30; i++)
            {
                balas.Add(new Bala());
                balas.ElementAt(i).SpriteBala = Content.Load<Texture2D>("Sprites//Weapon//bala");
            }

            player1.arma.Balas = balas;

            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            player1.CalcularTimer(gameTime);

            base.Update(gameTime);
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

            if (player1.Status.Equals(""))
            {
                spriteBatch.Draw(player1.SpriteCaminar, player1.RectanguloDestino, player1.RectanguloFuente, Color.White);
                spriteBatch.Draw(player1.arma.SpriteArma, player1.arma.RectanguloDestino, player1.arma.RectanguloFuente, Color.White);
            }
            if (player1.Status.Equals("SALTO"))
            {
                spriteBatch.Draw(player1.SpriteSaltar, player1.RectanguloDestino, player1.RectanguloFuente, Color.White);
            }

            foreach (Bala bala in player1.arma.Balas)
            {
                if (bala.Vivo)
                {
                    spriteBatch.Draw(bala.SpriteBala,
                                     bala.RectanguloDestino,
                                     bala.RectanguloFuente,
                                     Color.White);
                }
            }

            spriteBatch.End();

            


            base.Draw(gameTime);
        }
    }
}
