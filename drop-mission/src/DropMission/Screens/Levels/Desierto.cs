using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DropMission.ScreensManager;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using DropMission.Entidades;
using Microsoft.Xna.Framework;

namespace DropMission.Screens
{
    public class Desierto : LevelScreen
    {
        public Desierto()
        {
            Jugador = new DropMission.Entidades.Player();
            Plataformas = new List<Plataforma>();
            Enemigos = new List<Enemy>();
            //Prueba de las plataformas
            Plataforma p = new Plataforma(tipoPlataforma.Platform);
            p.Posicion = new Vector2(400, 400);
            Plataformas.Add(p);

            //Prueba Kamikazes
            Kamikaze KamiPrueba1 = new Kamikaze(posicionKamikaze.Derecha);
            Kamikaze KamiPrueba2 = new Kamikaze(posicionKamikaze.Izquierda);
            Enemigos.Add(KamiPrueba1);
            Enemigos.Add(KamiPrueba2);

            //Prueba Camper
            Camper CamperPrueba = new Camper(400, 325);
            Enemigos.Add(CamperPrueba);
        }

        public override void Initialize()
        {
            InputHandler input = new InputHandler(ScreenManager.Game, Jugador);
            ScreenManager.Game.Components.Add(input);
            base.Initialize();
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;
        
            //Cargo el pixel del fade
            Pixel = content.Load<Texture2D>("singlePixel");

            //Cargo los fondos
            Layers = new LevelLayer[2];

            Layers[0] = new LevelLayer(content,"Backgrounds//Desierto//DesiertoLejos",0.6f);
            Layers[1] = new LevelLayer(content,"Backgrounds//Desierto//DesiertoCerca",1f);

            //Cargo los Spritesheets del personaje
            Jugador.SpriteCaminar = content.Load<Texture2D>("Sprites//Player//walk");
            Jugador.SpriteSaltar = content.Load<Texture2D>("Sprites//Player//jump");

            //Cargo los sprites del arma
            Jugador.arma.SpriteArma = content.Load<Texture2D>("Sprites//Weapon//M16");
            for (int i = 0; i < 30; i++)
            {
                Bala bala = new Bala();
                bala.SpriteBala = content.Load<Texture2D>("Sprites//Weapon//bala");
                Jugador.arma.Balas.Add(bala);
            }

            //Cargo el sprite para todas las texturas
            foreach (Plataforma plat in Plataformas)
            {
                plat.texture = content.Load<Texture2D>("Sprites//Plataforma//plataformaCentro");
            }

            foreach (Enemy enemigo in Enemigos)
            {
                enemigo.LoadContent(content);
            }
        }


    }

}
