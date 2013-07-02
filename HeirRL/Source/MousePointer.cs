using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeirRL.Graphics;
using Microsoft.Xna.Framework;

namespace HeirRL.Source
{
    class MousePointer : DrawableGameComponent
    {
        private VisualComponent _object = null;
        private VisualComponent ObjectUnderPointer
        {
            get { return _object; }
            set
            {
                if (_object != value && ObjectChanged != null) 
                    ObjectChanged(this, null);
                _object = value;
            }
        }

        private event EventHandler ObjectChanged;

        public MousePointer() : base(Program.Game)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            ObjectUnderPointer = MouseManager.ObjectUnderPointer;
        }
    }
}
