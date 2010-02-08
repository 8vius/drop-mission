﻿using System;
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

namespace DropMission
{
    public class Weapon
    {
        #region Atributos para animacion

        private const int spriteWidth = 150;
        private const int spriteHeight = 100;

        private int frameCount = 5;
        private int currentFrame = 0;

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

        public Weapon()
        {
            destinationRect = new Rectangle(100, 450, spriteWidth, spriteHeight);
        }

        public virtual Rectangle RectanguloFuente
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

        public virtual Rectangle RectanguloDestino
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

        public virtual Texture2D SpriteArma
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

        public virtual int FrameActual
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

        public virtual int SpriteAncho
        {
            get
            {
                return spriteWidth;
            }
        }

        public virtual int SpriteAlto
        {
            get
            {
                return spriteHeight;
            }
        }
    }
}
