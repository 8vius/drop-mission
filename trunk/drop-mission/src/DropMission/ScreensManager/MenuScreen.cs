using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DropMission.ScreensManager
{
    public abstract class MenuScreen : GameScreen
    {

        #region Fields and Properties

        //Lista de imagenes de las opciones
        public List<Texture2D> MenuEntries
        {
            get { return menuEntries; }
        }
        List<Texture2D> menuEntries = new List<Texture2D>();

        //Imagen del fondo
        public Texture2D Background
        {
            get { return background; }
            set { background = value; }
        }
        Texture2D background;

        //Imagen del titulo
        public Texture2D Title
        {
            get { return title; }
            set { title = value; }
        }
        Texture2D title;

        //Imagen del jugador
        public Texture2D Player
        {
            get { return player; }
            set { player = value; }
        }
        Texture2D player;

        //Imagen de la estrella
        public Texture2D SelectedOption
        {
            get { return selectedOption; }
            set { selectedOption = value; }
        }
        Texture2D selectedOption;

        //Posicion inicial del las opciones
        public Vector2 StartPositionMenu
        {
            get { return startPositionMenu; }
            set { startPositionMenu = value; }
        }
        Vector2 startPositionMenu;

        //Posicion de las opciones
        public Vector2 PositionMenu
        {
            get { return positionMenu; }
            set { positionMenu = value; }
        }
        Vector2 positionMenu;

        //Posicion inicial del titulo
        public Vector2 StartPositionTitle
        {
            get { return startPositionTitle; }
            set { startPositionTitle = value; }
        }
        Vector2 startPositionTitle;

        //Posicion inicial del jugador
        public Vector2 StartPositionPlayer
        {
            get { return startPositionPlayer; }
            set { startPositionPlayer = value; }
        }
        Vector2 startPositionPlayer;

        //Variable para verificar que opcion eligio el usuario
        int selectedEntry = 0;

        #endregion

        #region Menu Operations

        //Aqui va la declaracion de los metodos de las operaciones del menu

        public abstract void MenuSelect(int selected);

        public abstract void MenuCancel();

        #endregion

        #region Constructor

        public MenuScreen()
        {
            TransitionOnTime = TransitionOffTime = TimeSpan.FromSeconds(1.5);
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

            if (selectedOption != null)
                selectedOption = null;
        }

        #endregion

        #region HandleInput

        public override void HandleInput()
        {
            KeyboardHandler input = ScreenManager.Input;

            if(input.WasKeyPressed(Keys.Up))
            {
                selectedEntry--;
                if (selectedEntry < 0)
                    selectedEntry = menuEntries.Count - 1;
            }

            if (input.WasKeyPressed(Keys.Down))
            {
                selectedEntry++;
                if (selectedEntry >= menuEntries.Count)
                    selectedEntry = 0;
            }

            if (input.WasKeyPressed(Keys.Escape))
                MenuCancel();

            if (input.WasKeyPressed(Keys.Enter))
                MenuSelect(selectedEntry);

        }

        #endregion

        #region Update

        public override void Update(GameTime gameTime, bool covered)
        {
            base.Update(gameTime, covered);
        }

        #endregion

        #region Drwan

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

            positionMenu = startPositionMenu;

            for (int i = 0; i < menuEntries.Count; i++)
            {
                bool isSelected = (i == selectedEntry);
                DrawSelected(spriteBatch, menuEntries[i], isSelected);
                positionMenu.Y += 50f;
            }

            spriteBatch.End();
        }

        #region DrawSelected

        private void DrawSelected(SpriteBatch spriteBatch, Texture2D menuEntry, bool isSelected)
        {
            spriteBatch.Draw(menuEntry, new Rectangle((int)startPositionMenu.X, (int)positionMenu.Y,
                menuEntry.Width, menuEntry.Height), Color.White);

            if (isSelected)
                spriteBatch.Draw(selectedOption, new Rectangle((int)startPositionMenu.X - selectedOption.Width,
                    (int)positionMenu.Y - 5, selectedOption.Width, selectedOption.Height), Color.White);
        }

        #endregion

        #endregion
    }
}
