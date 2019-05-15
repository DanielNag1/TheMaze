using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public class Camera
    {
        private Matrix transform; //håller en transformation från position i "spelvärden" till position i fönstret;
        private Vector2 position; //spelarens position
        private Viewport view; //kamera vyn, (det som hela spelrutan visar?)
        private Vector2 Zoom = new Vector2(1f, 1.5f);
        private Matrix transformhallway;

        public Matrix TransformHallway
        {
            get { return transformhallway; }
        }
        public Matrix Transform
        {
            get { return transform; }
        }

        public Camera(Viewport view)
        {
            this.view = view;
        }

        public void SetPosition(Vector2 position) // "var på fönstret objektet ska visas" så att kameran följer objektet
        {
            this.position = position;

            if (GamePlayManager.black)
            {
                transform = Matrix.CreateTranslation(-position.X + view.Width / 2, -position.Y + view.Height / 2, 0f);
            }
            if (!GamePlayManager.black)
            {
                if(position.X>=0 && position.X<788)
                {
                    transformhallway = Matrix.CreateTranslation(-position.X + view.Width / 2 - 300, -position.Y + view.Height/2, 0f);
                }
                if(position.X>=788)
                {
                    transformhallway = Matrix.CreateTranslation(-128, -position.Y + view.Height/2, 0f);
                }
            }
            
            //transform = Matrix.CreateTranslation(-position.X + view.Width/2, -position.Y + view.Height/2, 0f) * Matrix.CreateScale(Zoom.X,Zoom.Y,1f);
        }
    }

}
