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

        /*public List<string> MenuEntries
        {
            get { return menuEntries; }
        }
        List<string> menuEntries = new List<string>();

        
        public SpriteFont SpriteFont
        {
            get { return spriteFont; }
            set { spriteFont = value; }
        }
        SpriteFont spriteFont;

        public Color Selected
        {
            get { return selected;}
            set { selected = value;}
        }
        Color selected;

        public Color NoneSelected
        {
            get { return noneSelected; }
            set { noneSelected = value; }
        }
        Color noneSelected;
         
        public Vector2 StartPosition
        {
            get { return startPosition; }
            set { startPosition = value; }
        }
        Vector2 startPosition;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        Vector2 position;*/

        public List<Texture2D> MenuEntries
        {
            get { return menuEntries; }
        }
        List<Texture2D> menuEntries = new List<Texture2D>();

        public Texture2D Background
        {
            get { return background; }
            set { background = value; }
        }
        Texture2D background;

        public Texture2D Title
        {
            get { return title; }
            set { title = value; }
        }
        Texture2D title;

        public Texture2D Player
        {
            get { return player; }
            set { player = value; }
        }
        Texture2D player;

        public Texture2D SelectedOption
        {
            get { return selectedOption; }
            set { selectedOption = value; }
        }
        Texture2D selectedOption;

        int selectedEntry = 0;

        #endregion

        #region Menu Ops

        public abstract void MenuSelect(int selected);

        public abstract void MenuCancel();

        #endregion

        public MenuScreen()
        {
            TransitionOnTime = TransitionOffTime = TimeSpan.FromSeconds(1.5);
        }

        public override void UnloadContent()
        {
            //if (spriteFont != null)
              //  spriteFont = null;
        }

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
                ScreenManager.Game.Exit();

            if (input.WasKeyPressed(Keys.Enter))
                MenuSelect(selectedEntry);

        }

        public override void Update(GameTime gameTime, bool covered)
        {
            base.Update(gameTime, covered);

            /*position = new Vector2(startPosition.X, startPosition.Y);

            if (ScreenState == ScreenState.TransitionOn || ScreenState == ScreenState.TransitionOff)
            {
                Vector2 acceleration = new Vector2((float)Math.Pow(TransitionPercent - 1, 2), 0);
                acceleration.X *= TransitionDirection * -150;

                position += acceleration;
            }*/
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Viewport viewport = ScreenManager.Game.GraphicsDevice.Viewport;

            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, viewport.Width, viewport.Height), Color.White);

            spriteBatch.Draw(title, new Rectangle((viewport.Width / 2) - title.Width / 2, (viewport.Height / 2) - 250 , 366, 147), Color.White);

            spriteBatch.Draw(player, new Rectangle(155, 425, 100, 80), Color.White);

            for (int i = 0; i < menuEntries.Count; i++)
            {
                bool isSelected = (i == selectedEntry);
                DrawSelected(spriteBatch, viewport, menuEntries[i], isSelected);
                viewport.Height += 100;
            }

            spriteBatch.End();

            


            /*Vector2 menuPosition = new Vector2(position.X, position.Y);

            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, viewport.Width, viewport.Height), Color.White);

            for (int i = 0; i < menuEntries.Count; i++)
            {
                bool isSelected = (i == selectedEntry);
                DrawEntry(spriteBatch, gameTime, menuEntries[i], menuPosition, isSelected);
                menuPosition.Y += spriteFont.LineSpacing;
            }

            spriteBatch.End();*/
        }

        /*private void DrawEntry(SpriteBatch spriteBatch, GameTime gameTime, string entry, Vector2 position,
            bool isSelected)
        {
            Vector2 origin = new Vector2(0, spriteFont.LineSpacing / 2);
            Color color = isSelected ? selected : noneSelected;
            color = new Color(color, ScreenAlpha);

            float pulse = (float)(Math.Sin(gameTime.TotalGameTime.TotalSeconds * 3) + 1);
            float scale = isSelected ? (1 + pulse * 0.05f) : 1f;

            spriteBatch.DrawString(spriteFont, entry, position, color, 0, origin, scale, SpriteEffects.None,
                0);
        }*/

        private void DrawSelected(SpriteBatch spriteBatch, Viewport viewport, Texture2D menuEntry, bool isSelected)
        {
            if (isSelected)
            {
                spriteBatch.Draw(selectedOption, new Rectangle((viewport.Width / 2) - (menuEntry.Width / 2) - selectedOption.Width,
                    (viewport.Height / 2), selectedOption.Width, selectedOption.Height), Color.White);

                spriteBatch.Draw(menuEntry, new Rectangle((viewport.Width / 2) - menuEntry.Width / 2, (viewport.Height / 2),
                    menuEntry.Width, menuEntry.Height), Color.White);

                spriteBatch.Draw(selectedOption, new Rectangle((viewport.Width / 2) + (menuEntry.Width / 2),
                    (viewport.Height / 2), selectedOption.Width, selectedOption.Height), Color.White);
            }
            else
            {
                spriteBatch.Draw(menuEntry, new Rectangle((viewport.Width / 2) - menuEntry.Width / 2, (viewport.Height / 2),
                    menuEntry.Width, menuEntry.Height), Color.White);
            }
        }
    }
}
