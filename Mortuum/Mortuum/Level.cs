﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mortuum
{
    class Level
    {
        protected VertexDeclaration vertexDecl;
        protected VertexBuffer vBuffer;

        protected BasicEffect effect;
        protected ContentManager content;
        protected GraphicsDeviceManager graphics;

        protected Texture2D wallTexture;
        protected Texture2D floorTexture;

        protected Effect levelEffect;

        protected Vector4[] LightColors;
        protected float[] LightPowers;
        protected Vector3[] LightPositions;

        protected bool loaded;
        protected int numVertices;
        protected int floorVerticesStart;

        public Level()
        {
            loaded = false;
        }

        public bool Load(string level, ContentManager content, GraphicsDeviceManager graphics)
        {
            int r = Settings.TileRows;
            int c = Settings.TileColumns;
            float h = Settings.LevelHeight;
            float k;
            int j = 0;

            this.content = content;
            this.graphics = graphics;

            numVertices = (((c + 2) * 2) + (r * 2) + (c * r) + (c * 2) + (r * 2)) * 6;

            vertexDecl = new VertexDeclaration(new VertexElement[]
            {
                new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
                new VertexElement(24, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0)
            });

            vBuffer = new VertexBuffer(graphics.GraphicsDevice, vertexDecl, numVertices, BufferUsage.None);
            VertexPositionNormalTexture[] vertices = new VertexPositionNormalTexture[numVertices];

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

            vBuffer.SetData<VertexPositionNormalTexture>(vertices);

            // Load textures.
            wallTexture = content.Load<Texture2D>("wall_5");
            floorTexture = content.Load<Texture2D>("floor_5");

            // Load effect.
            levelEffect = content.Load<Effect>("levelfx");

            LightColors = new Vector4[]
            {
                new Vector4(1.0f, 0.941176f, 0.607843f, 1.0f),
                new Vector4(1.0f, 0.941176f, 0.607843f, 1.0f),
                new Vector4(1.0f, 0.941176f, 0.607843f, 1.0f),
                new Vector4(1.0f, 0.941176f, 0.607843f, 1.0f)
            };

            LightPowers = new float[]
            {
                1.0f,
                1.0f,
                1.0f,
                1.0f
            };

            LightPositions = new Vector3[]
            {
                new Vector3(5.0f, 1.1f, 4.0f),
                new Vector3(-5.0f, 1.1f, 4.0f),
                new Vector3(5.0f, 1.1f, -4.0f),
                new Vector3(-5.0f, 1.1f, -4.0f)
            };

            levelEffect.Parameters["LightPos"].SetValue(LightPositions);
            levelEffect.Parameters["LightPower"].SetValue(LightPowers);
            levelEffect.Parameters["LightColor"].SetValue(LightColors);

            loaded = true;

            return true;
        }

        public void Draw(Matrix view, Matrix projection)
        {
            if (!loaded) return;

            levelEffect.CurrentTechnique = levelEffect.Techniques["Torches"];

            levelEffect.Parameters["View"].SetValue(view);
            levelEffect.Parameters["Projection"].SetValue(projection);
            levelEffect.Parameters["World"].SetValue(Matrix.Identity);
            levelEffect.Parameters["xTexture"].SetValue(wallTexture);

            graphics.GraphicsDevice.SetVertexBuffer(vBuffer);

            foreach (EffectPass pass in levelEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphics.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, (floorVerticesStart / 3));
            }

            levelEffect.Parameters["xTexture"].SetValue(floorTexture);

            foreach (EffectPass pass in levelEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphics.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, floorVerticesStart, ((numVertices - floorVerticesStart) / 3));
            }
        }
    }
}