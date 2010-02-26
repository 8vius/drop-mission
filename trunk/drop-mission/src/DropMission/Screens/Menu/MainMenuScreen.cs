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
            MenuEntries.Add("Comenzar");
            MenuEntries.Add("Opciones");
            MenuEntries.Add("Salir");

            Selected = Color.Yellow;
            NoneSelected = Color.White;

            StartPosition = new Vector2(535, 440);
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;
            SpriteFont = content.Load<SpriteFont>("Menu//MenuFont");
            Background = content.Load<Texture2D>("Menu//Fondo");
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
