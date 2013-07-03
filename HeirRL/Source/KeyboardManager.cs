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

        private TimeSpan _lastAction = TimeSpan.FromMilliseconds(0);
        private TimeSpan _currentTime = TimeSpan.FromMilliseconds(0);
        private TimeSpan _actionDelay = TimeSpan.FromMilliseconds(350);

        private KeyboardManager() : base(Program.Game) { }

        public override void Update(GameTime gameTime)
        {
            _old = _current;
            _current = Keyboard.GetState();
            _currentTime = Program.Game.GameTime;
        }

        public static bool IsKeyDown(Keys key)
        {
            var self = Instance;
            return self._current.IsKeyDown(key) && 
                self._currentTime - self._lastAction >= self._actionDelay;
        }

        public static bool IsKeyPress(Keys key)
        {
            var self = Instance;
            return Instance._current.IsKeyUp(key) && Instance._old.IsKeyDown(key) &&
                self._currentTime - self._lastAction >= self._actionDelay;
        }

        public static void RegisterKeyAction()
        {
            var self = Instance;
            self._lastAction = Program.Game.GameTime;
        }
    }
}
