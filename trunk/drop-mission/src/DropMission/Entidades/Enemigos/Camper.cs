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
    public enum miradaCamper
    {
        Izquierda,
        Derecha
    };

    public class Camper
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
        private Texture2D spriteSheetPosition;
        private Texture2D spriteSheetExplode;

        private bool vivo;

        private miradaCamper Mirada;

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

        public virtual Texture2D SpritePosicion
        {
            get { return spriteSheetPosition; }
            set { spriteSheetPosition = value; }
        }

        

        public bool Vivo
        {
            get { return vivo; }
            set { vivo = value; }
        }

        #endregion

        #region Gadgets

        public Weapon arma;

        #endregion

        public Camper(int X, int Y)
        {
            Mirada = miradaCamper.Izquierda;
            vivo = true;
            destinationRect = new Rectangle(X, Y, spriteWidth, spriteHeight);
            sourceRect = new Rectangle(0, 100, spriteWidth, spriteHeight);
            arma = new Weapon(destinationRect);
           

        }

        #region Metodos

        public void CalcularTimer(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }
        public void Girar()
        { }
        public void Disparar()
        { }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.SpritePosicion,
                             this.RectanguloDestino,
                             this.RectanguloFuente,
                             Color.White);


        }
        #endregion

    }
}
