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
    public class Player
    {
        #region Atributos para animacion
        const int spriteWidth = 150;
        const int spriteHeight = 100;

        float timer = 0f;
        float interval = 1000f / 15f;
        int frameCount = 4;
        int currentFrame = 0;
        
        public Rectangle sourceRect;
        public Rectangle destinationRect;
        public Texture2D spriteSheetWalk;
        public Texture2D spriteSheetJump;

        int posicionXanterior;
        int tiempoDeSalto = 0; 
        #endregion

        #region Atributos de posicionamiento
        int PosicionX 
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

        int PosicionY
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

        public string Status = "";

        public Player()
        {
            destinationRect = new Rectangle(100, 450, spriteWidth, spriteHeight);
            posicionXanterior = 100;
        }

        public void Reset()
        {
            int sourceY = sourceRect.Y;

            currentFrame = 0;
            timer = 0f;
            sourceRect = new Rectangle(currentFrame * spriteWidth, sourceRect.Y, spriteWidth, spriteHeight);
            
        }

        public void CalcularTimer(GameTime gameTime)
        { 
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public void CaminarDerecha()
        {
            if (timer > interval)
            {
                currentFrame++;
                if (currentFrame > frameCount - 1)
                {
                    currentFrame = 0;
                }
                timer = 0f;
            }

            sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);

            posicionXanterior = PosicionX;
            PosicionX += 3;
        }

        public void CaminarIzquierda()
        {
            if (timer > interval)
            {
                currentFrame++;
                if (currentFrame > frameCount - 1)
                {
                    currentFrame = 0;
                }
                timer = 0f;
            }

            sourceRect = new Rectangle(currentFrame * spriteWidth, 100, spriteWidth, spriteHeight);

            posicionXanterior = PosicionX;
            PosicionX -= 3;
        }

        public void Saltar()
        {
            Status = "SALTO";
            tiempoDeSalto += 1;
            PosicionY = int.Parse(Math.Truncate(PosicionY - 1.5f * tiempoDeSalto
                                + (0.1f * Math.Pow(tiempoDeSalto,2)) / 2).ToString());

            if (timer > interval)
            {
                currentFrame++;
                if (currentFrame > frameCount - 1)
                {
                    currentFrame = 0;
                }
                timer = 0f;
            }

            if(posicionXanterior < PosicionX)
                sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
            else
                sourceRect = new Rectangle(currentFrame * spriteWidth, 100, spriteWidth, spriteHeight);

            //Aqui deberia ser colisionando con el piso pero por ahora
            if (PosicionY >= 450)
            {
                Status = "";
                tiempoDeSalto = 0;
                PosicionY = 450;
            }
        }
    }
}
