using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeirRL.Scenes;
using HeirRL.Source;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeirRL.Graphics
{
    public class VisualComponent: DrawableGameComponent
    {
        protected SpriteBatch SpriteBatch;

        public float X { get; set; }
        public float Y { get; set; }

        public Vector2 Position
        {
            get{return new Vector2(X, Y);}
            set 
            { 
                X = value.X;
                Y = value.Y;
            }
        }

        public float Width { get; set; }
        public float Height { get; set; }

        public Vector2 Size
        {
            get{ return new Vector2(Width, Height);}
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }

        public virtual Rectangle DrawRectangle
        {
            get{return new Rectangle((int)X, (int)Y, (int)Width, (int)Height);}
        }

        private float _layer = 0f;
        private const float LayerMin = 0f;
        private const float LayerMax = 1f;

        public float Layer
        {
            get { return _layer; }
            set
            {
                if (value >= LayerMin && value <= LayerMax)
                    _layer = value;
            }
        }

        private Image _texture = null;
        public Image Texture
        {
            get { return _texture; }
            set
            {
                _texture = value;
                if (TextureChanged != null)
                    TextureChanged(this, null);
            }
        }

        private string _textureKey = "";
        public string TextureKey
        {
            get { return _textureKey; }
            set
            {
                _textureKey = value;
                if (TextureChanged != null)
                    TextureChanged(this, null);
            }
        }
        
        public Color Overlay { get; set; }
        public float Scale { get; set; }
        public float Rotation { get; set; }

        public event VisualEventHandler TextureChanged;
        public event VisualEventHandler OriginChanged;
        public event VisualEventHandler MouseIn;
        public event VisualEventHandler MouseOut;
        public event VisualEventHandler MouseHover;
        public event VisualEventHandler MouseLeftClick;

        private Origin _origin;
        public Origin Origin
        {
            get { return _origin; }
            set
            {
                _origin = value;
                if (OriginChanged != null)
                    OriginChanged(this, null);
            }
        }

        protected Vector2 OriginValue;

        public VisualComponent() : base(Program.Game)
        {
            SpriteBatch = Program.Game._spriteBatch;
            Overlay = Color.White;
            Scale = 1f;

            TextureChanged += CalculateTextureSize;   
            OriginChanged += OnOriginChanged;
        }

        protected static void OnOriginChanged(VisualComponent sender, VisualEventArgs visualEventArgs)
        {
            switch (sender.Origin)
            {
                case Origin.Default:
                    sender.OriginValue = Vector2.Zero;
                    break;
                case Origin.Center:
                    sender.OriginValue = new Vector2(sender.Width * 0.5f, sender.Height * 0.5f);
                    break;
            }
        }

        protected static void CalculateTextureSize(VisualComponent sender, VisualEventArgs visualEventArgs)
        {
            if (string.IsNullOrEmpty(sender.TextureKey) || sender.Texture == null) return;
            var rect = sender.Texture.GetSourceRect(sender._textureKey);
            sender.Width = rect.Width;
            sender.Height = rect.Height;

            OnOriginChanged(sender, null);
        }

        public override void Draw(GameTime gameTime)
        {
            Texture.Draw(SpriteBatch, TextureKey, CalculateCoordsWithCamera(), Rotation, Scale, OriginValue, Overlay, Layer);
        }

        protected Vector2 CalculateCoordsWithCamera()
        {
            var scene = SceneManager.Current;
            return Position - scene.Camera.Position;
        }

        private bool _isHover, _lastTickHover;

        public override void Update(GameTime gameTime)
        {
            _isHover = PointInRect(Origin, new Vector2(MouseManager.X, MouseManager.Y), CalculateCoordsWithCamera(), Size * 0.5f);
            if (_isHover)
            {
                if (_lastTickHover)
                {
                    if (MouseHover != null) MouseHover(this, null);
                }
                else
                {
                    if (MouseIn != null) MouseIn(this, null);
                    _lastTickHover = true;
                }

                if (MouseManager.LeftButtonClick && MouseLeftClick != null) 
                    MouseLeftClick(this, null);
            }
            else if (_lastTickHover)
            {
                if (MouseOut != null) MouseOut(this, null);
                _lastTickHover = false;
            }
        }

        private static bool PointInRect(Origin origin, Vector2 point, Vector2 location, Vector2 size)
        {
            if (origin == Origin.Default)
            {
                bool x = (point.X >= location.X) && (point.X <= location.X + size.X);
                if (!x) return false;
                bool y = (point.Y >= location.Y) && (point.Y <= location.Y + size.Y);
                return y;
            }
            else
            {
                bool x = (point.X >= location.X - size.X * 0.5f) && (point.X <= location.X + size.X * 0.5f);
                if (!x) return false;
                bool y = (point.Y >= location.Y - size.Y * 0.5f) && (point.Y <= location.Y + size.Y * 0.5f);
                return y;
            }
        }
    }
}
