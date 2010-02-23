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

namespace DropMission.Entidades.Enemigos
{
    public enum estadoKamikaze
    {
        Caminando,
        Explotando
    };

    public enum posicionKamikaze
    {
        Izquierda,
        Derecha
    };

    public class Kamikaze : Enemigo
    {
        #region Atributos para animacion

        private const int spriteWidth = 150;
        private const int spriteHeight = 100;
        private float timer = 0f;
        private int currentFrame = 0;
        private int frameCount = 4;
        private float interval = 1000 / 15;
        private Rectangle sourceRect;
        private Rectangle destinationRect;
        private Texture2D spriteSheetWalk;
        private Texture2D spriteSheetExplode;

        private bool vivo;

        private int posicionXanterior;

        #endregion

        #region Atributos de posicionamiento

        int PosicionX
        {
            get { return destinationRect.X; }
            set { destinationRect.X = value; }
        }

        int PosicionY
        {
            get { return destinationRect.Y; }
            set { destinationRect.Y = value; }
        }


        #endregion


        #region Propiedades

        public virtual Rectangle RectanguloFuente
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }

        public virtual Rectangle RectanguloDestino
        {
            get { return destinationRect; }
            set { destinationRect = value; }
        }

        public virtual Texture2D SpriteCaminar
        {
            get { return spriteSheetWalk; }
            set { spriteSheetWalk = value; }
        }

        public virtual Texture2D SpriteExplotar
        {
            get { return spriteSheetExplode; }
            set { spriteSheetExplode = value; }
        }

        public bool Vivo
        {
            get { return vivo; }
            set { vivo = value; }
        }

        #endregion

        public estadoKamikaze Status;

        public Kamikaze(posicionKamikaze Posicion)
        {
            this.Status = estadoKamikaze.Caminando;
            this.vivo = true;

            if (Posicion == posicionKamikaze.Derecha)
            {
                destinationRect = new Rectangle(900, 450, spriteWidth, spriteHeight);
                posicionXanterior = 900;
            }
            if (Posicion == posicionKamikaze.Izquierda)
            {
                destinationRect = new Rectangle(-100, 450, spriteWidth, spriteHeight);
                posicionXanterior = -100;
            }
                       
        }

        #region Metodos

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
            PosicionX += 5;
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
            PosicionX -= 5;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.SpriteCaminar, 
                             this.RectanguloDestino, 
                             this.RectanguloFuente, 
                             Color.White);

            
        }

        #endregion
    }
}
