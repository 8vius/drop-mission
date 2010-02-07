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
    public class Player
    {
        const int spriteWidth = 150;
        const int spriteHeight = 100;

        float timer = 0f;
        float interval = 1000f / 15f;
        int frameCount = 4;
        int currentFrame = 0;
        
        public Rectangle sourceRect;
        public Rectangle destinationRect;

        public Texture2D spriteSheetWalk;

        public Player()
        {
            destinationRect = new Rectangle(0, 0, spriteWidth, spriteHeight);
        }

        public void CaminarDerecha(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer > interval)
            {
                currentFrame++;
                if (currentFrame > frameCount - 1)
                {
                    currentFrame = 0;
                }
                timer = 0f;
            }

            sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
        }

        public void CaminarIzquierda(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer > interval)
            {
                currentFrame++;
                if (currentFrame > frameCount - 1)
                {
                    currentFrame = 0;
                }
                timer = 0f;
            }

            sourceRect = new Rectangle(currentFrame * spriteWidth, 100, spriteWidth, spriteHeight);
        }
    }
}
