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

        public MainMenuScreen()
        {
            StartPositionMenu = new Vector2(450, 400);
            StartPositionPlayer = new Vector2(15, 300);
            StartPositionTitle = new Vector2(420, 25);
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;
            Background = content.Load<Texture2D>("Menu//MainMenu//Fondo");
            Player = content.Load<Texture2D>("Menu//MainMenu//Player");
            SelectedOption = content.Load<Texture2D>("Menu//MainMenu//Star");
            Title = content.Load<Texture2D>("Menu//MainMenu//title");
            MenuEntries.Add(content.Load<Texture2D>("Menu//MainMenu//startGame"));
            MenuEntries.Add(content.Load<Texture2D>("Menu//MainMenu//options"));
            MenuEntries.Add(content.Load<Texture2D>("Menu//MainMenu//quit"));
        }

        public override void Remove()
        {
            base.Remove();
            MenuEntries.Clear();
        }

        public override void MenuSelect(int selected)
        {
            switch (selected)
            {
                case 0: ExitScreen();
                        ScreenManager.AddScreen(new Desierto()); break;

                case 2: MenuCancel(); break;
            }
        }

        public override void MenuCancel()
        {
            ScreenManager.AddScreen(new MessageBoxScreen("Are you sure you want to exit?"));
        }
    }
}
