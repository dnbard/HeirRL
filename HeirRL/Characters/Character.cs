using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using HeirRL.Graphics;
using HeirRL.Level;
using HeirRL.Source;

namespace HeirRL.Characters
{
    public class Character : VisualComponent
    {
        private GridElement _parent;
        public GridElement Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
                _targetX = value.X;
                _targetY = value.Y;
                Layer = CalculateLayer();
            }
        }

        private float _targetX = 0, _targetY = 0;

        public int MovementCost { get; set; }

        private static float CharacterLayerHeight = 0.85f;

        public Character(GridElement location)
        {
            Texture = ImagesManager.Get("chars-default");
            TextureKey = "full";
            Origin = Origin.Center;

            _parent = location;

            Layer = CalculateLayer();
            Scale = 0.75f;
        }

        private float CalculateLayer()
        {
            return CharacterLayerHeight - Parent.Column * 0.0001f;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (_targetX > X) X--;
            else if (_targetX < X) X++;

            if (_targetX > Y) X--;
            else if (_targetX < Y) X++;
        }        
    }
}
