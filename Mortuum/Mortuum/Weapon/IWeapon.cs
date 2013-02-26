using Microsoft.Xna.Framework;

namespace Mortuum.Weapon
{
    internal interface IWeapon
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
