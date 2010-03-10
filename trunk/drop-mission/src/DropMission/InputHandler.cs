using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using DropMission.Entidades;
using DropMission.Screens.Menu;
using DropMission.ScreensManager;

namespace DropMission
{
    public class InputHandler
    {
        #region Fields and Properties

        public KeyboardHandler KeyboardState
        {
            get { return (keyboard); }
        }
        private KeyboardHandler keyboard;

        #endregion

        #region Constructor

        public InputHandler(Game game, Player playerHandled)
        {
            keyboard = new KeyboardHandler();
        }

        public InputHandler()
        {
            keyboard = new KeyboardHandler();
        }

        #endregion

        #region Update

        public void Update()
        {
            keyboard.Update();
        }

        #endregion

        //Metodos para manejar el input del MainMenu, PauseMenu...
        #region Menu Input

        //Verifica si se presiono la flecha Up
        public bool IsMenuEntryUp()
        {
            return keyboard.WasKeyPressed(Keys.Up);
        }

        //Verifica si se presiono la flecha Down
        public bool IsMenuEntryDown()
        {
            return keyboard.WasKeyPressed(Keys.Down);
        }

        //Verifica si se presiono Escape
        public bool IsMenuCancel()
        {
            return keyboard.WasKeyPressed(Keys.Escape);
        }

        //Verifica si se presiono Enter
        public bool IsMenuEntrySelected()
        {
            return keyboard.WasKeyPressed(Keys.Enter);
        }

        //Verifica si se presiono Escape o P
        public bool IsPausePressed()
        {
            return keyboard.WasKeyPressed(Keys.Escape) || keyboard.WasKeyPressed(Keys.P);
        }

        #endregion

        //Metodos para manejar el input del jugador
        #region Player Input

        //Verifica si se presiono Shift izquierdo y la flecha Darecha
        public bool IsPlayerStaticRight(Player jugador)
        {
            if (keyboard.IsHoldingKey(Keys.LeftShift) && jugador.Status != estadoPlayer.Saltando &&
                jugador.Status != estadoPlayer.Cayendo)
            {
                if (keyboard.IsHoldingKey(Keys.Right))
                    return true;
            }
            return false;
        }

        //Verifica si se presiono Shift izquierdo y la flecha Izquierda
        public bool IsPlayerStaticLeft(Player jugador)
        {
            if (keyboard.IsHoldingKey(Keys.LeftShift) && jugador.Status != estadoPlayer.Saltando &&
                jugador.Status != estadoPlayer.Cayendo)
            {
                if (keyboard.IsHoldingKey(Keys.Left))
                    return true;
            }
            return false;
        }

        //Verifica si se presiono la flecha Derecha
        public bool IsPlayerWalkingRight()
        {
            return keyboard.IsKeyDown(Keys.Right);
        }

        //Verifica si se presiono la flecha Izquierda
        public bool IsPlayerWalkingLeft()
        {
            return keyboard.IsKeyDown(Keys.Left);
        }

        //Verifica si se presiono F
        public bool IsPlayerShooting()
        {
            return keyboard.IsKeyDown(Keys.F);
        }

        //Verifica si se presiono Space
        public bool IsPlayerJumping(Player jugador)
        {
            return (keyboard.WasKeyPressed(Keys.Space) ||
                jugador.Status == estadoPlayer.Saltando);
        }

        //Verifica si se presiono flecha Abajo despues de haber presionado Space
        //o que el jugador ya se encuentre en el estado Cayendo
        public bool IsPlayerFalling(Player jugador)
        {
            return (keyboard.IsHoldingKey(Keys.Down) &&
                keyboard.HasReleasedKey(Keys.Space)) ||
                jugador.Status == estadoPlayer.Cayendo;
        }

        //Verifica si se esta presionando flecha Arriba
        public bool IsPlayerLookingUp()
        {
            return keyboard.IsKeyDown(Keys.Up);
        }

        //Verifica si se esta presionando flecha Abajo
        public bool IsPlayerLookingDown()
        {
            return keyboard.IsKeyDown(Keys.Down);
        }

        //Verifica que se haya soltado cualquiera de las flechas de direccion
        public bool IsPlayerReset()
        {
            return  keyboard.HasReleasedKey(Keys.Right) ||
                    keyboard.HasReleasedKey(Keys.Left) ||
                    keyboard.HasReleasedKey(Keys.Up) ||
                    keyboard.HasReleasedKey(Keys.Down);
        }

        #endregion

        public bool IsHoldingKey(Keys key)
        {
            return keyboard.IsHoldingKey(key);
        }
    }
}
