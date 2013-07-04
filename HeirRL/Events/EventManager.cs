using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using HeirRL.Scenes;
using HeirRL.Source;

namespace HeirRL.Events
{
    public class EventManager : GameComponent
    {
        private SceneLevel _parent;

        private List<ActionBase> Events = new List<ActionBase>();

        public EventManager(SceneLevel Parent) : base(Program.Game)
        {
            _parent = Parent;
        }

        public static EventManager Instance
        {
            get
            {
                var cLevel = SceneManager.CurrentLevel;
                if (cLevel == null) throw new NullReferenceException("Current level can't be null.");

                return cLevel.EventManager;
            }
        }    

        public override void Update(GameTime gameTime)
        {
            IngameTime current = _parent.Time;

            for (int i = 0; i < Events.Count; i++)
            {
                var action = Events[i];
                if (current.Time >= action.RaiseTime)
                {
                    action.Action();
                    if (action.OnTimeExcess())
                    {
                        Events.Remove(action);
                        i--;
                    }
                }
            }
        }
    }
}
