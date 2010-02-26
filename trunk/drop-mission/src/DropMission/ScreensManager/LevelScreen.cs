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

    /*    Texture2D backgroundLejos;
        public Texture2D BackgroundLejos
        {
            get { return backgroundLejos; }
            set { backgroundLejos = value; }
        }

        Texture2D backgroundCerca;
        public Texture2D BackgroundCerca
        {
            get { return backgroundCerca; }
            set { backgroundCerca = value; }
        } */

        float cameraPosition;
        public float CameraPosition
        {
            get { return cameraPosition; }
            set { cameraPosition = value; }
        }

        LevelLayer[] layers;
        public LevelLayer[] Layers
        {
            get { return layers; }
            set { layers = value; }
        }

        Player jugador;
        public Player Jugador
        {
            get { return jugador; }
            set { jugador = value; }
        }

        #region Propiedades para la transparencia
        public Texture2D Pixel
        {
            get { return pixel; }
            set { pixel = value; }
        }
        Texture2D pixel;

        public float FadeOpacity
        {
            get { return fadeOpacity; }
            set { fadeOpacity = value; }
        }
        float fadeOpacity;

        public Color FadeColor
        {
            get { return fadeColor; }
            set { fadeColor = value; }
        }
        Color fadeColor;
        #endregion 

        #endregion

        public LevelScreen()
        {
            TransitionOnTime = TransitionOffTime = TimeSpan.FromSeconds(1.5);
        }

        public override void Update(GameTime gameTime, bool covered)
        {
            jugador.CalcularTimer(gameTime);

            foreach (Bala bala in jugador.arma.Balas)
            {
                if (bala.Vivo)
                {
                    bala.Mover();
                }

          

            }

            base.Update(gameTime, covered);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Rectangle viewportRect = new Rectangle(0,0,ScreenManager.Game.GraphicsDevice.Viewport.Width,
                                                       ScreenManager.Game.GraphicsDevice.Viewport.Height);

      /*       spriteBatch.Begin();

            spriteBatch.Draw(backgroundCerca, viewportRect, Color.White);
            jugador.Draw(spriteBatch);

            spriteBatch.End(); */

            spriteBatch.Begin();
            for (int i = 0; i <= 1; ++i)
                layers[i].Draw(spriteBatch, cameraPosition);
            spriteBatch.End();

            ScrollCamera(spriteBatch.GraphicsDevice.Viewport);
            Matrix cameraTransform = Matrix.CreateTranslation(-cameraPosition, 0.0f, 0.0f);
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, cameraTransform);

            jugador.Draw(spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin();
            for (int i = 1 + 1; i < layers.Length; ++i)
                layers[i].Draw(spriteBatch, cameraPosition);
            spriteBatch.End();
        }

        private void ScrollCamera(Viewport viewport)
        {
            // Esta es la medida donde queremos que se pare el player
            const float ViewMargin = 0.5f;

            // Aca se calculan los bordes de la pantalla
            float marginWidth = viewport.Width * ViewMargin;
            float marginLeft = cameraPosition + marginWidth;
            float marginRight = cameraPosition + viewport.Width - marginWidth;

            // Aca se calcula que tanto se debe scrollear cuando el player esta al borde de la pantalla
            float cameraMovement = 0.0f;
            if (jugador.RectanguloDestino.X < marginLeft)
                cameraMovement = jugador.RectanguloDestino.X - marginLeft;
            else if (jugador.RectanguloDestino.X > marginRight)
                cameraMovement = jugador.RectanguloDestino.X - marginRight;

            // Aca se hace update de la position de la camara para que no se salga del nivel
            // aun tengo que determinar que tanto va a medir el nivel para hacerlo bien, le puse 5000 por random
            float maxCameraPosition = 5000;
            cameraPosition = MathHelper.Clamp(cameraPosition + cameraMovement, 0.0f, maxCameraPosition);
        }

    }
}
