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
    public enum estadoPlayer
    { 
        Caminando, 
        Saltando 
    };

    public class Player
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
        private Texture2D spriteSheetJump;

        private int posicionXanterior;
        private int tiempoDeSalto = 0; 

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
                arma.PosicionX = value;
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
                arma.PosicionY = value;
            }
        }

        #endregion

        #region Gadgets

        public Weapon arma;

        #endregion

        #region Propiedades
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

        public virtual Texture2D SpriteCaminar
        {
            get
            {
                return spriteSheetWalk;
            }
            set
            {
                spriteSheetWalk = value;
            }
        }

        public virtual Texture2D SpriteSaltar
        {
            get
            {
                return spriteSheetJump;
            }
            set
            {
                spriteSheetJump = value;
            }
        }
        #endregion

        int elapsedTime = 0;
        bool delay = true;

        public estadoPlayer Status;
        private posicionArma direccionApuntado = posicionArma.Derecha;

        #region Constructor

        public Player()
        {
            this.Status = estadoPlayer.Caminando;
           
            destinationRect = new Rectangle(100, 480, spriteWidth, spriteHeight);
            posicionXanterior = 100;
            arma = new Weapon(destinationRect);
        }

        #endregion

        #region Metodos

        public void Reset()
        {
            int sourceY = sourceRect.Y;

            currentFrame = 0;
            timer = 0f;
            sourceRect = new Rectangle(currentFrame * spriteWidth, sourceRect.Y, spriteWidth, spriteHeight);

            arma.RectanguloFuente = new Rectangle(0, sourceRect.Y, arma.SpriteWidth, arma.SpriteHeight);
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
            arma.RectanguloFuente = new Rectangle(arma.FrameActual * arma.SpriteWidth, 0, arma.SpriteWidth, arma.SpriteHeight);

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
            arma.RectanguloFuente = new Rectangle(arma.FrameActual * arma.SpriteWidth, 100, arma.SpriteWidth, arma.SpriteHeight);

            posicionXanterior = PosicionX;
            PosicionX -= 5;
        }

        public void Saltar()
        {
            Status = estadoPlayer.Saltando;
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
            if (PosicionY >= 480)
            {
                Status = estadoPlayer.Caminando;
                tiempoDeSalto = 0;
                PosicionY = 480;
            }
        }

        public void GirarArma(posicionArma inclinacion)
        {
            direccionApuntado = inclinacion;

            arma.Rotar(inclinacion,sourceRect);
        }

        public void Disparar(GameTime gameTime)
        {

            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            if(elapsedTime >= 100)
            {
                delay = true;
                elapsedTime = 0;
            }
      
            foreach (Bala bala in arma.Balas)
            {
                if (!bala.Vivo && delay)
                {
                    bala.Vivo = true;
                    
                    if (direccionApuntado == posicionArma.Derecha)//derecha
                        bala.Posicion = new Vector2(arma.PosicionX + 135, arma.PosicionY + 50);
                    if (direccionApuntado == posicionArma.ArribaDerecha)//derecha-arriba
                        bala.Posicion = new Vector2(arma.PosicionX + 120, arma.PosicionY);
                    if (direccionApuntado == posicionArma.Arriba)//arriba
                        bala.Posicion = new Vector2(arma.PosicionX + 70, arma.PosicionY - 10);
                    if (direccionApuntado == posicionArma.ArribaIzquierda)//izquierda-arriba
                        bala.Posicion = new Vector2(arma.PosicionX + 15, arma.PosicionY);
                    if (direccionApuntado == posicionArma.Izquierda)//izquierda
                        bala.Posicion = new Vector2(arma.PosicionX, arma.PosicionY + 50);
                    if (direccionApuntado == posicionArma.AbajoIzquierda)//izquierda-abajo
                        bala.Posicion = new Vector2(arma.PosicionX + 15, arma.PosicionY + 85);
                    if (direccionApuntado == posicionArma.Abajo)//abajo
                        bala.Posicion = new Vector2(arma.PosicionX + 75, arma.PosicionY + 100);
                    if (direccionApuntado == posicionArma.AbajoDerecha)//derecha-abajo
                        bala.Posicion = new Vector2(arma.PosicionX + 115, arma.PosicionY + 90);
                    
                    bala.Velocidad = new Vector2((float)Math.Cos(arma.Rotacion),(float)Math.Sin(arma.Rotacion)) * 13.0f;
                    delay = false;
                    
                    return;
                }
            }
        }

        #endregion

        public void Draw(SpriteBatch spriteBatch)
        {

            if (this.Status == estadoPlayer.Caminando)
            {
                spriteBatch.Draw(this.SpriteCaminar, this.RectanguloDestino, this.RectanguloFuente, Color.White);
                spriteBatch.Draw(this.arma.SpriteArma, this.arma.RectanguloDestino, this.arma.RectanguloFuente, Color.White);
            }
            if (this.Status == estadoPlayer.Saltando)
            {
                spriteBatch.Draw(this.SpriteSaltar, this.RectanguloDestino, this.RectanguloFuente, Color.White);
                spriteBatch.Draw(this.arma.SpriteArma, this.arma.RectanguloDestino, this.arma.RectanguloFuente, Color.White);
            }

            foreach (Bala bala in this.arma.Balas)
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
}
