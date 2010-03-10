using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DropMission.Screens.Menu;

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
            if (selectedOption != null)
                selectedOption = null;
        }

        #endregion

        #region HandleInput

        public override void HandleInput(InputHandler input, GameTime gameTime)
        {
            if(input.IsMenuEntryUp())
            {
                selectedEntry--;

                if (selectedEntry < 0)
                    selectedEntry = menuEntries.Count - 1;
            }

            if (input.IsMenuEntryDown())
            {
                selectedEntry++;

                if (selectedEntry >= menuEntries.Count)
                    selectedEntry = 0;
            }

            if (input.IsMenuCancel())
                MenuCancel();

            if (input.IsMenuEntrySelected())
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

            spriteBatch.Begin();

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
