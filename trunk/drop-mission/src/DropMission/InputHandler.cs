﻿using System;
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

        public KeyboardHandler KeyboardState
        {
            get { return (keyboard); }
        }

        #endregion

        #region Atributos

        private KeyboardHandler keyboard;
        private Player player;

        #endregion

        #region Constructor

        public InputHandler(Game game, Player playerHandled) : base(game)
        {
            //Declara que el juego que entra como parametro va a usar un servicio del tipo IInputHandler
            game.Services.AddService(typeof(IInputHandler), this);

            keyboard = new KeyboardHandler();
            player = playerHandled;

        }

        #endregion

        public override void Initialize()
        {
            base.Initialize();
        }

        #region Update

        public override void Update(GameTime gameTime)
        {
            keyboard.Update();

            if (keyboard.IsKeyDown(Keys.Escape))
            {
                Game.Exit();
            }

            if (keyboard.IsKeyDown(Keys.Right))
            {
                player.CaminarDerecha();
            }

            if (keyboard.IsKeyDown(Keys.Left))
            {
                player.CaminarIzquierda();
            }

            if ((keyboard.WasKeyPressed(Keys.Space))
                || player.Status.Equals("SALTO"))
            {
                player.Saltar();
            }

            if ((keyboard.HasReleasedKey(Keys.Right) ||
                (keyboard.HasReleasedKey(Keys.Left))))
            {
                player.Reset();
            }

            base.Update(gameTime);
        }

        #endregion

    }
}
