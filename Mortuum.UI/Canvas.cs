using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mortuum.Ui
{
    public class Canvas : IElement
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

        public string Name
        {
            get;
            set;
        }

        public Canvas()
        {
            Hidden = false;
            IsActive = false;

            Position = new Vector2(10, 10);
            Size = new Vector2(300, 200);

            m_Children = new List<IElement>(1);

            m_Loaded = false;
        }

        public bool Load(ContentManager content, GraphicsDeviceManager graphics)
        {
            m_Content = content;
            m_Graphics = graphics;

            m_SpriteBatch = new SpriteBatch(graphics.GraphicsDevice);

            m_BorderTex = new Texture2D(m_Graphics.GraphicsDevice, 1, 1, true, SurfaceFormat.Color);
            m_BackgroundTex = new Texture2D(m_Graphics.GraphicsDevice, 1, 1, true, SurfaceFormat.Color);

            m_BorderTex.SetData(new Color[] { new Color(1.0f, 1.0f, 1.0f) });
            m_BackgroundTex.SetData(new Color[] { new Color(0.0f, 0.0f, 0.0f, 0.5f) });

            m_Loaded = true;

            return true;
        }

        public void Unload()
        {
            foreach (var e in m_Children)
            {
                e.Unload();
            }

            m_SpriteBatch = null;
        }

        public void Update(float fElapsedTime)
        {
            if (!m_Loaded) return;
            if (Hidden) return;

            foreach (var e in m_Children)
            {
                e.Update(fElapsedTime);
            }
        }

        public void Draw()
        {
            if (!m_Loaded) return;
            if (Hidden) return;

            m_SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);

            m_SpriteBatch.Draw(m_BackgroundTex, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), Color.White);

            m_SpriteBatch.Draw(m_BorderTex, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, 1), Color.White);
            m_SpriteBatch.Draw(m_BorderTex, new Rectangle((int)Position.X, (int)Position.Y, 1, (int)Size.Y), Color.White);
            m_SpriteBatch.Draw(m_BorderTex, new Rectangle((int)Position.X + (int)Size.X - 1, (int)Position.Y, 1, (int)Size.Y), Color.White);
            m_SpriteBatch.Draw(m_BorderTex, new Rectangle((int)Position.X, (int)Position.Y + (int)Size.Y - 1, (int)Size.X, 1), Color.White);

            m_SpriteBatch.End();

            foreach (var e in m_Children)
            {
                e.Draw();
            }
        }

        public void AddChild(IElement child)
        {
            child.Parent = this;

            m_Children.Add(child);
        }

        private GraphicsDeviceManager m_Graphics;
        private ContentManager m_Content;

        private SpriteBatch m_SpriteBatch;

        private Texture2D m_BorderTex;
        private Texture2D m_BackgroundTex;

        private List<IElement> m_Children;

        private bool m_Loaded;
    }
}
