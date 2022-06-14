using System;
using System.Collections.Generic;
using System.Text;
using SFML_Engine;
using SFML.System;
using SFML.Window;

namespace ZombiesGame
{
    class Player : Character
    {
        //Properties
        PlayerState _state = PlayerState.NORMAL;  //Current state of the player
        PlayerState _nextState;                   //Next state of the player
        bool _isSwitching = false;                //Indicate if player is switching state

        //Constant
        const float NORMAL_SPEED = 800;
        const float SLOW_SPEED = 400;

        //enum
        enum PlayerState
        {
            NORMAL,
            RELOADING,
            SHOOTING
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Player(Vector2f pos)
        {
            //Set character properties
            _health = 5;
            _MaxHealth = 5;

            //Set graphic handler
            Animation anim1 = new Animation(200);
            anim1.AddSprite(0, 0, 16, 16);
            anim1.AddSprite(0, 16, 16, 16);
            anim1.AddSprite(0, 32, 16, 16);
            Animation anim2 = new Animation(200);
            anim2.AddSprite(16, 0, 16, 16);
            anim2.AddSprite(16, 16, 16, 16);
            anim2.AddSprite(16, 32, 16, 16);
            _graphicHandler.AddGraphicObject("Idle", new GameSprite(48, 48, 16, 16));
            _graphicHandler.AddGraphicObject("Pistol_Idle", new GameSprite(48, 48, 16, 16));
            _graphicHandler.AddGraphicObject("Rifle_Idle", new GameSprite(48, 48, 16, 16));
            _graphicHandler.AddGraphicObject("Pistol_Reload", anim1);
            _graphicHandler.AddGraphicObject("Rifle_Reload", anim2);
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
            //Reset movement
            _movement = new Vector2f(0, 0);

            //Switch state
            if (_isSwitching)
            {
                _state = _nextState;
                _isSwitching = false;

                //Setup new state
                switch (_state)
                {
                    case PlayerState.NORMAL: SET_NORMAL_STATE(); break;
                    case PlayerState.RELOADING: SET_RELOADING_STATE(); break;
                    case PlayerState.SHOOTING: SET_SHOOTING_STATE(); break;
                }
            }

            //Change behavior according to the current state
            switch (_state)
            {
                case PlayerState.NORMAL: NORMAL_STATE(); break;
                case PlayerState.RELOADING: RELOADING_STATE(); break;
                case PlayerState.SHOOTING: SHOOTING_STATE(); break;
            }

            //Base methode
            base.OnUpdate();
        }

        #region PlayerBehaviors
        /// <summary>
        /// Setup normal state
        /// </summary>
        void SET_NORMAL_STATE()
        {
            _speed = NORMAL_SPEED;
        }

        /// <summary>
        /// Player normal behavior
        /// </summary>
        void NORMAL_STATE()
        {
            Move();
            AimAtMouse();
        }

        /// <summary>
        /// Setup reloading state
        /// </summary>
        void SET_RELOADING_STATE()
        {
            _speed = SLOW_SPEED;
        }

        /// <summary>
        /// Player behavior when reloading
        /// </summary>
        void RELOADING_STATE()
        {
            Move();
            AimAtMouse();
        }

        /// <summary>
        /// Setup shooting state
        /// </summary>
        void SET_SHOOTING_STATE()
        {
            _speed = SLOW_SPEED;
        }

        /// <summary>
        /// Player behavior when shooting
        /// </summary>
        void SHOOTING_STATE()
        {
            Move();
            AimAtMouse();
        }
        #endregion

        #region PlayerFeatures
        /// <summary>
        /// Move player
        /// </summary>
        void Move()
        {
            if (Inputs.IsPressed(Keyboard.Key.W))
            {
                _movement += new Vector2f(0, -1);
            }
            if (Inputs.IsPressed(Keyboard.Key.A))
            {
                _movement += new Vector2f(-1, 0);
            }
            if (Inputs.IsPressed(Keyboard.Key.S))
            {
                _movement += new Vector2f(0, 1);
            }
            if (Inputs.IsPressed(Keyboard.Key.D))
            {
                _movement += new Vector2f(1, 0);
            }
        }

        /// <summary>
        /// Make player aim at mouse
        /// </summary>
        void AimAtMouse()
        {
            Vector2f mousePos = Inputs.GetMousePosition(true);
            Rotation = MathF.Atan2(mousePos.Y - Position.Y, mousePos.X - Position.X);
        }
        #endregion

        /// <summary>
        /// Switch state
        /// </summary>
        /// <param name="toSwitch"> State to switch to </param>
        void SwitchState(PlayerState toSwitch)
        {
            _nextState = toSwitch;
            _isSwitching = true;
        }
    }
}
