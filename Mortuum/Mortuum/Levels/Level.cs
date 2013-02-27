using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Mortuum.Common;

namespace Mortuum.Levels
{
    internal class Level
    {
        protected VertexDeclaration vertexDecl;
        protected VertexBuffer vBuffer;

        protected ContentManager _content;
        protected GraphicsDeviceManager _graphics;

        protected Texture2D wallTexture;
        protected Texture2D floorTexture;
        protected Texture2D powerCircleTexture;

        protected BasicEffect levelEffect;

        protected int numVertices;
        protected int floorVerticesStart;

        public int LevelNumber
        {
            get;
            set;
        }

        public Level(ContentManager content, GraphicsDeviceManager graphics)
        {
            _content = content;
            _graphics = graphics;
        }

        public void Load()
        {
            int r = Settings.TileRows;
            int c = Settings.TileColumns;
            float h = Settings.LevelHeight;
            float k;
            int j = 0;

            numVertices = (((c + 2) * 2) + (r * 2) + (c * r) + (c * 2) + (r * 2)) * 6;

            vertexDecl = new VertexDeclaration(new VertexElement[]
            {
                new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
                new VertexElement(24, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0)
            });

            vBuffer = new VertexBuffer(_graphics.GraphicsDevice, vertexDecl, numVertices + 6, BufferUsage.None);
            VertexPositionNormalTexture[] vertices = new VertexPositionNormalTexture[numVertices + 6];

            // Top of wall, upper
            k = (r / 2) + 1;

            for (int i = ((c / 2) + 1); i > (-(c / 2) - 1); i--)
            {
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)i, h, (float)k), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(0.0f, 0.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)(i - 1), h, (float)k), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(1.0f, 0.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)i, h, (float)(k - 1)), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(0.0f, 1.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)i, h, (float)(k - 1)), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(0.0f, 1.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)(i - 1), h, (float)k), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(1.0f, 0.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)(i - 1), h, (float)(k - 1)), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(1.0f, 1.0f));
            }

            // Top of wall, lower
            k = -(r / 2);

            for (int i = ((c / 2) + 1); i > (-(c / 2) - 1); i--)
            {
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)i, h, (float)k), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(0.0f, 0.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)(i - 1), h, (float)k), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(1.0f, 0.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)i, h, (float)(k - 1)), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(0.0f, 1.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)i, h, (float)(k - 1)), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(0.0f, 1.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)(i - 1), h, (float)k), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(1.0f, 0.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)(i - 1), h, (float)(k - 1)), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(1.0f, 1.0f));
            }

            // Top of wall, left
            k = (c / 2) + 1;

            for (int i = (r / 2); i > -(r / 2); i--)
            {
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)k, h, (float)i), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(0.0f, 0.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)(k - 1), h, (float)i), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(1.0f, 0.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)k, h, (float)(i - 1)), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(0.0f, 1.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)k, h, (float)(i - 1)), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(0.0f, 1.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)(k - 1), h, (float)i), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(1.0f, 0.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)(k - 1), h, (float)(i - 1)), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(1.0f, 1.0f));
            }

            // Top of wall, right
            k = -(c / 2);

            for (int i = (r / 2); i > -(r / 2); i--)
            {
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)k, h, (float)i), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(0.0f, 0.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)(k - 1), h, (float)i), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(1.0f, 0.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)k, h, (float)(i - 1)), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(0.0f, 1.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)k, h, (float)(i - 1)), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(0.0f, 1.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)(k - 1), h, (float)i), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(1.0f, 0.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)(k - 1), h, (float)(i - 1)), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(1.0f, 1.0f));
            }

            // Wall side, top
            k = (r / 2);

            for (int i = (c / 2); i > -(c / 2); i--)
            {
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)i, h, (float)k), new Vector3(0.0f, 0.0f, -1.0f), new Vector2(0.0f, 0.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)(i - 1), h, (float)k), new Vector3(0.0f, 0.0f, -1.0f), new Vector2(1.0f, 0.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)i, (h - h), (float)k), new Vector3(0.0f, 0.0f, -1.0f), new Vector2(0.0f, 1.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)i, (h - h), (float)k), new Vector3(0.0f, 0.0f, -1.0f), new Vector2(0.0f, 1.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)(i - 1), h, (float)k), new Vector3(0.0f, 0.0f, -1.0f), new Vector2(1.0f, 0.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)(i - 1), (h - h), (float)k), new Vector3(0.0f, 0.0f, -1.0f), new Vector2(1.0f, 1.0f));
            }

            // Wall side, bottom
            k = -(r / 2);

            for (int i = -(c / 2); i < (c / 2); i++)
            {
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)i, h, (float)k), new Vector3(0.0f, 0.0f, 1.0f), new Vector2(0.0f, 0.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)(i + 1), h, (float)k), new Vector3(0.0f, 0.0f, 1.0f), new Vector2(1.0f, 0.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)i, (h - h), (float)k), new Vector3(0.0f, 0.0f, 1.0f), new Vector2(0.0f, 1.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)i, (h - h), (float)k), new Vector3(0.0f, 0.0f, 1.0f), new Vector2(0.0f, 1.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)(i + 1), h, (float)k), new Vector3(0.0f, 0.0f, 1.0f), new Vector2(1.0f, 0.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)(i + 1), (h - h), (float)k), new Vector3(0.0f, 0.0f, 1.0f), new Vector2(1.0f, 1.0f));
            }

            // Wall side, left
            k = (c / 2);

            for (int i = -(r / 2); i < (r / 2); i++)
            {
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)k, h, (float)i), new Vector3(-1.0f, 0.0f, 0.0f), new Vector2(0.0f, 0.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)k, h, (float)(i + 1)), new Vector3(-1.0f, 0.0f, 0.0f), new Vector2(1.0f, 0.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)k, (h - h), (float)i), new Vector3(-1.0f, 0.0f, 0.0f), new Vector2(0.0f, 1.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)k, (h - h), (float)i), new Vector3(-1.0f, 0.0f, 0.0f), new Vector2(0.0f, 1.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)k, h, (float)(i + 1)), new Vector3(-1.0f, 0.0f, 0.0f), new Vector2(1.0f, 0.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)k, (h - h), (float)(i + 1)), new Vector3(-1.0f, 0.0f, 0.0f), new Vector2(1.0f, 1.0f));
            }

            // Wall side, right
            k = -(c / 2);

            for (int i = (r / 2); i > -(r / 2); i--)
            {
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)k, h, (float)i), new Vector3(1.0f, 0.0f, 0.0f), new Vector2(0.0f, 0.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)k, h, (float)(i - 1)), new Vector3(1.0f, 0.0f, 0.0f), new Vector2(1.0f, 0.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)k, (h - h), (float)i), new Vector3(1.0f, 0.0f, 0.0f), new Vector2(0.0f, 1.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)k, (h - h), (float)i), new Vector3(1.0f, 0.0f, 0.0f), new Vector2(0.0f, 1.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)k, h, (float)(i - 1)), new Vector3(1.0f, 0.0f, 0.0f), new Vector2(1.0f, 0.0f));
                vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)k, (h - h), (float)(i - 1)), new Vector3(1.0f, 0.0f, 0.0f), new Vector2(1.0f, 1.0f));
            }

            floorVerticesStart = j;

            // Floor.
            for (int i = (c / 2); i > -(c / 2); i--)
            {
                for (k = (r / 2); k > -(r / 2); k--)
                {
                    vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)i, 0.0f, (float)k), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(0.0f, 0.0f));
                    vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)(i - 1), 0.0f, (float)k), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(1.0f, 0.0f));
                    vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)i, 0.0f, (float)(k - 1)), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(0.0f, 1.0f));
                    vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)i, 0.0f, (float)(k - 1)), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(0.0f, 1.0f));
                    vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)(i - 1), 0.0f, (float)k), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(1.0f, 0.0f));
                    vertices[j++] = new VertexPositionNormalTexture(new Vector3((float)(i - 1), 0.0f, (float)(k - 1)), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(1.0f, 1.0f));
                }
            }

            // Power circle.

            vertices[j++] = new VertexPositionNormalTexture(new Vector3(0.5f, 0.01f, 0.5f), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(0.0f, 0.0f));
            vertices[j++] = new VertexPositionNormalTexture(new Vector3(-0.5f, 0.01f, 0.5f), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(1.0f, 0.0f));
            vertices[j++] = new VertexPositionNormalTexture(new Vector3(0.5f, 0.01f, -0.5f), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(0.0f, 1.0f));
            vertices[j++] = new VertexPositionNormalTexture(new Vector3(0.5f, 0.01f, -0.5f), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(0.0f, 1.0f));
            vertices[j++] = new VertexPositionNormalTexture(new Vector3(-0.5f, 0.01f, 0.5f), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(1.0f, 0.0f));
            vertices[j++] = new VertexPositionNormalTexture(new Vector3(-0.5f, 0.01f, -0.5f), new Vector3(0.0f, 1.0f, 0.0f), new Vector2(1.0f, 1.0f));

            vBuffer.SetData<VertexPositionNormalTexture>(vertices);

            // Load textures.
            wallTexture = _content.Load<Texture2D>("wall_" + LevelNumber);
            floorTexture = _content.Load<Texture2D>("floor_" + LevelNumber);
            powerCircleTexture = _content.Load<Texture2D>("power_circle");

            // Load effect.
            levelEffect = new BasicEffect(_graphics.GraphicsDevice);

            levelEffect.PreferPerPixelLighting = true;
            levelEffect.EnableDefaultLighting();
            levelEffect.Alpha = 1.0f;
            levelEffect.DirectionalLight2.SpecularColor = new Vector3(0.0f, 0.0f, 0.0f);
            levelEffect.DirectionalLight0.SpecularColor = new Vector3(0.0f, 0.0f, 0.0f);
        }

        public void Unload()
        {
            vBuffer.Dispose();
            levelEffect.Dispose();
            wallTexture.Dispose();
            floorTexture.Dispose();
            powerCircleTexture.Dispose();

            vBuffer = null;
            levelEffect = null;
            wallTexture = null;
            floorTexture = null;
            powerCircleTexture = null;
        }

        public void Draw(Matrix view, Matrix projection)
        {
            levelEffect.TextureEnabled = true;
            levelEffect.Texture = wallTexture;
            levelEffect.View = view;
            levelEffect.Projection = projection;
            levelEffect.World = Matrix.Identity;

            _graphics.GraphicsDevice.SetVertexBuffer(vBuffer);

            var clampState = new SamplerState() { AddressU = TextureAddressMode.Clamp, AddressV = TextureAddressMode.Clamp };
            var oldState = _graphics.GraphicsDevice.SamplerStates[0];

            _graphics.GraphicsDevice.SamplerStates[0] = clampState;

            foreach (EffectPass pass in levelEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                _graphics.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, (floorVerticesStart / 3));
            }

            levelEffect.Texture = floorTexture;

            foreach (EffectPass pass in levelEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                _graphics.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, floorVerticesStart, ((numVertices - floorVerticesStart) / 3));
            }

            levelEffect.Texture = powerCircleTexture;

            _graphics.GraphicsDevice.BlendState = BlendState.AlphaBlend;

            foreach (EffectPass pass in levelEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                _graphics.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, numVertices, 2);
            }

            _graphics.GraphicsDevice.BlendState = BlendState.Opaque;
            _graphics.GraphicsDevice.SamplerStates[0] = oldState;
        }
    }
}
