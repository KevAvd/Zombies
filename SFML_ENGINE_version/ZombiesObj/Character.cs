using SFML.System;
using SFML_Engine.GameObjects;
using SFML_Engine.Mathematics;
using SFML_Engine.Systems;

namespace ZombiesGame
{
    abstract class Character : ScriptObject
    {
        //Character's property
        protected float _health;                          //Health
        protected float _speed;                           //Speed
        protected Vector2f _velocity;                     //Velocity
        protected Vector2f _movement;                     //Movement

        /// <summary>
        /// Get/Set health
        /// </summary>
        public float Health { get => _health; set => _health = value; }

        /// <summary>
        /// Get/Set speed
        /// </summary>
        public float Speed { get => _speed; set => _speed = value; }

        /// <summary>
        /// Get/Set velocity
        /// </summary>
        public Vector2f Velocity { get => _velocity; set => _velocity = value; }

        /// <summary>
        /// Get/Set Movement
        /// </summary>
        public Vector2f Movement { get => _movement; set => _movement = value; }

        public override abstract void OnStart();

        public override void OnUpdate()
        {
            //Kill character if dead
            if(_health == 0)
            {
                Destroy();
            }

            //Move character
            Position += LinearAlgebra.NormalizeVector(_movement) * _speed * GameTime.DeltaTimeU;

            //Handle velocity
            Position += Velocity * GameTime.DeltaTimeU;
            _velocity.X *= 0.9f;
            _velocity.Y *= 0.9f;
            //if (_velocity.X > 1 || _velocity.X < 0)
            //{
            //    _velocity.X *= 0.9f;
            //}
            //else
            //{
            //    _velocity.X = 0;
            //}
            //if (_velocity.Y > 1 || _velocity.Y < 0)
            //{
            //    _velocity.Y *= 0.9f;
            //}
            //else
            //{
            //    _velocity.Y = 0;
            //}
        }
    }
}