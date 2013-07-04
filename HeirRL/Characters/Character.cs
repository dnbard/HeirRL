using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using HeirRL.Graphics;
using HeirRL.Level;
using HeirRL.Source;
using HeirRL.Scenes;

namespace HeirRL.Characters
{
    public partial class Character : VisualComponent
    {
        protected long Time = 0;
        public bool IsPlayer { get; protected set; }

        public bool IsHostile { get; set; }

        private GridElement _parent;
        public GridElement Parent
        {
            get { return _parent; }
            set
            {
                _parent.Creature = null;
                value.Creature = this;
                _parent = value;                
                _targetX = value.X;
                _targetY = value.Y;
                Layer = CalculateLayer();
            }
        }

        private float _targetX = 0, _targetY = 0;

        public int MovementCost { get; set; }
        public int MeleeAttackCost { get; set; }

        private static float CharacterLayerHeight = 0.85f;

        public Character(GridElement location)
        {
            IsPlayer = false;
            IsHostile = true;

            Texture = ImagesManager.Get("chars-default");
            TextureKey = "full";
            Origin = Origin.Center;

            Parent = location;
            Parent.Creature = this;

            Layer = CalculateLayer();
            Scale = 0.75f;

            InitializeAttributes();

            MovementCost = 100;
            MeleeAttackCost = 100;
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

        protected void Remove()
        {
            var cLevel = SceneManager.CurrentLevel;
            if (cLevel == null) throw new NullReferenceException("Current level can't be null.");

            Parent.Creature = null;
            cLevel.Remove(this);
        }
    }
}
