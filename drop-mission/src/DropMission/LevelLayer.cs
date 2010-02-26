using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

// Esta clase se utiliza para almacenar cada capa completa de un nivel
namespace DropMission
{
    public class LevelLayer
    {
        #region Fields

        //Aca se almacenan los segmentos que pertenecen a cada capa
        Texture2D[] segments;
        public Texture2D[] Segments
        {
            get { return segments; }
            set { segments = value; }
        }

        //Esto representa el ritmo al que se va a mover cada capa
        float scrollRate;
        public float ScrollRate
        {
            get { return scrollRate; }
            set { scrollRate = value; }
        }

        #endregion

        #region Constructor
        public LevelLayer(ContentManager content,string basePath, float scrollrate)
        {
            //Se cargan losa segmentos, por ahora asumo que solo hay 1
            segments = new Texture2D[1];
            for (int i = 0; i < 1; ++i)
                segments[i] = content.Load<Texture2D>(basePath + "_" + i);

            scrollRate = scrollrate;
        }
        #endregion

        #region Methods

        public void Draw(SpriteBatch spriteBatch, float cameraPosition)
        {
            // Se asume que todos los segmentos miden lo mismo
            int segmentWidth = segments[0].Width;

            // Aca calculo cuales segmentos se van a dibujar y cuando mostrar de ellos
            float x = cameraPosition * scrollRate;
            int leftSegment = (int)Math.Floor(x / segmentWidth);
            int rightSegment = leftSegment + 1;
            x = (x / segmentWidth - leftSegment) * -segmentWidth;

            spriteBatch.Draw(segments[leftSegment % segments.Length], new Vector2(x, 0.0f), Color.White);
            spriteBatch.Draw(segments[rightSegment % segments.Length], new Vector2(x + segmentWidth, 0.0f), Color.White);
        }
        
        #endregion

    }
}
