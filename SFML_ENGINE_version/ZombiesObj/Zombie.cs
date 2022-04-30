using SFML.System;
using SFML_Engine.GameObjects.GraphicObjects;
using SFML_Engine.GameObjects.PhysicObjects;
using SFML_Engine.Systems;
using SFML_Engine.Mathematics;

namespace ZombiesGame
{
    internal class Zombie : Character
    {
        float _oldHealth;
        float _deathTimeAcc;
        bool _dead = false;

        //Sprites
        GameSprite _sprite_Idle;
        GameSprite _sprite_Dead;

        public Zombie(float x, float y)
        {
            //Set speed
            _speed = 200;
            _oldHealth = 100;
            _health = 100;

            //Set AABB
            _physicObject = new AABB(new Vector2f(x, y), 100, 100);

            //Set sprites
            _sprite_Idle = new GameSprite(32, 0, 16, 16);
            _sprite_Dead = new GameSprite(0, 0, 32, 16);

            //Set graphic object
            _graphicObject = _sprite_Idle;

            //Set transformable
            _transformable.Position = new Vector2f(x, y);
            _transformable.Rotation = 0;
            _transformable.Scale = new Vector2f(50, 50);
            _transformable.Origin = new Vector2f(0, 0);
        }

        public override void OnStart()
        {

        }

        public override void OnUpdate()
        {
            //Don't update zombie if dead
            if (_dead)
            {
                _deathTimeAcc += GameTime.DeltaTimeU;

                //Destroy object 10 second after death
                if (_deathTimeAcc >= 10)
                {
                    Destroy();
                }

                return;
            }

            //Check if zombie has died
            if(_oldHealth > 0 && _health <= 0)
            {
                _graphicObject = _sprite_Dead;
                _graphicObject.Background = true;
                _transformable.Scale = new Vector2f(100, 50);
                _dead = true;
            }

            Player player = PlayerSGLTN.GetInstance().Player;

            //Move zombie to player
            _movement = player.Position - Position;

            //Make zombie aim at player
            Rotation = MathF.Atan2(player.Position.Y - Position.Y, player.Position.X - Position.X);

            //Update old health
            _oldHealth = _health;

            //Execute base methode
            base.OnUpdate();
        }
    }
}