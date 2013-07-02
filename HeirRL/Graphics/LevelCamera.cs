using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeirRL.Source;

namespace HeirRL.Graphics
{
    class LevelCamera: StaticCamera
    {
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            if (MouseManager.RightButtonPress)
            {
                var mouseDiff = MouseManager.MouseDiff;
                X += mouseDiff.X;
                Y += mouseDiff.Y;
            }
        }
    }
}
