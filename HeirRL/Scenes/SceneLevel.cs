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
        public IngameTime Time { get; set; }

        public SceneLevel() : base("level")
        {
            Camera = new LevelCamera();

            Add(new MousePointer());

            Add(new MapGrid(11, 19));
            Add(new Character());
        }
    }
}
