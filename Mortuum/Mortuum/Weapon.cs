using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Mortuum.Effects;

namespace Mortuum
{
    public enum WeaponType
    {
        Sword = 1,
        Mace,
        Axe,
        Hammer
    }

    class Weapon
    {
        private WeaponType type;

        private Model model;

        public Weapon()
        {
            type = WeaponType.Sword;
            model = null;
        }

        public void Init(ContentManager content, GraphicsDeviceManager graphics)
        {
            switch (type)
            {
                case WeaponType.Sword:
                    model = content.Load<Model>("sword");
                    break;
            }

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                }
            }
        }

        public void Unload()
        {
            model = null;
        }

        public void Update(float elapsedTime)
        {
        }

        public void Draw(Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect e in mesh.Effects)
                {
                    e.View = view;
                    e.Projection = projection;
                    e.World = Matrix.CreateTranslation(Position);

                    mesh.Draw();
                }
            }
        }

        public Vector3 Position
        {
            get;
            set;
        }

        public Vector3 Direction
        {
            get;
            set;
        }

        public WeaponType Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }
    }
}
