using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DropMission.Entidades;

namespace DropMission.ScreensManager
{
    public abstract class LevelScreen : GameScreen
    {
        #region Fields and Properties

        /// <summary>
        /// Posicion de la camara para mover el fondo
        /// </summary>
        float cameraPosition;
        public float CameraPosition
        {
            get { return cameraPosition; }
            set { cameraPosition = value; }
        }

        /// <summary>
        /// Capas del escenario (fondo lejos, cerca, hud)
        /// </summary>
        LevelLayer[] layers;
        public LevelLayer[] Layers
        {
            get { return layers; }
            set { layers = value; }
        }

        /// <summary>
        /// La instancia del jugador en el nivel
        /// </summary>
        Player jugador;
        public Player Jugador
        {
            get { return jugador; }
            set { jugador = value; }
        }

        /// <summary>
        /// un listado de todas las plataformas del nivel
        /// </summary>
        List<Plataforma> plataformas;
        public List<Plataforma> Plataformas
        {
            get { return plataformas; }
            set { plataformas = value; }
        }

        /// <summary>
        /// Lista que contiene a todos los enemigos del nivel
        /// </summary>
        List<Enemy> enemigos;
        public List<Enemy> Enemigos
        {
            get { return enemigos; }
            set { enemigos = value; }
        }

        #region Propiedades para la transparencia
        /// <summary>
        /// Es el pixel que utilizamos para las transparencias al entrar
        /// y salir de la pantalla.
        /// </summary>
        public Texture2D Pixel
        {
            get { return pixel; }
            set { pixel = value; }
        }
        Texture2D pixel;

        /// <summary>
        /// La opacidad de la transparencia en las
        /// transiciones de pantalla.
        /// </summary>
        public float FadeOpacity
        {
            get { return fadeOpacity; }
            set { fadeOpacity = value; }
        }
        float fadeOpacity;

        /// <summary>
        /// El Color de la transparencia de las transiciones
        /// de pantalla (Normalmente es negro)
        /// </summary>
        public Color FadeColor
        {
            get { return fadeColor; }
            set { fadeColor = value; }
        }
        Color fadeColor;
        #endregion 

        #endregion

        /// <summary>
        /// Constructor de la clase y solo asigna 1.5 segundos
        /// a las transiciones de entrada y salida de la pantalla
        /// </summary>
        public LevelScreen()
        {
            TransitionOnTime = TransitionOffTime = TimeSpan.FromSeconds(1.5);
        }

        public override void Update(GameTime gameTime, bool covered)
        {
            //Esto es para calcular un tiempo de delay para las animaciones
            //Posiblemente lo podamos cambiar para q funcione mejor
            jugador.CalcularTimer(gameTime);

            #region Prueba de colisiones con plataformas
            //Colisiones
            Vector2 centroPlayer = new Vector2(jugador.PosicionX + (jugador.RectanguloDestino.Width / 2),
                                               jugador.PosicionY + jugador.RectanguloDestino.Height - 30);

            foreach (Plataforma p in plataformas)
            {
                //El jugador esta en la posicion x de la plataforma
                if (p.Posicion.X < centroPlayer.X && centroPlayer.X < (p.Posicion.X + p.Width))
                {
                    //el jugador esta colisionando con la plataforma
                    if (p.Posicion.Y < centroPlayer.Y && centroPlayer.Y < (p.Posicion.X + p.Width))
                    {
                        if (jugador.Status == estadoPlayer.Saltando && jugador.posicionYanterior < jugador.PosicionY)
                        {
                            jugador.Status = estadoPlayer.Caminando;
                            jugador.tiempoDeSalto = 0;
                            jugador.PosicionY = ((int)p.Posicion.Y) - jugador.RectanguloDestino.Height + 30;
                        }
                    }
                }
                else
                { 
                    //Aqui reviso cuando el personaje sale de la plataforma
                    if (jugador.Status == estadoPlayer.Caminando && jugador.PosicionY < 480)
                    {
                        jugador.Caer();
                    }
                }
            }
            
            #endregion

            //Hace un update de movimiento de las balas que fueron disparadas
            foreach (Bala bala in jugador.arma.Balas)
            {
                if (bala.Vivo)
                {
                    bala.Mover(jugador.RectanguloDestino);
                }
            }

            foreach (Enemy enemigo in enemigos)
            {
                enemigo.Update(gameTime,jugador.RectanguloDestino);
            }

            base.Update(gameTime, covered);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Rectangle viewportRect = new Rectangle(0,0,ScreenManager.Game.GraphicsDevice.Viewport.Width,
                                                       ScreenManager.Game.GraphicsDevice.Viewport.Height);

            spriteBatch.Begin();
            for (int i = 0; i <= 1; ++i)
                layers[i].Draw(spriteBatch, cameraPosition);
            spriteBatch.End();

            ScrollCamera(spriteBatch.GraphicsDevice.Viewport);
            Matrix cameraTransform = Matrix.CreateTranslation(-cameraPosition, 0.0f, 0.0f);
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, cameraTransform);

            jugador.Draw(spriteBatch);
            //Dibujar todas las plataformas del nivel
            foreach (Plataforma plat in plataformas)
            {
                plat.Draw(spriteBatch);
            }

            foreach (Enemy enemigo in enemigos)
            {
                enemigo.Draw(spriteBatch);
                enemigo.CalcularTimer(gameTime);
            }

            spriteBatch.End();

            spriteBatch.Begin();
            for (int i = 1 + 1; i < layers.Length; ++i)
                layers[i].Draw(spriteBatch, cameraPosition);
            spriteBatch.End();
        }

        private void ScrollCamera(Viewport viewport)
        {
            // Esta es la medida donde queremos que se pare el player
            // Lo intentas poner a mas de 0.5 y estalla asi q mejor usa
            // el vector2 de posicion jugador que esta mas abajo
            const float ViewMargin = 0.5f;

            // Aca se calculan los bordes de la pantalla
            float marginWidth = viewport.Width * ViewMargin;
            float marginLeft = cameraPosition + marginWidth;
            float marginRight = cameraPosition + viewport.Width - marginWidth;

            //Para calcular la posicion en la que queremos que el jugador deje de avanzar y se mueva el fondo
            Vector2 PosicionJugador = new Vector2(jugador.RectanguloDestino.X + 150, jugador.RectanguloDestino.Y);

            // Aca se calcula que tanto se debe scrollear cuando el player esta al borde de la pantalla
            float cameraMovement = 0.0f;
            if (PosicionJugador.X < marginLeft)
                cameraMovement = PosicionJugador.X - marginLeft;
            else if (PosicionJugador.X > marginRight)
                cameraMovement = PosicionJugador.X - marginRight;

            // Aca se hace update de la position de la camara para que no se salga del nivel
            // aun tengo que determinar que tanto va a medir el nivel para hacerlo bien, le puse 5000 por random
            float maxCameraPosition = 5000;
            cameraPosition = MathHelper.Clamp(cameraPosition + cameraMovement, 0.0f, maxCameraPosition);
        }

    }
}
