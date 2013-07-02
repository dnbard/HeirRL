using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeirRL.Scenes;
using Microsoft.Xna.Framework;

namespace HeirRL.Events
{
    public class IngameTime: GameComponent
    {
        public long Time { get; private set; }
        public int TickSinceLastUpdate { get; private set; }

        public event EventHandler OnTimeIncrement;

        private SceneLevel _parent;

        public IngameTime(SceneLevel Parent):base(Program.Game)
        {
            _parent = Parent;
        }

        public override void Update(GameTime gameTime)
        {
            TickSinceLastUpdate++;
        }

        public void Increment(int Value)
        {
            Time += Value;
            TickSinceLastUpdate = 0;

            if (OnTimeIncrement != null) OnTimeIncrement(this, null);
        }
    }
}
