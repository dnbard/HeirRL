using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HeirRL.Graphics
{
    class StaticCamera: GameComponent
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2 Position
        {
            get { return new Vector2(X, Y); }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public StaticCamera() : base(Program.Game)
        {
            
        }
    }
}
