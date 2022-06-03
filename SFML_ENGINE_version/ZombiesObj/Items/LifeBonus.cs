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
    class LifeBonus : BonusBox
    {
        //Sprite
        GameSprite _sprite_idle;

        public LifeBonus(Vector2f pos)
        {
            //Set bonus property
            _price = 500;

            //Set physic object
            _physicObject = new AABB(pos, 200, 200);

            //Set sprites
            _sprite_idle = new GameSprite(16, 80, 16, 16);

            //Set graphic object
            _graphicObject = _sprite_idle;
            _graphicState = GraphicState.BACKGROUND;

            //Set transformable
            _transformable.Position = pos;
            _transformable.Rotation = 0;
            _transformable.Scale = new Vector2f(50, 50);
            _transformable.Origin = new Vector2f(0, 0);
        }

        protected override void Bonus()
        {
            if(_player.Points < _price)
            {
                return;
            }

            //Aply bonus to player
            _player.Points -= _price;
            _player.MaxHealth = 10;
            _player.Health = 10;
        }
    }
}
