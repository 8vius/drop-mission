using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DropMission.ScreensManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace DropMission.Screens.Menu
{
    public class MessageBoxScreen : GameScreen
    {
        #region Fields

        string message = "";
        SpriteFont font;
        Vector2 position;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor automatically includes the standard "Enter=ok, Escape=cancel"
        /// usage text prompt.
        /// </summary>
        public MessageBoxScreen(string message)
        {
            const string usageText = "\nEnter = ok" +
                                     "\nEsc = cancel";

            this.message = message + usageText;

            IsPopup = true;

            TransitionOnTime = TimeSpan.FromSeconds(0.2);
            TransitionOffTime = TimeSpan.FromSeconds(0.2);
        }

        #endregion

        #region LoadContent

        /// <summary>
        /// Loads graphics content for this screen. This uses the shared ContentManager
        /// provided by the Game class, so the content will remain loaded forever.
        /// Whenever a subsequent MessageBoxScreen tries to load this same content,
        /// it will just get back another reference to the already loaded data.
        /// </summary>
        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;

            font = content.Load<SpriteFont>("Menu//MessageBox//MessageBoxFont");
        }

        #endregion

        #region HandlerInput

        public override void HandleInput()
        {
            KeyboardHandler input = ScreenManager.Input;

            if(input.WasKeyPressed(Keys.Enter))
                ScreenManager.Game.Exit();

            if (input.WasKeyPressed(Keys.Escape))
                ExitScreen();
        }

        #endregion

        #region Draw

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Viewport viewport = ScreenManager.Game.GraphicsDevice.Viewport;

            // Darken down any other screens that were drawn beneath the popup.
            ScreenManager.FadeBackBufferToBlack(ScreenAlpha * 2 / 3);

            position = new Vector2((viewport.Width / 2) - (message.Length * 2), viewport.Height / 2);

            spriteBatch.Begin();

            spriteBatch.DrawString(font, message, position, Color.White);

            spriteBatch.End();
        }

        #endregion
    }
}
