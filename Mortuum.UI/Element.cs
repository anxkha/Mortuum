using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Mortuum.Ui
{
    public interface IElement
    {
        bool Hidden
        {
            get;
            set;
        }

        bool IsActive
        {
            get;
            set;
        }

        Vector2 Position
        {
            get;
            set;
        }

        Vector2 Size
        {
            get;
            set;
        }

        IElement Parent
        {
            get;
            set;
        }

        string Name
        {
            get;
            set;
        }

        bool Load(ContentManager content, GraphicsDeviceManager graphics);

        void Unload();

        void Update(float fElapsedTime);

        void Draw();
    }
}
