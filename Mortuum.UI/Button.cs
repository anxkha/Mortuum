using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mortuum.Ui
{
    public class Button : IElement
    {
        private ContentManager _content;
        private GraphicsDeviceManager _graphics;
        private SpriteFont _font;
        private SpriteBatch _spriteBatch;

        private string _fontName;
        private bool _loaded;

        public bool Hidden
        {
            get;
            set;
        }

        public bool IsActive
        {
            get;
            set;
        }

        public Vector2 Position
        {
            get;
            set;
        }

        public Vector2 Size
        {
            get;
            set;
        }

        public IElement Parent
        {
            get;
            set;
        }

        public string Font
        {
            set { _fontName = value; }
        }

        public string Name
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        public Button()
        {
            Hidden = false;
            IsActive = false;

            Position = new Vector2(10, 10);
            Size = new Vector2(20, 50);

            Parent = null;

            _font = null;
            Font = "";

            Text = "";
            Name = "";

            _loaded = false;
        }

        public bool Load(ContentManager content, GraphicsDeviceManager graphics)
        {
            if (string.IsNullOrEmpty(Name))
                throw new ArgumentNullException("Name", "The button control must have a name.");

            if (string.IsNullOrEmpty(Name))
                throw new ArgumentNullException("Font", "A font for the button text must be specified.");

            _content = content;
            _graphics = graphics;

            _font = _content.Load<SpriteFont>(_fontName);

            _spriteBatch = new SpriteBatch(graphics.GraphicsDevice);

            _loaded = true;

            return true;
        }

        public void Unload()
        {
            _font = null;
            _spriteBatch = null;
        }

        public void Update(float fElapsedTime)
        {
            if (!_loaded) return;
        }

        public void Draw()
        {
            if (!_loaded) return;

            var px = (int)Position.X;
            var py = (int)Position.Y;
            var sx = (int)Size.X;
            var sy = (int)Size.Y;

            if (Parent != null)
            {
                px += (int)Parent.Position.X;
                py += (int)Parent.Position.Y;
            }

            Texture2D borderTex = new Texture2D(_graphics.GraphicsDevice, 1, 1, true, SurfaceFormat.Color);
            Texture2D backgroundTex = new Texture2D(_graphics.GraphicsDevice, 1, 1, true, SurfaceFormat.Color);

            borderTex.SetData(new Color[] { new Color(1.0f, 1.0f, 1.0f) });
            backgroundTex.SetData(new Color[] { new Color(0.0f, 0.0f, 0.0f, 0.5f) });

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);

            _spriteBatch.Draw(backgroundTex, new Rectangle(px, py, sx, sy), Color.White);

            _spriteBatch.Draw(borderTex, new Rectangle(px, py, sx, 1), Color.White);
            _spriteBatch.Draw(borderTex, new Rectangle(px, py, 1, sy), Color.White);
            _spriteBatch.Draw(borderTex, new Rectangle(px + sx - 1, py, 1, sy), Color.White);
            _spriteBatch.Draw(borderTex, new Rectangle(px, py + sy - 1, sx, 1), Color.White);

            _spriteBatch.DrawString(_font, Name, new Vector2(px, py), Color.White);

            _spriteBatch.End();
        }
    }
}
