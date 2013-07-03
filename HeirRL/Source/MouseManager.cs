using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeirRL;
using HeirRL.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HeirRL.Source
{
    class MouseManager : GameComponent
    {
        public static VisualComponent ObjectUnderPointer { get; set; }

        private static MouseManager _instance;
        public static MouseManager Instance
        {
            get 
            { 
                if (_instance == null)
                    _instance = new MouseManager();
                return _instance;
            }
        }

        private MouseManager() : base(Program.Game) { }

        private MouseState _mouseCurrent;
        private MouseState _mouseLast;

        public static Vector2 MouseDiff
        {
            get
            {
                var current = Instance._mouseCurrent;
                var last = Instance._mouseLast;

                return new Vector2(last.X - current.X, last.Y - current.Y);
            }
        }

        public static bool MouseMoved
        {
            get 
            {
                var current = Instance._mouseCurrent;
                var last = Instance._mouseLast;

                return current.X != last.X || current.Y != last.Y;
            }
        }

        public static int X
        {
            get { return Instance._mouseCurrent.X; }
        }

        public static int Y
        {
            get { return Instance._mouseCurrent.Y; }
        }

        public static bool LeftButtonClick
        {
            get
            {
                return Instance._mouseCurrent.LeftButton == ButtonState.Released &&
                       Instance._mouseLast.LeftButton == ButtonState.Pressed;
            }
        }

        public static bool RightButtonClick
        {
            get
            {
                return Instance._mouseCurrent.RightButton == ButtonState.Released &&
                       Instance._mouseLast.RightButton == ButtonState.Pressed;
            }
        }

        public static bool LeftButtonPress
        {
            get { return Instance._mouseCurrent.LeftButton == ButtonState.Pressed; }
        }

        public static bool RightButtonPress
        {
            get { return Instance._mouseCurrent.RightButton == ButtonState.Pressed; }
        }

        public override void Update(GameTime gTime)
        {
            _mouseLast = _mouseCurrent;
            _mouseCurrent = Mouse.GetState();
        }
    }
}
