using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeirRL.Graphics;
using HeirRL.Level;
using HeirRL.Source;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HeirRL.Characters
{
    class Character : VisualComponent
    {
        public GridElement Parent { get; protected set; }

        public Character()
        {
            Texture = ImagesManager.Get("chars-default");
            TextureKey = "full";
            Origin = Origin.Center;

            Layer = 0.85f /*- column * 0.0001f*/;
            Scale = 0.75f;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            var keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Right))
            {
                X += 48;
            }
        }
    }
}
