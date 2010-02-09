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


        private Rectangle sourceRect;
        private Rectangle destinationRect;
        private Texture2D spriteSheet;

        #endregion

        #region Atributos de posicionamiento
        public int PosicionX
        {
            get
            {
                return destinationRect.X;
            }
            set
            {
                destinationRect.X = value;
            }
        }

        public int PosicionY
        {
            get
            {
                return destinationRect.Y;
            }
            set
            {
                destinationRect.Y = value;
            }
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
        #endregion

        public Bala()
        {
            destinationRect = new Rectangle(100, 450, spriteWidth, spriteHeight);
        }
    }
}
