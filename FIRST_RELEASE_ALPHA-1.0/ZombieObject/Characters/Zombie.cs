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

        //Random
        Random _rnd = new Random();

        //Reference
        Player _player;
        RoundHandler _roundHandler;

        //State properties
        float _angVel = GameMath.ToRadian(90);   //Angular velocity of zombie

        //Time accumulators
        float _deadAcc;                          //Accumulate time

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
        /// <param name="player"> Player </param>
        public Zombie(Vector2f pos, Player player, RoundHandler rndHndlr)
        {
            //Get reference to player
            _player = player;
            _roundHandler = rndHndlr;

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

            //Check if zombie is dead and change state accordingly
            if (_state != ZombieState.DEAD && _health <= 0)
            {
                SwitchState(ZombieState.DEAD);
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
            //Make zombie walk in circle
            GoForward();
            Rotation += _angVel * GameTime.DeltaTimeU;

            //Start chasing player if close enough
            if(GameMath.GetVectorLength(_player.Position - Position) < 500)
            {
                SwitchState(ZombieState.CHASING);
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
        void CHASING_STATE()
        {
            Follow(_player.Position);
            AimAt(_player.Position);

            //Stop chasing player if too far
            if (GameMath.GetVectorLength(_player.Position - Position) > 500)
            {
                SwitchState(ZombieState.NORMAL);
            }
        }
        /// <summary>
        /// Setup dead state
        /// </summary>
        void SET_DEAD_STATE()
        {
            _deadAcc = 0;
            _graphicHandler.SetDefaultSprite("Dead");
            _graphicHandler.SetDefaultSpriteToCurrent();
            _graphicHandler.GraphicState = GraphicState.LAYER_1;
            Scale = new Vector2f(100, 50);
            _physicObject.State = PhysicState.NOCLIP;
            _velocity = new Vector2f(0, 0);
            _movement = new Vector2f(0, 0);
            _player.Money += 100;
            _roundHandler.NbrOfZombie--;
            DropItem();
        }
        /// <summary>
        /// Zombie dead behavior
        /// </summary>
        void DEAD_STATE()
        {
            _deadAcc += GameTime.DeltaTimeU;

            if(_deadAcc >= DEAD_DESTROY_TIME)
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

        /// <summary>
        /// Make the zombie go forward
        /// </summary>
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


        void DropItem()
        {
            //Generate a random number
            int nbr = _rnd.Next(101);

            if (nbr % 2 == 0)
            {
                switch (_rnd.Next(3))
                {
                    case 0: GameState.AddGameObj(new PistolAmmo(Position, _rnd.Next(20, 50))); break;
                    case 1: GameState.AddGameObj(new RifleAmmo(Position, _rnd.Next(30, 60))); break;
                    case 2: GameState.AddGameObj(new ShellAmmo(Position, _rnd.Next(6, 10))); break;
                }
            }
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
