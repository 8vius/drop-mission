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
    public class Weapon
    {
        #region Atributos para animacion

        private const int spriteWidth = 150;
        private const int spriteHeight = 100;
        private float rotation;

        private int frameCount = 5;
        private int currentFrame = 0;

        private Rectangle sourceRect;
        private Rectangle destinationRect;
        private Texture2D spriteSheet;

        private List<Bala> clip;

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

        public float Rotacion
        {
            get { return rotation; }
            set { rotation = value; }
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

        public Texture2D SpriteArma
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

        public List<Bala> Balas
        {
            get { return clip; }
            set { clip = value; }
        }

        public int FrameActual
        {
            get
            {
                return currentFrame;
            }
            set
            {
                currentFrame = value;
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

        public Weapon()
        {
            destinationRect = new Rectangle(100, 450, spriteWidth, spriteHeight);
            Rotacion = (float)-MathHelper.PiOver4;//0.0f;
        }
    }
}
