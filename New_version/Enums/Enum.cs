using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombiesGame.Enums
{
    enum AmmoType
    {
        PISTOL,
        RIFLE,
        SHOTGUN
    }

    enum ObjectState
    {
        ALIVE,
        DEAD
    }

    enum ItemState
    {
        INVENTORY,
        ONGROUND
    }

    enum AnimationType
    {
        STATIC,
        NORMAL,
        REVERSE,
        LOOP
    }
}
