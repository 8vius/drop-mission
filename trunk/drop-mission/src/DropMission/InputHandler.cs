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

        #region Initialize

        public override void Initialize()
        {
            base.Initialize();
        }

        #endregion

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
                player.GirarArma(0);
            }

            if (keyboard.IsKeyDown(Keys.Left))
            {
                player.CaminarIzquierda();
                player.GirarArma(4);
            }

            if ((keyboard.WasKeyPressed(Keys.Space))
                || player.Status == estadoPlayer.Saltando)
            {
                player.Saltar();
            }

            if (keyboard.IsKeyDown(Keys.F))
            {
                player.Disparar();
            }

            if ((keyboard.HasReleasedKey(Keys.Right) ||
                (keyboard.HasReleasedKey(Keys.Left))))
            {
                player.Reset();
            }

            if ((keyboard.IsKeyDown(Keys.Up) && (keyboard.IsKeyDown(Keys.Right))))
            {
                player.GirarArma(1);
            }

            if ((keyboard.IsKeyDown(Keys.Up)) && (!keyboard.IsKeyDown(Keys.Right)))
            {
                player.GirarArma(2);
            } 

            if ((keyboard.IsKeyDown(Keys.Up) && (keyboard.IsKeyDown(Keys.Left))))
            {
                player.GirarArma(3);
            }

            if ((keyboard.IsKeyDown(Keys.Down) && (keyboard.IsKeyDown(Keys.Left))))
            {
                player.GirarArma(5);
            }

            if (keyboard.IsKeyDown(Keys.Down))
            {
                player.GirarArma(6);
            }

            if ((keyboard.IsKeyDown(Keys.Down) && (keyboard.IsKeyDown(Keys.Right))))
            {
                player.GirarArma(7);
            }


            

            base.Update(gameTime);
        }

        #endregion

    }
}
