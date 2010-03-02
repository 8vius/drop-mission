﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DropMission.ScreensManager;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using DropMission.Entidades;

namespace DropMission.Screens
{
    public class Desierto : LevelScreen
    {
        public Desierto()
        {
            Jugador = new DropMission.Entidades.Player();
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

            Layers[0] = new LevelLayer(content,"Backgrounds//Desierto//DesiertoLejos",0.5f);
            Layers[1] = new LevelLayer(content,"Backgrounds//Desierto//DesiertoCerca",0.8f);
  //          BackgroundCerca = content.Load<Texture2D>("Backgrounds//bg1");

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
        }


    }

}