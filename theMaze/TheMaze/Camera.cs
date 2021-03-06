﻿using Microsoft.Xna.Framework;
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
        private Vector2 Zoom = new Vector2(1f,1.5f);

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
            transform = Matrix.CreateTranslation(-position.X + view.Width / 2, -position.Y + view.Height / 2, 0f);
            //transform = Matrix.CreateTranslation(-position.X + view.Width/2, -position.Y + view.Height/2, 0f) * Matrix.CreateScale(Zoom.X,Zoom.Y,1f);
        }
    }
}
