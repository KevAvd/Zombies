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
    class Heart : ScriptObject
    {
        //Player
        Player _player;     //Reference to player

        //Property
        bool _halfHeart;    //Indicate if the heart is full or not

        //Sprites
        GameSprite _sprite_idle;

        public Heart(Vector2f pos, bool halfHeart ,Player p)
        {
            //Set player
            _player = p;

            //Set physic object
            _physicObject = new AABB(pos, 50, 50);

            //Set sprites
            _sprite_idle = new GameSprite(32, 64, 16, 16);

            //Set graphic object
            _graphicObject = _sprite_idle;
            _graphicObject.State = GraphicState.BACKGROUND;

            //Set transformable
            _transformable.Position = pos;
            _transformable.Rotation = 0;
            _transformable.Scale = new Vector2f(50, 50);
            _transformable.Origin = new Vector2f(0, 0);
        }

        public override void OnStart()
        {

        }

        public override void OnUpdate()
        {
            //Check collision with player
            if (CollisionDetection.AABB_AABB(_physicObject as AABB, _player.PhysicObject as AABB))
            {
                if (_player.Health < _player.MaxHealth)
                {
                    if (_halfHeart)
                    {
                        _player.Health += 0.5f;
                    }
                    else
                    {
                        _player.Health += 1.0f;
                    }
                }
                Destroy();
            }
        }
    }
}
