using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class SpriteBase : DrawableGameComponent
    {
        private int spriteWidth;
        private int spriteHeight;
        protected float timer;
        private float interval;
        private int frameCount;
        private int currentFrame = 0;
        private int posicionXanterior;
        private Rectangle sourceRect;
        private Rectangle destinationRect;
        protected Texture2D spriteSheet;
        private SpriteBatch spriteBatch;

        public SpriteBase(Game game) : base(game)
        {
            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            ObtenerDatos();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public void ObtenerDatos()
        {
            this.spriteWidth = (int)SpritePlayer.width;
            this.spriteHeight = (int)SpritePlayer.height;
            this.frameCount = (int)SpritePlayer.frames;
            this.interval = (float)Frames.interval;
            destinationRect = new Rectangle(100, 450, spriteWidth, spriteHeight);
            posicionXanterior = 100;
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            spriteSheet = base.Game.Content.Load<Texture2D>("Sprites//Player//walk");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(spriteSheet, destinationRect, sourceRect, Color.White);
            base.Draw(gameTime);
        }
    }
}
