using System;
using System.Collections.Generic;
using System.Text;
using SFML_Engine.GameObjects.PhysicObjects;
using SFML.System;
using SFML.Graphics;
namespace ZombiesGame
{
    interface IPlayerObserver
    {
        void Notify(PlayerEvent evnt, Player player);
    }
}
