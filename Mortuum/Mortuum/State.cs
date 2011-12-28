using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mortuum
{
    enum GameState
    {
        TitleScreen = 1,
        WinScreen,
        LoseScreen,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7,
        Level8,
        Level9,
        Level10
    };

    class State
    {
        public virtual bool Load()
        {
            return false;
        }

        public virtual void Unload()
        {
        }

        public virtual GameState Update(float elapsedGameTime)
        {
            return GameState.TitleScreen;
        }

        public virtual void Draw()
        {
        }
    }
}
