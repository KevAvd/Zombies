﻿using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;
using SFML_Engine;

namespace ZombiesGame
{
    class Character : ScriptObject
    {
        //Character's property
        protected int _health;                          //Health
        protected int _MaxHealth;                       //Health
        protected float _speed;                         //Speed
        protected Vector2f _velocity;                   //Velocity
        protected Vector2f _movement;                   //Movement

        /// <summary>
        /// Get/Set health
        /// </summary>
        public int Health { get => _health; set => _health = value; }

        /// <summary>
        /// Get/Set health
        /// </summary>
        public int MaxHealth { get => _MaxHealth; set => _MaxHealth = value; }

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

        public override void OnStart()
        {
            _graphicHandler.GraphicState = GraphicState.LAYER_3;
        }

        public override void OnUpdate()
        {
            //Move character
            Position += GameMath.NormalizeVector(_movement) * _speed * GameTime.DeltaTimeU;

            //Handle velocity
            Position += Velocity * GameTime.DeltaTimeU;
            _velocity.X *= 0.9f;
            _velocity.Y *= 0.9f;
        }
    }
}
