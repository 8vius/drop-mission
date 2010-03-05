using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DropMission.Entidades;
using Microsoft.Xna.Framework.Content;

namespace DropMission
{

    //Clase maestra de enemigos, todos los enemigos normales heredaran de ella
    public abstract class Enemy
    {
           
        #region Atributos de Animacion

        const int spriteHeight = 100;
        public int SpriteHeight
        {
            get { return spriteHeight; }
        }

        const int spriteWidth = 150;
        public int SpriteWidth
        {
            get { return spriteWidth; }
        }

        float timer = 0f;
        public float Timer
        {
            get { return timer; }
            set { timer = value; }
        }

        int currentFrame = 0;
        public int CurrentFrame
        {
            get { return currentFrame; }
            set { currentFrame = value; }
        }

        int frameCount = 4;
        public int FrameCount
        {
            get { return frameCount; }
            set { frameCount = value; }
        }

        float interval = 1000 / 15;
        public float Interval
        {
            get { return interval; }
        }

        Rectangle sourceRect;
        public Rectangle SourceRect
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }

        Rectangle destinationRect;
        public Rectangle DestinationRect
        {
            get { return destinationRect; }
            set { destinationRect = value; }
        }

        Texture2D spriteSheetAlive;
        public Texture2D SpriteSheetAlive
        {
            get { return spriteSheetAlive; }
            set { spriteSheetAlive = value; }
        }

        Texture2D spriteSheetDead;
        public Texture2D SpriteSheetDead
        {
            get { return spriteSheetDead; }
            set { spriteSheetDead = value; }
        }

        bool alive;
        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }

        #endregion

        #region Atributos de posicionamiento

        public int PosicionX
        {
            get { return destinationRect.X; }
            set { destinationRect.X = value; }
        }

        public int PosicionY
        {
            get { return destinationRect.Y; }
            set { destinationRect.Y = value; }
        }

        #endregion

        #region Gadgets

        public Weapon arma;
        public Weapon Arma
        {
            get { return arma; }
            set { arma = value; }
        }

        #endregion

        #region Constructor

        public Enemy()
        { }

        #endregion

        #region Metodos

        public virtual void CalcularTimer(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteSheetAlive,
                             destinationRect,
                             sourceRect,
                             Color.White);

            if (arma != null)
            {
                spriteBatch.Draw(arma.SpriteArma,
                                 arma.RectanguloDestino,
                                 arma.RectanguloFuente,
                                 Color.White);
                foreach (Bala bala in Arma.Balas)
                {
                    if (bala.Vivo)
                    {
                        spriteBatch.Draw(bala.SpriteBala,
                                         bala.Posicion,
                                         Color.White);
                    }
                }
            }


        }

        public abstract void Update(GameTime gameTime, Rectangle playerRect);

        public abstract void LoadContent(ContentManager content);
        
        #endregion
    }
}
