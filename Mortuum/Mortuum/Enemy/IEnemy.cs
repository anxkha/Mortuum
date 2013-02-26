using Microsoft.Xna.Framework;

namespace Mortuum.Enemy
{
    internal interface IEnemy
    {
        void SetPosition(Vector3 position);

        Vector3 GetPosition();

        void SetDirection(float direction);

        float GetDirection();

        void Load();

        void Unload();

        void Update(float elapsedTime);

        void Draw(Matrix view, Matrix projection);
    }
}
