using SFML.System;
using SFML_Engine.GameObjects.GraphicObjects;
using SFML_Engine.GameObjects.PhysicObjects;
using SFML_Engine.Systems;
using SFML_Engine.Mathematics;
using SFML_Engine.Enums;
using System;
using SFML.Graphics;

namespace ZombiesGame
{
    internal class Zombie : Character
    {
        //Zombie's property 
        float _oldHealth;
        float _deathTimeAcc;
        bool _dead = false;

        //Random
        Random _rnd = new Random();

        //Player
        Player _player;

        //Sprites
        GameSprite _sprite_Idle;
        GameSprite _sprite_Dead;

        public Zombie(float x, float y, Player p)
        {
            //Set speed
            _speed = 200;
            _oldHealth = 100;
            _health = 100;

            //Set player
            _player = p;

            //Set AABB
            _physicObject = new AABB(new Vector2f(x, y), 100, 100);

            //Set sprites
            _sprite_Idle = new GameSprite(32, 48, 16, 16);
            _sprite_Dead = new GameSprite(0, 48, 32, 16);

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

                //Destroy object 5 second after death
                if (_deathTimeAcc >= 5)
                {
                    Destroy();
                }

                return;
            }

            //Check if zombie has died
            if(_oldHealth > 0 && _health <= 0)
            {
                _graphicObject = _sprite_Dead;
                _graphicObject.State = GraphicState.BACKGROUND;
                _transformable.Scale = new Vector2f(100, 50);
                _dead = true;
                DropItem();
            }

            //Move zombie to player
            _movement = _player.Position - Position;

            //Make zombie aim at player
            Rotation = (float)Math.Atan2(_player.Position.Y - Position.Y, _player.Position.X - Position.X);

            //Update old health
            _oldHealth = _health;

            //Execute base methode
            base.OnUpdate();
        }

        void DropItem()
        {
            //Generate a random number
            int nbr = _rnd.Next(101);

            if(nbr % 2 == 0)
            {
                switch (_rnd.Next(3))
                {
                    case 0: GetGameState().AddGameObj(new PistolAmmo(Position, _rnd.Next(20,50), _player)); break;
                    case 1: GetGameState().AddGameObj(new RifleAmmo(Position, _rnd.Next(30,60), _player)); break;
                    case 2: GetGameState().AddGameObj(new ShotgunAmmo(Position, _rnd.Next(6,10), _player)); break;
                }
            }

            if (nbr % 20 == 0)
            {
                GetGameState().AddGameObj(new Pistol(Position, _player));
            }

            if (nbr % 30 == 0)
            {
                GetGameState().AddGameObj(new Rifle(Position, _player));
            }

            if (nbr % 50 == 0)
            {
                GetGameState().AddGameObj(new Shotgun(Position, _player));
            }
        }
    }
}