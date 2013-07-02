using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeirRL.Graphics;
using HeirRL.Source;
using Microsoft.Xna.Framework;

namespace HeirRL.Level
{
    class GridElement : VisualComponent
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public GridElement(int row, int column)
        {
            Texture = ImagesManager.Get("tiles-default");
            TextureKey = "full";
            Origin = Origin.Center;
            NearbyElements = new GridElement[8];

            Row = row;
            Column = column;

            X = row * 135;
            bool even = ParityCheck(column);
            if (even) X -= 67.5f;
            
            Y = column*30;

            Layer = 0.95f - column * 0.0001f;

            MouseIn += (sender, args) => { MouseManager.ObjectUnderPointer = this; };
            MouseOut += (sender, args) =>
                {
                    if (MouseManager.ObjectUnderPointer == this)
                        MouseManager.ObjectUnderPointer = null;
                };
        }

        private static bool ParityCheck(int val)
        {
            return (val % 2 == 0);
        }

        public GridElement[] NearbyElements { get; set; }
    }
}
