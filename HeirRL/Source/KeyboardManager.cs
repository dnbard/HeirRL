using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HeirRL.Source
{
    public class KeyboardManager: GameComponent 
    {
        private static readonly object _sync = new object();

        private static KeyboardManager _instance;
        public static KeyboardManager Instance
        {
            get
            {
                if (_instance == null)
                    lock(_sync)
                        _instance = new KeyboardManager();
                return _instance;
            }
        }

        private KeyboardState _current = Keyboard.GetState();
        private KeyboardState _old;

        private KeyboardManager() : base(Program.Game) { }

        public override void Update(GameTime gameTime)
        {
            _old = _current;
            _current = Keyboard.GetState();
        }

        public bool IsKeyDown(Keys key)
        {
            return _current.IsKeyDown(key);
        }

        public bool IsKeyPress(Keys key)
        {
            return _current.IsKeyUp(key) && _old.IsKeyDown(key);
        }
    }
}
