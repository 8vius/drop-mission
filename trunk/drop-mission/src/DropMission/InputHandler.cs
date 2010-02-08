using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using DropMission.Entidades;


namespace DropMission
{
    public partial class InputHandler : GameComponent, IInputHandler
    {

        #region IInputHandler Propiedades

        public KeyboardState KeyboardState
        {
            get { return (keyboardState); }
        }

        #endregion

        #region Atributos

        private KeyboardState keyboardState;
        private KeyboardState previousKeyboardState;
        private Player player1;

        #endregion

        #region Constructor

        public InputHandler(Game game, Player player) : base(game)
        {
            //Declara que el juego que entra como parametro va a usar un servicio del tipo IInputHandler
            game.Services.AddService(typeof(IInputHandler), this);

            player1 = player;
        }

        #endregion


        #region Update

        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                Game.Exit();
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                player1.CaminarDerecha();
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                player1.CaminarIzquierda();
            }

            if ((keyboardState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
                || player1.Status.Equals("SALTO"))
            {
                player1.Saltar();
            }

            if ((previousKeyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyUp(Keys.Right)) ||
                (previousKeyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyUp(Keys.Left)))
            {
                player1.Reset();
            }

            previousKeyboardState = KeyboardState;
            base.Update(gameTime);
        }

        #endregion

    }
}
