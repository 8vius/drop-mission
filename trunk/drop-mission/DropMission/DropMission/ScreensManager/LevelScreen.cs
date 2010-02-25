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

        Texture2D backgroundLejos;
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
            base.Update(gameTime, covered);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Rectangle viewportRect = new Rectangle(0,0,ScreenManager.Game.GraphicsDevice.Viewport.Width,
                                                       ScreenManager.Game.GraphicsDevice.Viewport.Height);

            spriteBatch.Begin();

            spriteBatch.Draw(backgroundCerca, viewportRect, Color.White);
            jugador.Draw(spriteBatch);

            spriteBatch.End();
        }

    }
}
