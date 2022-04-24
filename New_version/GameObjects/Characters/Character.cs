using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using ZombiesGame.Systems;
using ZombiesGame.Enums;

namespace ZombiesGame.GameObjects.Characters
{
    class Character : GameObject
    {
        //Character's property
        protected float _health;                          //Health
        protected float _speed;                           //Speed
        protected Vector2f _velocity;                     //Velocity
        protected Vector2f _movement;                     //Velocity
        protected ObjectState _state = ObjectState.ALIVE; //Object state

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

        /// <summary>
        /// Get state
        /// </summary>
        public ObjectState State { get => _state; set => _state = value; }
    }
}
