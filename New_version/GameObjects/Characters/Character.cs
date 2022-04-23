using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using ZombiesGame.Systems;

namespace ZombiesGame.GameObjects.Characters
{
    class Character : GameObject
    {
        //Character's property
        protected float _health;              //Health
        protected float _speed;               //Speed
        protected Vector2f _velocity;         //Velocity

        /// <summary>
        /// Get/Set health
        /// </summary>
        public float Health { get => _health; set => _health = value; }

        /// <summary>
        /// Get/Set velocity
        /// </summary>
        public Vector2f Velocity { get => _velocity; set => _velocity = value; }

        public override void Start()
        {

        }

        public override void Update()
        {
            if(_velocity.X != 0 || _velocity.Y != 0)
            {
                _transformable.Position += _velocity * _speed * GameTime.DeltaTimeU;
                _aabb.UpdatePosition(_transformable.Position);
            }
        }
    }
}
