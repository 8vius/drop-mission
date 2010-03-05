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
            Background = content.Load<Texture2D>("Menu//Fondo");
            Player = content.Load<Texture2D>("Menu//Player");
            SelectedOption = content.Load<Texture2D>("Menu//Star");
            Title = content.Load<Texture2D>("Menu//title");
            MenuEntries.Add(content.Load<Texture2D>("Menu//startGame"));
            MenuEntries.Add(content.Load<Texture2D>("Menu//options"));
            MenuEntries.Add(content.Load<Texture2D>("Menu//quit"));
        }

        public override void Remove()
        {
            base.Remove();
            MenuEntries.Clear();
        }

        public override void MenuSelect(int selected)
        {
            ExitScreen();
            switch (selected)
            {
                case 0 : ScreenManager.AddScreen(new Desierto()); break;
            }
        }

        public override void MenuCancel()
        {
            ExitScreen();
        }
    }
}
