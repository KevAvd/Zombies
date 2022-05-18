using System;
using System.Collections.Generic;
using System.Text;
using SFML_Engine.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;
using SFML_Engine.GameObjects;
using SFML_Engine.Systems;
using SFML_Engine.Enums;
using SFML_Engine.Mathematics;
using SFML_Engine.GameObjects.PhysicObjects;
using SFML_Engine.GameObjects.GraphicObjects;
using SFML.System;
using SFML.Window;
using SFML.Audio;

namespace ZombiesGame
{
    class RifleAmmo : Ammo
    {
        public RifleAmmo(Vector2f pos, int amount, Player p)
        {
            //Set player
            _player = p;

            //Set ammo property
            _amount = amount;
            _type = Weapon.AmmoType.RIFLE;

            //Set physic object
            _physicObject = new AABB(pos, 50, 50);

            //Set sprites
            _sprite_idle = new GameSprite(48, 32, 16, 16);

            //Set graphic object
            _graphicObject = _sprite_idle;
            _graphicObject.State = GraphicState.BACKGROUND;

            //Set transformable
            _transformable.Position = pos;
            _transformable.Rotation = 0;
            _transformable.Scale = new Vector2f(50, 50);
            _transformable.Origin = new Vector2f(0, 0);
        }
    }
}