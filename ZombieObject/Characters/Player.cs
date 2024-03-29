﻿using System;
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
        Weapon _weapon;                           //Player's weapon
        float normal_speed = 800;
        float slow_speed = 400;

        //Inventory
        int _pistolBullet = 21;
        int _RifleBullet = 0;
        int _Shell = 0;
        int _money = 0;
        bool _speedBonus = false;
        bool _lifeBonus = false;

        //Constant
        const float HITTED_DURATION = 2;
        const float DEAD_DURATION = 10;

        //Time accumulators
        float _hittedAcc = 0;
        float _deadAcc = 0;

        public int PistolBullet { get => _pistolBullet; set => _pistolBullet = value; }
        public int RifleBullet { get => _RifleBullet; set => _RifleBullet = value; }
        public int Shell { get => _Shell; set => _Shell = value; }
        public int Money { get => _money; set => _money = value; }
        public PlayerState State { get => _state; }
        public Weapon Weapon { get => _weapon; set => _weapon = value; }
        public bool IsSwitching { get => _isSwitching; }
        public PlayerState NextState { get => _nextState; }
        public float NormalSpeed { get => normal_speed; set => normal_speed = value; }
        public float SlowSpeed { get => slow_speed; set => slow_speed = value; }

        //enum
        public enum PlayerState
        {
            NORMAL,
            RELOADING,
            HITTED,
            DEAD
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Player(Vector2f pos)
        {
            //Set character properties
            _health = 3;
            _MaxHealth = 3;

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
            _graphicHandler.AddGraphicObject("Pistol_Idle", new GameSprite(0, 0, 16, 16));
            _graphicHandler.AddGraphicObject("Rifle_Idle", new GameSprite(16, 0, 16, 16));
            _graphicHandler.AddGraphicObject("Pistol_Reload", anim1);
            _graphicHandler.AddGraphicObject("Rifle_Reload", anim2);
            _graphicHandler.SetDefaultSprite("Pistol_Idle");

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

            Pistol pistol = new Pistol();
            GameState.AddGameObj(pistol);
            _weapon = pistol;
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
                    case PlayerState.HITTED: SET_HITTED_STATE(); break;
                    case PlayerState.DEAD: SET_DEAD_STATE(); break;
                }
            }

            //Change behavior according to the current state
            switch (_state)
            {
                case PlayerState.NORMAL: NORMAL_STATE(); break;
                case PlayerState.RELOADING: RELOADING_STATE(); break;
                case PlayerState.HITTED: HITTED_STATE(); break;
                case PlayerState.DEAD: DEAD_STATE(); break;
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
            _graphicHandler.GraphicState = GraphicState.LAYER_3;
            _speed = normal_speed;
        }

        /// <summary>
        /// Player normal behavior
        /// </summary>
        void NORMAL_STATE()
        {
            Move();
            AimAtMouse();
            _weapon.Shoot(this);
            if(Inputs.IsClicked(Keyboard.Key.R) && _weapon.Reload(this))
            {
                SwitchState(PlayerState.RELOADING);
            }
            if (_health <= 0)
            {
                SwitchState(PlayerState.DEAD);
            }
        }

        /// <summary>
        /// Setup reloading state
        /// </summary>
        void SET_RELOADING_STATE()
        {
            _speed = slow_speed;
            _graphicHandler.GraphicState = GraphicState.LAYER_3;
            if (_weapon.GetType() == typeof(Pistol))
            {
                _graphicHandler.PlayAnimation("Pistol_Reload", 1, AnimationType.LOOP);
            }
            else
            {
                _graphicHandler.PlayAnimation("Rifle_Reload", 1, AnimationType.LOOP);
            }
        }

        /// <summary>
        /// Player behavior when reloading
        /// </summary>
        void RELOADING_STATE()
        {
            Move();
            AimAtMouse();
            if(_graphicHandler.CurrentGrphObj.GetType() == typeof(GameSprite))
            {
                SwitchState(PlayerState.NORMAL);
            }
            if (_health <= 0)
            {
                SwitchState(PlayerState.DEAD);
            }
        }

        /// <summary>
        /// Setup hitted state
        /// </summary>
        void SET_HITTED_STATE()
        {
            _speed = normal_speed;
            _hittedAcc = 0;
        }

        /// <summary>
        /// Player hitted behavior
        /// </summary>
        void HITTED_STATE()
        {
            Move();
            AimAtMouse();
            _weapon.Shoot(this);
            _hittedAcc += GameTime.DeltaTimeU;
            _graphicHandler.GraphicState = _graphicHandler.GraphicState == GraphicState.LAYER_3 ? GraphicState.HIDDEN : GraphicState.LAYER_3;
            if (Inputs.IsClicked(Keyboard.Key.R) && _weapon.Reload(this))
            {
                SwitchState(PlayerState.RELOADING);
            }

            if(_hittedAcc >= HITTED_DURATION)
            {
                SwitchState(PlayerState.NORMAL);
            }

            if (_health <= 0)
            {
                SwitchState(PlayerState.DEAD);
            }
        }

        void SET_DEAD_STATE()
        {
            _deadAcc = 0;
        }

        void DEAD_STATE()
        {
            _deadAcc += GameTime.DeltaTimeU;

            Renderer.RenderText($"VOUS ÊTES MORT", 100, GameState.Game.Window.GetView().Center - new Vector2f(200, 100));

            if (_deadAcc >= DEAD_DURATION)
            {
                GameState.Game.Window.Close();
            }
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

        /// <summary>
        /// Switch weapon
        /// </summary>
        /// <param name="toSwitch"> Weapon to switch to </param>
        public void SwitchWeapon(Weapon toSwitch)
        {
            if(_weapon != null)
            {
                _weapon.Destroy();
            }

            _weapon = toSwitch;
            GameState.AddGameObj(_weapon);

            if (_weapon.GetType() == typeof(Pistol))
            {
                _graphicHandler.SetDefaultSprite("Pistol_Idle");
                _graphicHandler.SetDefaultSpriteToCurrent();
            }
            else
            {
                _graphicHandler.SetDefaultSprite("Rifle_Idle");
                _graphicHandler.SetDefaultSpriteToCurrent();
            }
        }

        /// <summary>
        /// Switch state
        /// </summary>
        /// <param name="toSwitch"> State to switch to </param>
        public void SwitchState(PlayerState toSwitch)
        {
            _nextState = toSwitch;
            _isSwitching = true;
        }
        #endregion
    }
}
