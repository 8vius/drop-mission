using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace DropMission
{
    public partial class InputHandler : GameComponent, IInputHandler
    {

        private KeyboardState keyboardState;

        public InputHandler(Game game) : base(game)
        {
            //Declara que esta clase va a usar un servicio del tipo IInputHandler
            game.Services.AddService(typeof(IInputHandler), this);
        }

        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
                Game.Exit();
            base.Update(gameTime);
        }
    }
}
