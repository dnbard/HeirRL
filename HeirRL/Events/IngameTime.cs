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
                var currentScene = SceneManager.Current;
                if (currentScene == null) throw new NullReferenceException("Current scene can't be null.");

                var levelScene = currentScene as SceneLevel;
                if (levelScene == null) throw new Exception("To get an scene time, scene must be a level.");

                return levelScene.Time;
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
