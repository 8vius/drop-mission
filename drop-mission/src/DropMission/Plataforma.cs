using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropMission
{
    /// <summary>
    /// Controls the collision detection and response behavior of a tile.
    /// </summary>
    public enum tipoPlataforma
    {
        /// <summary>
        /// A passable tile is one which does not hinder player motion at all.
        /// </summary>
        Passable = 0,

        /// <summary>
        /// An impassable tile is one which does not allow the player to move through
        /// it at all. It is completely solid.
        /// </summary>
        Impassable = 1,

        /// <summary>
        /// A platform tile is one which behaves like a passable tile except when the
        /// player is above it. A player can jump up through a platform as well as move
        /// past it to the left and right, but can not fall down through the top of it.
        /// </summary>
        Platform = 2,
    }

    /// <summary>
    /// Stores the appearance and collision behavior of a tile.
    /// </summary>
    public class Plataforma
    {
        #region Fields and properties

        public Texture2D texture;
        public tipoPlataforma Tipo;

        public Vector2 Posicion { get; set; }

        public int Height = 48;
        private int width = 100;
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        #endregion

        public Plataforma(tipoPlataforma tipo)
        {
            Posicion = new Vector2();
            Tipo = tipo;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)Posicion.X, (int)Posicion.Y, width, Height), Color.White);
        }

    }
}