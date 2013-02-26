using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mortuum.Ui
{
    public class Canvas : IElement
    {
        private GraphicsDeviceManager _graphics;
        private ContentManager _content;
        private SpriteBatch _spriteBatch;
        private Texture2D _borderTex;
        private Texture2D _backgroundTex;

        private List<IElement> _children;
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

            _children = new List<IElement>(1);

            _loaded = false;
        }

        public bool Load(ContentManager content, GraphicsDeviceManager graphics)
        {
            _content = content;
            _graphics = graphics;

            _spriteBatch = new SpriteBatch(graphics.GraphicsDevice);

            _borderTex = new Texture2D(_graphics.GraphicsDevice, 1, 1, true, SurfaceFormat.Color);
            _backgroundTex = new Texture2D(_graphics.GraphicsDevice, 1, 1, true, SurfaceFormat.Color);

            _borderTex.SetData(new Color[] { new Color(1.0f, 1.0f, 1.0f) });
            _backgroundTex.SetData(new Color[] { new Color(0.0f, 0.0f, 0.0f, 0.5f) });

            _loaded = true;

            return true;
        }

        public void Unload()
        {
            foreach (var e in _children)
            {
                e.Unload();
            }

            _spriteBatch = null;
        }

        public void Update(float fElapsedTime)
        {
            if (!_loaded) return;
            if (Hidden) return;

            foreach (var e in _children)
            {
                e.Update(fElapsedTime);
            }
        }

        public void Draw()
        {
            if (!_loaded) return;
            if (Hidden) return;

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);

            _spriteBatch.Draw(_backgroundTex, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), Color.White);

            _spriteBatch.Draw(_borderTex, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, 1), Color.White);
            _spriteBatch.Draw(_borderTex, new Rectangle((int)Position.X, (int)Position.Y, 1, (int)Size.Y), Color.White);
            _spriteBatch.Draw(_borderTex, new Rectangle((int)Position.X + (int)Size.X - 1, (int)Position.Y, 1, (int)Size.Y), Color.White);
            _spriteBatch.Draw(_borderTex, new Rectangle((int)Position.X, (int)Position.Y + (int)Size.Y - 1, (int)Size.X, 1), Color.White);

            _spriteBatch.End();

            foreach (var e in _children)
            {
                e.Draw();
            }
        }

        public void AddChild(IElement child)
        {
            child.Parent = this;

            _children.Add(child);
        }
    }
}
