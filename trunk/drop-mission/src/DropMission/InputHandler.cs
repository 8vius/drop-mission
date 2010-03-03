using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using DropMission.Entidades;

namespace DropMission
{
    public partial class InputHandler : GameComponent
    {

        public KeyboardHandler KeyboardState
        {
            get { return (keyboard); }
        }

        #region Atributos

        private KeyboardHandler keyboard;
        private Player player;

        #endregion

        #region Constructor

        public InputHandler(Game game, Player playerHandled) : base(game)
        {
            //Declara que el juego que entra como parametro va a usar un servicio del tipo IInputHandler
            //game.Services.AddService(typeof(IInputHandler), this);

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

            if (keyboard.WasKeyPressed(Keys.Escape))
            {
                Game.Exit();
            }

            //Verifica si el player esta estatico
            if (keyboard.IsHoldingKey(Keys.LeftShift) && player.Status != estadoPlayer.Saltando)
            {
                if (keyboard.IsHoldingKey(Keys.Right))
                {
                    player.Direccion = "Derecha";

                    player.Estatico();

                    player.GirarArma(posicionArma.Derecha);

                    if (keyboard.IsHoldingKey(Keys.Up))
                        player.GirarArma(posicionArma.ArribaDerecha);
                    if (keyboard.IsHoldingKey(Keys.Down))
                        player.GirarArma(posicionArma.AbajoDerecha);
                }

                if (keyboard.IsHoldingKey(Keys.Left))
                {
                    player.Direccion = "Izquierda";

                    player.Estatico();

                    player.GirarArma(posicionArma.Izquierda);

                    if (keyboard.IsHoldingKey(Keys.Up))
                        player.GirarArma(posicionArma.ArribaIzquierda);
                    if (keyboard.IsHoldingKey(Keys.Down))
                        player.GirarArma(posicionArma.AbajoIzquierda);
                }

                if(keyboard.IsHoldingKey(Keys.Up))
                    player.GirarArma(posicionArma.Arriba);

                if(keyboard.IsHoldingKey(Keys.Down))
                    player.GirarArma(posicionArma.Abajo);
            }

            if (keyboard.IsKeyDown(Keys.Up))
            {
                player.GirarArma(posicionArma.Arriba);
            }

            if (keyboard.IsKeyDown(Keys.Down))
            {
                player.GirarArma(posicionArma.Abajo);
            }

            if (keyboard.IsHoldingKey(Keys.Right))
            {
                player.Direccion = "Derecha";

                if (!keyboard.IsHoldingKey(Keys.LeftShift))
                    player.CaminarDerecha();

                player.GirarArma(posicionArma.Derecha);
                
                if(keyboard.IsHoldingKey(Keys.Up))
                    player.GirarArma(posicionArma.ArribaDerecha);
                if(keyboard.IsHoldingKey(Keys.Down))
                    player.GirarArma(posicionArma.AbajoDerecha);
            }

            if (keyboard.IsKeyDown(Keys.Left))
            {
                player.Direccion = "Izquierda";

                if (!keyboard.IsHoldingKey(Keys.LeftShift))
                    player.CaminarIzquierda();

                player.GirarArma(posicionArma.Izquierda);            

                if (keyboard.IsHoldingKey(Keys.Up))
                    player.GirarArma(posicionArma.ArribaIzquierda);
                if (keyboard.IsHoldingKey(Keys.Down))
                    player.GirarArma(posicionArma.AbajoIzquierda);
            }

            if ((keyboard.IsHoldingKey(Keys.Down) &&
                keyboard.HasReleasedKey(Keys.Space)) ||
                player.Status == estadoPlayer.Cayendo)
            {
                player.Caer();
            }

            if ((keyboard.WasKeyPressed(Keys.Space) && !keyboard.IsHoldingKey(Keys.LeftShift)
                || player.Status == estadoPlayer.Saltando)
                && player.Status != estadoPlayer.Cayendo)
            {
                player.Saltar();
            }

            if (keyboard.IsKeyDown(Keys.F))
            {
                player.Disparar(gameTime);
            }

            if (keyboard.HasReleasedKey(Keys.Right) ||
                keyboard.HasReleasedKey(Keys.Left) ||
                keyboard.HasReleasedKey(Keys.Up) ||
                keyboard.HasReleasedKey(Keys.Down))
            {
                player.Reset();
            }

            base.Update(gameTime);
        }

        #endregion

    }
}
