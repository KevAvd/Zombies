using System;
using System.Collections.Generic;
using System.Text;
using SFML_Engine;
using SFML.System;
using SFML.Window;
using SFML.Audio;

namespace ZombiesGame
{
    class ShellAmmo : Ammo
    {
        public ShellAmmo(Vector2f pos, int amount)
        {
            //Set weapons property
            _amount = amount;
            _type = Weapon.AmmoType.SHOTGUN;

            //Set physic object
            _physicObject = new AABB(pos, 50, 50);

            //Set graphic handler
            _graphicHandler.AddGraphicObject("base", new GameSprite(32, 16, 16, 16));
            _graphicHandler.SetDefaultSprite("base");

            //Set transformable
            _transformable.Position = pos;
            _transformable.Rotation = 0;
            _transformable.Scale = new Vector2f(50, 50);
            _transformable.Origin = new Vector2f(0, 0);
        }
    }
}