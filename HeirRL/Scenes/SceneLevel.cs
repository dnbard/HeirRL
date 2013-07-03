using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeirRL.Characters;
using HeirRL.Events;
using HeirRL.Graphics;
using HeirRL.Level;
using HeirRL.Source;

namespace HeirRL.Scenes
{
    public class SceneLevel : Scene
    {
        public IngameTime Time { get; protected set; }
        public EventManager EventManager { get; protected set; }

        public MapGrid Grid { get; set; }

        public SceneLevel() : base("level")
        {
            Camera = new LevelCamera();
            Add(new MousePointer());

            Time = new IngameTime(this); Add(Time);
            EventManager = new EventManager(this); Add(EventManager);
            Grid = new MapGrid(11, 19); Add(Grid);            

            Add(Player.GetOrCreate(Grid[0, 0]));
        }
    }
}
