using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeirRL.Scenes;
using Microsoft.Xna.Framework;

namespace HeirRL.Events
{
    public class EventManager : GameComponent
    {
        private SceneLevel _parent;

        

        public EventManager(SceneLevel Parent) : base(Program.Game)
        {
            _parent = Parent;
        }
    }
}
