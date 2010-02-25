using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using DropMission.ScreensManager;

namespace DropMission.Screens
{
    public class MainMenuScreen : MenuScreen
    {

        public MainMenuScreen()
        {
            MenuEntries.Add("Comenzar juego");
            MenuEntries.Add("Salir");

            Selected = Color.YellowGreen;
            NoneSelected = Color.Yellow;

            StartPosition = new Vector2(275, 250);
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;
            SpriteFont = content.Load<SpriteFont>("Font");
            Background = content.Load<Texture2D>("PortadaDM");
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
                case 0 : ScreenManager.AddScreen(new LogoScreen()); break;
            }
        }

        public override void MenuCancel()
        {
            ExitScreen();
        }
    }
}
