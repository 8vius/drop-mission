using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace DropMission
{
    public class Player2 : SpriteBase
    {

        public Player2(Game game)
            : base(game)
        {
            
        }

        public void CalcularTimer(GameTime gameTime)
        {
            base.timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
