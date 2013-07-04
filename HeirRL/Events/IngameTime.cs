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
        public static IngameTime Instance
        {
            get
            {
                var cLevel = SceneManager.CurrentLevel;
                if (cLevel == null) throw new NullReferenceException("Current level can't be null.");

                return cLevel.Time;
            }
        }  

        public long Time { get; private set; }
        public int TickSinceLastIncrement { get; private set; }

        public event EventHandler OnTimeIncrement;

        private SceneLevel _parent;

        public IngameTime(SceneLevel Parent):base(Program.Game)
        {
            _parent = Parent;
        }

        public override void Update(GameTime gameTime)
        {
            TickSinceLastIncrement++;
        }

        public void Increment(int Value)
        {
            Time += Value;
            TickSinceLastIncrement = 0;

            if (OnTimeIncrement != null) OnTimeIncrement(this, null);
        }
    }
}
