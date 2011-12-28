using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Mortuum
{
    public class Camera
    {
        public static Matrix viewMatrix;
        public static Matrix projectionMatrix;

        private static Vector3 position;
        private static Vector3 rotation;

        private static float ratio;
        private static float fov;
        private static float near;
        private static float far;

        public static Vector3 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;

                RecalculateView();
            }
        }

        public static Vector3 Rotation
        {
            get
            {
                return rotation;
            }

            set
            {
                rotation = value;

                RecalculateView();
            }
        }

        public static Matrix View
        {
            get
            {
                return viewMatrix;
            }
        }

        public static Matrix Projection
        {
            get
            {
                return projectionMatrix;
            }
        }

        private static void RecalculateView()
        {
            Matrix rX, rY, rZ, rT, rR;

            rX = Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X));
            rY = Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y));
            rZ = Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z));
            rT = Matrix.CreateTranslation(position);

            rR = rZ;
            rR = Matrix.Multiply(rR, rY);
            rR = Matrix.Multiply(rR, rX);

            viewMatrix = Matrix.Multiply(rR, rT);
        }

        private static void RecalculateProjection()
        {
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(fov, ratio, near, far);
        }

        public static void Resize(float FOV, float Ratio, float Near, float Far)
        {
            fov = MathHelper.ToRadians(FOV);
            ratio = Ratio;
            near = Near;
            far = Far;

            RecalculateProjection();
        }

        public static Matrix LookAt(Vector3 Pos, Vector3 At)
        {
            viewMatrix = Matrix.CreateLookAt(Pos, At, new Vector3(0.0f, 1.0f, 0.0f));

            return viewMatrix;
        }

        public static void Move(Vector3 Delta)
        {
            Position = position + Delta;
        }

        public static void Rotate(Vector3 Delta)
        {
            Rotation = rotation + Delta;
        }

        public static void MoveAndRotate(Vector3 PosDelta, Vector3 RotDelta)
        {
            position = position + PosDelta;
            rotation = rotation + RotDelta;

            RecalculateView();
        }
    }
}
