using System;
using System.Collections.Generic;
using System.Text;
using SFML_Engine;
using SFML.System;
using SFML.Graphics;

namespace ZombiesGame
{
    class Zombie : Character
    {
        //Properties
        ZombieState _state = ZombieState.NORMAL; //Current state of the zombie
        ZombieState _nextState;                  //Next state of the zombie
        bool _isSwitching = false;               //Indicate if zombie is switching state   

        //State properties
        Vector2f _objectif;                      //Position that zombie need to reach
        bool _hasObjectif = false;               //Indicate if zombie has an objectif to reach

        //Time accumulators
        float _timeAcc;

        //Constante
        const float NORMAL_SPEED = 250;
        const float CHASING_SPEED = 500;
        const float DEAD_DESTROY_TIME = 5;

        enum ZombieState
        {
            NORMAL,
            CHASING,
            DEAD
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pos"> Zombie position </param>
        public Zombie(Vector2f pos)
        {
            //Set character properties
            _health = 150;
            _MaxHealth = 150;

            //Set graphic handler
            _graphicHandler.AddGraphicObject("Idle", new GameSprite(32, 48, 16, 16));
            _graphicHandler.AddGraphicObject("Dead", new GameSprite(0, 48, 32, 16));
            _graphicHandler.SetDefaultSprite("Idle");

            //Set physic object
            _physicObject = new AABB(pos, 100, 100);

            //Set transformable
            Position = pos;
            Rotation = 0;
            Origin = new Vector2f(0, 0);
            Scale = new Vector2f(50, 50);
        }

        public override void OnStart()
        {
            //Set normal state
            SET_NORMAL_STATE();

            //Base methode
            base.OnStart();
        }

        public override void OnUpdate()
        {
            //Switch state
            if (_isSwitching)
            {
                _state = _nextState;
                _isSwitching = false;

                //Setup next state
                switch (_state)
                {
                    case ZombieState.NORMAL: SET_NORMAL_STATE(); break;
                    case ZombieState.CHASING: SET_CHASING_STATE(); break;
                    case ZombieState.DEAD: SET_DEAD_STATE(); break;
                }
            }

            //Change behavior according to the current state
            switch (_state)
            {
                case ZombieState.NORMAL: NORMAL_STATE(); break;
                case ZombieState.CHASING: CHASING_STATE(); break;
                case ZombieState.DEAD: DEAD_STATE(); break;
            }

            //Base methode
            base.OnUpdate();
        }

        #region ZombieBehavior
        /// <summary>
        /// Setup normal state
        /// </summary>
        void SET_NORMAL_STATE()
        {
            _speed = NORMAL_SPEED;
        }
        /// <summary>
        /// Zombie normal behavior
        /// </summary>
        void NORMAL_STATE()
        {
            //Get an objectif to reach if there's none
            if (!_hasObjectif)
            {
                do
                {
                    _objectif = GameMath.RandomPointInCircle(Position, 200, true);
                } while (_objectif.X > 1920 || _objectif.X < 0 || _objectif.Y > 1080 || _objectif.Y < 0);
                _hasObjectif = true;
            }

            //Make zombie follow the objectif
            GoForward();

            Renderer.DrawCircle(new Circle(_objectif, 10), Color.Red, 20);

            //Verify if zombie has reach the objectif
            if(GameMath.GetVectorLength(_objectif - Position) < 1)
            {
                _hasObjectif = false;
            }
        }
        /// <summary>
        /// Setup chasing state
        /// </summary>
        void SET_CHASING_STATE()
        {
            _speed = CHASING_SPEED;
        }
        /// <summary>
        /// Zombie chasing behavior
        /// </summary>
        void CHASING_STATE() { }
        /// <summary>
        /// Setup dead state
        /// </summary>
        void SET_DEAD_STATE()
        {
            _timeAcc = 0;
        }
        /// <summary>
        /// Zombie dead behavior
        /// </summary>
        void DEAD_STATE()
        {
            _timeAcc += GameTime.DeltaTimeU;

            if(_timeAcc >= DEAD_DESTROY_TIME)
            {
                Destroy();
            }
        }
        #endregion
        #region ZombieFeatures
        /// <summary>
        /// Make the zombie got to a position
        /// </summary>
        void Follow(Vector2f pos)
        {
            _movement = pos - Position;
        }

        void GoForward()
        {
            _movement = GameMath.GetUnitVectorFromAngle(Rotation);
        }

        /// <summary>
        /// Make the zombie aim at a position
        /// </summary>
        /// <param name="pos"></param>
        void AimAt(Vector2f pos)
        {
            Rotation = (float)Math.Atan2(pos.Y - Position.Y, pos.X - Position.X);
        }
        #endregion

        /// <summary>
        /// Switch state
        /// </summary>
        /// <param name="toSwitch"> State to switch to </param>
        void SwitchState(ZombieState toSwitch)
        {
            _nextState = toSwitch;
            _isSwitching = true;
        }
    }
}
