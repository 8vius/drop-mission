using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace DropMission
{
    public enum estadoKamikaze
    {
        Caminando,
        Explotando
    };

    public enum posicionKamikaze
    {
        Izquierda,
        Derecha
    };

    public class Kamikaze : Enemy
    {
       

        private int posicionXanterior;

        public estadoKamikaze Status;
        public posicionKamikaze PosicionKamikaze;

        public Kamikaze(posicionKamikaze Posicion)
        {
            Status = estadoKamikaze.Caminando;
            Alive = true;
            Arma = null;
            PosicionKamikaze = Posicion;

            if (Posicion == posicionKamikaze.Derecha)
            {
                DestinationRect = new Rectangle(900, 480, SpriteWidth, SpriteHeight);
                posicionXanterior = 900;
            }
            if (Posicion == posicionKamikaze.Izquierda)
            {
                DestinationRect = new Rectangle(-100, 480, SpriteWidth, SpriteHeight);
                posicionXanterior = -100;
            }
                       
        }

        #region Metodos

        public void CaminarDerecha()
        {
            if (Timer > Interval)
            {
                CurrentFrame++;
                if (CurrentFrame > FrameCount - 1)
                {
                    CurrentFrame = 0;
                }
                Timer = 0f;
            }

            SourceRect = new Rectangle(CurrentFrame * SpriteWidth, 0, SpriteWidth, SpriteHeight);

            posicionXanterior = PosicionX;
            PosicionX += 4;
        }

        public void CaminarIzquierda()
        {
            if (Timer > Interval)
            {
                CurrentFrame++;
                if (CurrentFrame > FrameCount - 1)
                {
                    CurrentFrame = 0;
                }
                Timer = 0f;
            }

            SourceRect = new Rectangle(CurrentFrame * SpriteWidth, 100, SpriteWidth, SpriteHeight);

            posicionXanterior = PosicionX;
            PosicionX -= 4;
        }

        public override void Update(GameTime gameTime, Rectangle playerRect)
        {
            if (PosicionKamikaze == posicionKamikaze.Derecha)
                CaminarIzquierda();
            if (PosicionKamikaze == posicionKamikaze.Izquierda)
                CaminarDerecha();
        }

        public override void LoadContent(ContentManager content)
        {
            SpriteSheetAlive = content.Load<Texture2D>("Sprites//Enemy//KamikazeWalk");
        }
        #endregion
    }
}
