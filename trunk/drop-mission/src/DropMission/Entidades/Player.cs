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
    /// <summary>
    /// Estados del jugador
    /// </summary>
    public enum estadoPlayer
    { 
        Caminando, 
        Saltando,
        Estatico,
        Callendo
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
        private string direccion = "";

        //lo puse public para hacer algo con las colisiones de las plataformas
        //lo cambiamos ahora
        public int posicionXanterior;
        public int posicionYanterior;
        public int tiempoDeSalto = 0; 

        #endregion

        #region Atributos de posicionamiento

        /// <summary>
        /// Propiedad para cambiar la posicion X de impresion del
        /// jugador en la pantalla.
        /// </summary>
        public int PosicionX 
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

        /// <summary>
        /// Propiedad para cambiar la posicion Y de impresion del
        /// jugador en la pantalla.
        /// </summary>
        public int PosicionY
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

        //Aqui se colocan todos los items del jugador como son
        //armas y granadas.
        public Weapon arma;

        #endregion

        #region Propiedades

        /// <summary>
        /// Rectangulo que contiene la informacion del sprite dentro 
        /// del spriteSheet a ser utilizado 
        /// </summary>
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

        /// <summary>
        /// Rectangulo donde se va a imprimir el sprite; los atributos 
        /// de posicionamiento cambian las cordenadas de este rectangulo
        /// </summary>
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

        /// <summary>
        /// SpriteSheet con todos los frames de animacion del personaje
        /// caminando
        /// </summary>
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

        /// <summary>
        /// SpriteSheet con todos los frames de animacion del personaje
        /// saltando
        /// </summary>
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

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
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

        /// <summary>
        /// Aqui se reinician las animaciones de movimiento
        /// </summary>
        public void Reset()
        {
            int sourceY = sourceRect.Y;

            currentFrame = 0;
            timer = 0f;
            sourceRect = new Rectangle(currentFrame * spriteWidth, sourceRect.Y, spriteWidth, spriteHeight);

            arma.RectanguloFuente = new Rectangle(0, sourceRect.Y, arma.SpriteWidth, arma.SpriteHeight);
            if (sourceRect.Y == 0)
            {
                this.direccionApuntado = posicionArma.Derecha;
                arma.Rotacion = 0;
            }
            else
            {
                this.direccionApuntado = posicionArma.Izquierda;
                arma.Rotacion = (float)Math.PI;
            }
        }

        /// <summary>
        /// Es un metodo que calcula el timer que es utilizado para
        /// hacer las animaciones en el tiempo que queramos
        /// </summary>
        /// <param name="gameTime">El gametime</param>
        public void CalcularTimer(GameTime gameTime)
        { 
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        /// <summary>
        /// Es el metodo que se encarga de mover y animar al personaje a la derecha
        /// </summary>
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

        /// <summary>
        /// Es el metodo que se encarga de mover y animar al personaje a la izquierda
        /// </summary>
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

        public void Estatico()
        {
            Status = estadoPlayer.Estatico;

            if (direccion == "Derecha")
            {
                sourceRect = new Rectangle(0, 0, spriteWidth, spriteHeight);
                arma.RectanguloFuente = new Rectangle(0, 0, arma.SpriteWidth, arma.SpriteHeight);
            }
            else
            {
                sourceRect = new Rectangle(0, 100, spriteWidth, spriteHeight);
                arma.RectanguloFuente = new Rectangle(0, 100, arma.SpriteWidth, arma.SpriteHeight);
            }
        }

        /// <summary>
        /// Es el metodo para animar y ejecutar el movimiento de Salto
        /// del personaje
        /// </summary>
        public void Saltar()
        {
            Status = estadoPlayer.Saltando;
            tiempoDeSalto += 1;
            posicionYanterior = PosicionY;
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

        public void Caer()
        {
            Status = estadoPlayer.Callendo;
            posicionYanterior = PosicionY;
            tiempoDeSalto += 1;
            PosicionY = int.Parse(Math.Truncate(PosicionY
                                + (0.1f * Math.Pow(tiempoDeSalto, 2)) / 2).ToString());

            if (timer > interval)
            {
                currentFrame++;
                if (currentFrame > frameCount - 1)
                {
                    currentFrame = 0;
                }
                timer = 0f;
            }

            if (posicionXanterior < PosicionX)
                sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
            else
                sourceRect = new Rectangle(currentFrame * spriteWidth, 100, spriteWidth, spriteHeight);

            if (PosicionY >= 480)
            {
                Status = estadoPlayer.Caminando;
                tiempoDeSalto = 0;
                PosicionY = 480;
            }
        }

        /// <summary>
        /// Gira el arma dependiendo de la posicion en que
        /// se este apuntando
        /// </summary>
        /// <param name="inclinacion">Nueva posicion del arma (sacado del enum)</param>
        public void GirarArma(posicionArma inclinacion)
        {
            direccionApuntado = inclinacion;
            arma.Rotar(inclinacion,sourceRect);
        }

        /// <summary>
        /// Metodo para realizar el disparo. por ahora solo lo hace con 
        /// 1 sola arma pero luego lo hara con cualquier arma adquirida.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Disparar(GameTime gameTime)
        {
            //Con esto lo que logro es hacer un delay a cada disparo para
            //que no salgan varias balas al mismo tiempo
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

                    //Cambio la posicion inicial de la bala para que dispare de la punta del arma
                    switch (direccionApuntado)
                    {
                        case posicionArma.Derecha: 
                            bala.Posicion = new Vector2(arma.PosicionX + 131, arma.PosicionY + 54);
                            break;

                        case posicionArma.ArribaDerecha:
                            bala.Posicion = new Vector2(arma.PosicionX + 118, arma.PosicionY + 12);
                            break;

                        case posicionArma.Arriba:
                            //Verifico en que direccion esta viendo el personaje
                            if(direccion == "Derecha")
                                bala.Posicion = new Vector2(arma.PosicionX + 88, arma.PosicionY - 5);
                            else
                                bala.Posicion = new Vector2(arma.PosicionX + 59, arma.PosicionY - 5);
                            break;

                        case posicionArma.ArribaIzquierda:
                            bala.Posicion = new Vector2(arma.PosicionX + 25, arma.PosicionY + 12);
                            break;

                        case posicionArma.Izquierda:
                            bala.Posicion = new Vector2(arma.PosicionX + 15, arma.PosicionY + 54);
                            break;

                        case posicionArma.AbajoIzquierda:
                            bala.Posicion = new Vector2(arma.PosicionX + 28, arma.PosicionY + 83);
                            break;

                        case posicionArma.Abajo:
                            //Verifico en que direccion esta viendo el personaje
                            if(direccion == "Derecha")
                                bala.Posicion = new Vector2(arma.PosicionX + 100, arma.PosicionY + 90);
                            else
                                bala.Posicion = new Vector2(arma.PosicionX + 50, arma.PosicionY + 90);
                            break;

                        case posicionArma.AbajoDerecha:
                            bala.Posicion = new Vector2(arma.PosicionX + 113, arma.PosicionY + 85);
                            break;
                        default: break;
                    }

                    //coloco la velocidad de la bala dependiendo de la rotacion del arma
                    bala.Velocidad = new Vector2((float)Math.Cos(arma.Rotacion),(float)Math.Sin(arma.Rotacion)) * 18.0f;
                    delay = false;
                    
                    return;
                }
            }
        }

        #endregion

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Status == estadoPlayer.Estatico)
            {
                if (direccion == "Derecha")
                {
                    spriteBatch.Draw(SpriteCaminar, RectanguloDestino, RectanguloFuente, Color.White);
                    spriteBatch.Draw(this.arma.SpriteArma, this.arma.RectanguloDestino, arma.RectanguloFuente, Color.White);
                }
                else
                {
                    spriteBatch.Draw(SpriteCaminar, RectanguloDestino, RectanguloFuente, Color.White);
                    spriteBatch.Draw(this.arma.SpriteArma, this.arma.RectanguloDestino, arma.RectanguloFuente, Color.White);
                }
            }
            //Primero reviso si debo dibujar al jugador caminando
            if (this.Status == estadoPlayer.Caminando)
            {
                //dibujo al jugador y su arma
                spriteBatch.Draw(this.SpriteCaminar, this.RectanguloDestino, this.RectanguloFuente, Color.White);
                spriteBatch.Draw(this.arma.SpriteArma, this.arma.RectanguloDestino, this.arma.RectanguloFuente, Color.White);
            }
            //Reviso si el personaje esta saltando
            if (this.Status == estadoPlayer.Saltando || this.Status == estadoPlayer.Callendo)
            {
                //dibujo al jugador saltando y a su arma
                spriteBatch.Draw(this.SpriteSaltar, this.RectanguloDestino, this.RectanguloFuente, Color.White);
                spriteBatch.Draw(this.arma.SpriteArma, this.arma.RectanguloDestino, this.arma.RectanguloFuente, Color.White);
            }

            //Por ultimo dibujo las balas que ya se hayan disparado
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
