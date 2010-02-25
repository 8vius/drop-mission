using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DropMission.ScreensManager
{
    public class IntroScreen : GameScreen
    {
        #region Fields and Properties

        /// <summary>
        /// la imagen que se va a mostrar en la pantalla
        /// </summary>
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        Texture2D texture;

        public Texture2D Pixel
        {
            get { return pixel; }
            set { pixel = value; }
        }
        Texture2D pixel;

        /// <summary>
        /// tiempo que la pantalla va a estar activa
        /// </summary>
        public TimeSpan ScreenTime
        {
            get { return screenTime; }
            set { screenTime = value; }
        }
        TimeSpan screenTime;

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

        public IntroScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(3);
            TransitionOffTime = TimeSpan.FromSeconds(3);
            fadeOpacity = 0.25f;
        }

        public override void UnloadContent()
        {
            if (texture != null)
                texture = null;

            if (pixel != null)
                pixel = null;
        }

        public override void Update(GameTime gameTime, bool covered)
        {
            if (ScreenState == ScreenState.Active)
            {
                screenTime = screenTime.Subtract(gameTime.ElapsedGameTime);
                if (screenTime.TotalSeconds <= 0)
                    ExitScreen();
            }

            base.Update(gameTime, covered);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Viewport viewport = ScreenManager.Game.GraphicsDevice.Viewport;
            Vector2 centerTexture = new Vector2((viewport.Width / 2) - (texture.Width / 2),
                (viewport.Height / 2) - (texture.Height / 2));

            spriteBatch.Begin();

            if (texture.Width < viewport.Width || texture.Height < viewport.Height)
                DrawFade(spriteBatch, viewport);

            spriteBatch.Draw(texture, centerTexture, new Color(Color.White, ScreenAlpha));

            spriteBatch.End();
        }

        private void DrawFade(SpriteBatch spriteBatch, Viewport viewport)
        {
            if (pixel != null)
                spriteBatch.Draw(pixel, new Rectangle(0, 0, viewport.Width, viewport.Height),
                    new Color(fadeColor, (byte)(fadeOpacity * 255)));
        }
    }
}
