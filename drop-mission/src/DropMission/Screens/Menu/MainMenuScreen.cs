using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using DropMission.ScreensManager;

namespace DropMission.Screens.Menu
{
    public class MainMenuScreen : MenuScreen
    {
        #region Fields and Properties

        //Imagen del fondo
        Texture2D background;

        //Imagen del titulo
        Texture2D title;

        //Imagen del jugador
        Texture2D player;

        //Posicion inicial del titulo
        Vector2 startPositionTitle;

        //Posicion inicial del jugador
        Vector2 startPositionPlayer;

        #endregion

        #region Constructor

        public MainMenuScreen()
        {
            StartPositionMenu = new Vector2(450, 400);
            startPositionPlayer = new Vector2(15, 300);
            startPositionTitle = new Vector2(420, 25);
        }

        #endregion

        #region LoadContent

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;

            background = content.Load<Texture2D>("Menu//MainMenu//Fondo");
            player = content.Load<Texture2D>("Menu//MainMenu//Player");
            title = content.Load<Texture2D>("Menu//MainMenu//title");
            SelectedOption = content.Load<Texture2D>("Menu//MainMenu//Star");
            MenuEntries.Add(content.Load<Texture2D>("Menu//MainMenu//startGame"));
            MenuEntries.Add(content.Load<Texture2D>("Menu//MainMenu//options"));
            MenuEntries.Add(content.Load<Texture2D>("Menu//MainMenu//quit"));
        }

        #endregion

        #region UnloadContent

        public override void UnloadContent()
        {
            if (background != null)
                background = null;

            if (player != null)
                player = null;

            if (title != null)
                title = null;
        }

        public override void Remove()
        {
            base.Remove();
            MenuEntries.Clear();
        }

        #endregion

        #region MenuOperations

        public override void MenuSelect(int selected)
        {
            switch (selected)
            {
                case 0: //ExitScreen();
                        ScreenManager.AddScreen(new Desierto()); break;

                case 2: MenuCancel(); break;
            }
        }

        public override void MenuCancel()
        {
            ScreenManager.AddScreen(new MessageBoxScreen("Are you sure you want to exit?"));
        }

        #endregion

        #region Draw

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Viewport viewport = ScreenManager.Game.GraphicsDevice.Viewport;

            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, viewport.Width, viewport.Height), Color.White);

            spriteBatch.Draw(title, new Rectangle((int)startPositionTitle.X, (int)startPositionTitle.Y,
                title.Width, title.Height), Color.White);

            spriteBatch.Draw(player, new Rectangle((int)startPositionPlayer.X, (int)startPositionPlayer.Y,
                player.Width, player.Height), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion
    }
}
