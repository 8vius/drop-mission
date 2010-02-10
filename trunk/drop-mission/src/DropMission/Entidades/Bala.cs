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


namespace DropMission.Entidades
{
    public class Bala
    {
        #region Atributos para animacion

        private const int spriteWidth = 20;
        private const int spriteHeight = 20;
        private Vector2 velocity;

        private bool alive;


        private Vector2 posicion;
        private Rectangle sourceRect;
        private Rectangle destinationRect;
        private Texture2D spriteSheet;

        #endregion

        #region Atributos de posicionamiento

        public Vector2 Posicion
        {
            get { return posicion; }
            set 
            {
                posicion = value;
                destinationRect.X = int.Parse(Math.Truncate(posicion.X).ToString());
                destinationRect.Y = int.Parse(Math.Truncate(posicion.Y).ToString());
            }
        }
        public Vector2 Velocidad
        {
            get { return velocity; }
            set { velocity = value; }
        }
        #endregion

        #region Propiedades
        public Rectangle RectanguloFuente
        {
            get
            {
                return sourceRect;
            }
            set
            {
                sourceRect = value;
            }
        }

        public Rectangle RectanguloDestino
        {
            get
            {
                return destinationRect;
            }
            set
            {
                destinationRect = value;
            }
        }

        public Texture2D SpriteBala
        {
            get
            {
                return spriteSheet;
            }
            set
            {
                spriteSheet = value;
            }
        }

        

        public int SpriteWidth
        {
            get { return spriteWidth; }
        }

        public int SpriteHeight
        {
            get { return spriteHeight; }
        }

        public bool Vivo
        {
            get { return alive; }
            set { alive = value; }
        }
        #endregion

        public Bala()
        {
            destinationRect = new Rectangle(100, 450, spriteWidth, spriteHeight);
            Velocidad = Vector2.Zero;
            Vivo = false;
            
        }

        public void Mover()
        {
            posicion += velocity;

            if (Posicion.X < -SpriteBala.Width || Posicion.X > (800 + SpriteBala.Width))
                Vivo = false;
            if (Posicion.Y < SpriteBala.Height || Posicion.Y > (600 + SpriteBala.Height))
                Vivo = false;
        }
    }
}
