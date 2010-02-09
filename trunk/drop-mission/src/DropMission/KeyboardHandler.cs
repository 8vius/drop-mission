using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace DropMission
{
    public class KeyboardHandler
    {

        #region Atributos

        private KeyboardState prevKeyboardState;
        private KeyboardState keyboardState;

        #endregion

        #region Constructor

        public KeyboardHandler()
        {
            prevKeyboardState = Keyboard.GetState();
        }

        #endregion

        #region Eventos del teclado

        public bool IsKeyDown(Keys key)
        {
            return (keyboardState.IsKeyDown(key));
        }

        public bool IsHoldingKey(Keys key)
        {
            return (keyboardState.IsKeyDown(key) &&
                prevKeyboardState.IsKeyDown(key));
        }

        public bool WasKeyPressed(Keys key)
        {
            return (keyboardState.IsKeyDown(key) &&
                prevKeyboardState.IsKeyUp(key));
        }

        public bool HasReleasedKey(Keys key)
        {
            return (keyboardState.IsKeyUp(key) &&
            prevKeyboardState.IsKeyDown(key));
        }

        #endregion

        #region Update

        public void Update()
        {
            //set our previous state to our new state
            prevKeyboardState = keyboardState;
            //get our new state
            keyboardState = Keyboard.GetState();
        }

        #endregion
    }
}
