using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DropMission.ScreensManager;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropMission.Screens.Menu
{
    public class PauseMenuScreen : MenuScreen
    {
        #region Constructor

        public PauseMenuScreen()
        {
            // Flag that there is no need for the game to transition
            // off when the pause menu is on top of it.
            IsPopup = true;

            TransitionOnTime = TimeSpan.FromSeconds(0.2);
            TransitionOffTime = TimeSpan.FromSeconds(0.2);

            StartPositionMenu = new Vector2(100, 150);

            
        }

        #endregion

        #region LoadContent

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;

            SelectedOption = content.Load<Texture2D>("Menu//MainMenu//Star");
            MenuEntries.Add(content.Load<Texture2D>("Menu//MainMenu//startGame"));
            MenuEntries.Add(content.Load<Texture2D>("Menu//MainMenu//quit"));
        }

        #endregion

        #region MenuOperations

        public override void MenuSelect(int selected)
        {
            switch (selected)
            {
                case 0: ExitScreen(); break;

                case 1: MenuCancel(); break;
            }
        }

        public override void MenuCancel()
        {
            //ScreenManager.AddScreen(new MessageBoxScreen("Are you sure you want to exit?"));
            ExitScreen();
            ScreenManager.AddScreen(new MainMenuScreen());
        }

        #endregion

        #region Draw

        public override void Draw(GameTime gameTime)
        {
            // Darken down any other screens that were drawn beneath the popup.
            ScreenManager.FadeBackBufferToBlack(ScreenAlpha * 2 / 3);

            base.Draw(gameTime);
        }

        #endregion
    }
}
