using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using HeirRL.Graphics;
using HeirRL.Source;
using HeirRL.Characters;

namespace HeirRL.Level
{
    public class GridElement : VisualComponent
    {
        public int Row { get; set; }
        public int Column { get; set; }

        protected bool _passable = true;
        public bool Passable
        {
            get { return _passable && Creature == null; }
        }

        public Character Creature { get; set; }

        public GridElement[] NearbyElements { get; set; }

        public GridElement North { get { return NearbyElements[(int)MapGridLinkage.North]; } }
        public GridElement NorthEast { get { return NearbyElements[(int)MapGridLinkage.NorthEast]; } }
        public GridElement East { get { return NearbyElements[(int)MapGridLinkage.East]; } }
        public GridElement SouthEast { get { return NearbyElements[(int)MapGridLinkage.SouthEast]; } }
        public GridElement South { get { return NearbyElements[(int)MapGridLinkage.South]; } }
        public GridElement SouthWest { get { return NearbyElements[(int)MapGridLinkage.SouthWest]; } }
        public GridElement West { get { return NearbyElements[(int)MapGridLinkage.West]; } }
        public GridElement NorthWest { get { return NearbyElements[(int)MapGridLinkage.NorthWest]; } }

        public GridElement GetEmptyNearbyElement()
        {
            var rnd = Program.Random;

            int count = 0;
            while (count < 8)
            {
                int i = rnd.Next(0, 8);
                if (NearbyElements[i].Passable) return NearbyElements[i];
            }
            
            count = 0;
            while (count < 16)
            {
                int i = rnd.Next(0, 8);
                int j = rnd.Next(0, 8);
                if (NearbyElements[i].NearbyElements[j].Passable) return NearbyElements[i].NearbyElements[j];
            }
            return null;
        }

        private static bool ParityCheck(int val)
        {
            return (val % 2 == 0);
        }

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

            Y = column * 30;

            Layer = 0.95f - column * 0.0001f;

            MouseIn += (sender, args) => { MouseManager.ObjectUnderPointer = this; };
            MouseOut += (sender, args) =>
            {
                if (MouseManager.ObjectUnderPointer == this)
                    MouseManager.ObjectUnderPointer = null;
            };
        }
    }
}
