using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Text;
using SFML_Engine;
using SFML.System;
using SFML.Window;
using SFML.Audio;

namespace ZombiesGame
{
    class Ammo : ScriptObject
    {
        protected Player _player;
        protected Weapon.AmmoType _type;
        protected int _amount;
        protected GameSprite _sprite_idle;

        public override void OnStart()
        {
            _graphicHandler.GraphicState = GraphicState.LAYER_2;
            foreach (GameObject obj in GameState.Objects)
            {
                if (obj.GetType() == typeof(Player))
                {
                    _player = obj as Player;
                    break;
                }
            }
        }

        public override void OnUpdate()
        {
            //Check collision with player
            if (CollisionDetection.AABB_AABB(_physicObject as AABB, _player.PhysicObject as AABB))
            {
                switch (_type)
                {
                    case Weapon.AmmoType.PISTOL: _player.PistolBullet += _amount; break;
                    case Weapon.AmmoType.RIFLE: _player.RifleBullet += _amount; break;
                    case Weapon.AmmoType.SHOTGUN: _player.Shell += _amount; break;
                }

                Destroy();
            }
        }
    }
}