using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HeirRL.Source
{
    public  static class MonoGameExtensions
    {
        public static void SetPosition(this GameWindow window, Point position) 
        {
            OpenTK.GameWindow otkWindow = GetForm(window);
            if (otkWindow != null) 
            {
                otkWindow.X = position.X;
                otkWindow.Y = position.Y;
            }
        }

        public static void SetResolution(this GameWindow window, GraphicsDeviceManager graphics, Point resolution)
        {
            OpenTK.GameWindow otkWindow = GetForm(window);
            if (otkWindow != null)
            {
                window.AllowUserResizing = true;
                otkWindow.Width = resolution.X;
                otkWindow.Height = resolution.Y;
                graphics.PreferredBackBufferHeight = resolution.Y;
                graphics.PreferredBackBufferWidth = resolution.X;
                graphics.ApplyChanges();
            }
        }
 
        public static OpenTK.GameWindow GetForm(this GameWindow gameWindow) {
            Type type = typeof(OpenTKGameWindow);
            System.Reflection.FieldInfo field = type.GetField("window", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (field != null)
                return field.GetValue(gameWindow) as OpenTK.GameWindow;
            return null;
        }
    }
}

