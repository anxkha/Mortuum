using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Mortuum.Ui
{
    class Button : IElement
    {
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
            set { m_FontName = value; }
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

            m_Font = null;
            Font = "";

            Text = "";
            Name = "";

            m_Loaded = false;
        }

        public bool Load(ContentManager content, GraphicsDeviceManager graphics)
        {
            if (string.IsNullOrEmpty(Name))
                throw new ArgumentNullException("Name", "The button control must have a name.");

            if (string.IsNullOrEmpty(Name))
                throw new ArgumentNullException("Font", "A font for the button text must be specified.");

            m_Content = content;
            m_Graphics = graphics;

            m_Font = m_Content.Load<SpriteFont>(m_FontName);

            m_SpriteBatch = new SpriteBatch(graphics.GraphicsDevice);

            m_Loaded = true;

            return true;
        }

        public void Unload()
        {
            m_Font = null;
            m_SpriteBatch = null;
        }

        public void Update(float fElapsedTime)
        {
            if (!m_Loaded) return;
        }

        public void Draw()
        {
            if (!m_Loaded) return;

            var px = (int)Position.X;
            var py = (int)Position.Y;
            var sx = (int)Size.X;
            var sy = (int)Size.Y;

            if (Parent != null)
            {
                px += (int)Parent.Position.X;
                py += (int)Parent.Position.Y;
            }

            Texture2D borderTex = new Texture2D(m_Graphics.GraphicsDevice, 1, 1, true, SurfaceFormat.Color);
            Texture2D backgroundTex = new Texture2D(m_Graphics.GraphicsDevice, 1, 1, true, SurfaceFormat.Color);

            borderTex.SetData(new Color[] { new Color(1.0f, 1.0f, 1.0f) });
            backgroundTex.SetData(new Color[] { new Color(0.0f, 0.0f, 0.0f, 0.5f) });

            m_SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);

            m_SpriteBatch.Draw(backgroundTex, new Rectangle(px, py, sx, sy), Color.White);

            m_SpriteBatch.Draw(borderTex, new Rectangle(px, py, sx, 1), Color.White);
            m_SpriteBatch.Draw(borderTex, new Rectangle(px, py, 1, sy), Color.White);
            m_SpriteBatch.Draw(borderTex, new Rectangle(px + sx - 1, py, 1, sy), Color.White);
            m_SpriteBatch.Draw(borderTex, new Rectangle(px, py + sy - 1, sx, 1), Color.White);

            m_SpriteBatch.DrawString(m_Font, Name, new Vector2(px, py), Color.White);

            m_SpriteBatch.End();
        }

        private ContentManager m_Content;
        private GraphicsDeviceManager m_Graphics;

        private SpriteFont m_Font;
        private SpriteBatch m_SpriteBatch;

        private string m_FontName;

        private bool m_Loaded;
    }
}
