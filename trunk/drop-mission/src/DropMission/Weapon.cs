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

namespace DropMission
{
    public class Weapon
    {
        #region Atributos para animacion
        public int spriteWidth = 187;
        public int spriteHeight = 100;

        float timer = 0f;
        float interval = 1000f / 15f;
        public int frameCount = 4;
        public int currentFrame = 0;

        public Rectangle sourceRect;
        public Rectangle destinationRect;
        public Texture2D spriteSheetWalk;
        public Texture2D spriteSheetJump;

        int posicionXanterior;
        int tiempoDeSalto = 0;
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

        public void Reset()
        {
            int sourceY = sourceRect.Y;

            currentFrame = 0;
            timer = 0f;
            sourceRect = new Rectangle(currentFrame * spriteWidth, sourceRect.Y, spriteWidth, spriteHeight);
        }
    }
}
