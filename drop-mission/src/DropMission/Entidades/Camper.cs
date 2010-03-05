using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DropMission.Entidades;
using Microsoft.Xna.Framework.Content;


namespace DropMission
{
    public enum miradaCamper
    {
        Izquierda,
        Derecha
    };

    public class Camper : Enemy
    {

        miradaCamper mirada;
        int elapsedTime;
        bool delay;

        public miradaCamper Mirada
        {
            get { return mirada; }
            set { mirada = value; }
        }

        public Camper(int X, int Y)
        {
            mirada = miradaCamper.Izquierda;
            Alive = true;
            DestinationRect = new Rectangle(X, Y, SpriteWidth, SpriteHeight);
            SourceRect = new Rectangle(0, 100, SpriteWidth, SpriteHeight);
            Arma = new Weapon(DestinationRect);
            Arma.RectanguloFuente = new Rectangle(0,100,arma.SpriteWidth,arma.SpriteHeight);

            for (int i = 0; i < 3;i++ )
                Arma.Balas.Add(new Bala());
           
        }

        #region Metodos

        
        public void Girar()
        { }
        
        public void Disparar(GameTime gameTime, Rectangle playerRect)
        {
            //Con esto lo que logro es hacer un delay a cada disparo para
            //que no salgan varias balas al mismo tiempo
            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsedTime >= 100)
            {
                delay = true;
                elapsedTime = 0;
            }


            foreach (Bala bala in arma.Balas)
            {
                if (!bala.Vivo && delay)
                {
                    bala.Vivo = true;
                    bala.Posicion = new Vector2(Arma.PosicionX, Arma.PosicionY);
                    //Cambio la posicion inicial de la bala para que dispare de la punta del arma
               

                    //coloco la velocidad de la bala dependiendo de la rotacion del arma
                    bala.Velocidad = new Vector2((float)Math.Cos(PosicionX - playerRect.X), (float)Math.Sin(PosicionY - playerRect.Y)) * 13.0f;
                    delay = false;

                    return;
                }
            }
        }
        public override void Update(GameTime gameTime, Rectangle playerRect)
        {
            Disparar(gameTime,playerRect);
            foreach (Bala bala in Arma.Balas)
            {
                if (bala.Vivo)
                    bala.Mover(DestinationRect);
            }
        }

        public override void LoadContent(ContentManager content)
        {
            SpriteSheetAlive = content.Load<Texture2D>("Sprites//Enemy//TerroristCamper");
            Arma.SpriteArma = content.Load<Texture2D>("Sprites//Weapon//AK");
            foreach (Bala bala in Arma.Balas)
                bala.SpriteBala = content.Load<Texture2D>("Sprites//Weapon//bala");
        }

       
        #endregion

    }
}
