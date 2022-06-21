using System;
using System.Collections.Generic;
using System.Text;
using SFML_Engine;
using SFML.System;

namespace ZombiesGame
{
    class Door : ScriptObject
    {
        //State properties
        DoorState _state = DoorState.CLOSED;
        DoorState _nextState;
        bool _isSwitching = false;
        Player _player;

        //time accumulators
        float _closedAcc = 0;

        //Constante
        const float CLOSEDTIME = 5;

        //enum
        public enum DoorState
        {
            CLOSED,
            OPENNING,
            CLOSING,
            OPEN
        }

        public Door(Vector2f pos, Vector2f size, Player p, bool horizontal)
        {
            //Get reference to player
            _player = p;

            //Set graphic handler
            Animation openning = new Animation(100);
            openning.AddSprite(80, 32, 16, 16);
            openning.AddSprite(80, 48, 16, 16);
            openning.AddSprite(80, 64, 16, 16);
            openning.AddSprite(80, 80, 16, 16);
            Animation closing = new Animation(100);
            closing.AddSprite(80, 80, 16, 16);
            closing.AddSprite(80, 64, 16, 16);
            closing.AddSprite(80, 48, 16, 16);
            closing.AddSprite(80, 32, 16, 16);
            _graphicHandler.AddGraphicObject("DoorClosed", new GameSprite(80, 32, 16, 16));
            _graphicHandler.AddGraphicObject("DoorOpen", new GameSprite(80, 80, 16, 16));
            _graphicHandler.AddGraphicObject("DoorOpenning", openning);
            _graphicHandler.AddGraphicObject("DoorClosing", closing);
            _graphicHandler.SetDefaultSprite("DoorClosed");
            _graphicHandler.GraphicState = GraphicState.LAYER_2;

            //Set physic object
            _physicObject = horizontal ? new AABB(new Vector2f(pos.X - size.X, pos.Y), size.X * 3, size.Y) : new AABB(new Vector2f(pos.X, pos.Y - size.Y), size.X, size.Y * 3);

            //Set transformable
            Rotation = horizontal ? GameMath.ToRadian(90) : 0;
            Origin = new Vector2f(0, 0);
            Scale = GameMath.ScaleVector(size, new Vector2f(2, 2), true);
            Position = pos;

        }

        public override void OnStart()
        {

        }

        public override void OnUpdate()
        {
            //Switch state
            if (_isSwitching)
            {
                _state = _nextState;
                _isSwitching = false;

                //Setup new state
                switch (_state)
                {
                    case DoorState.CLOSED: SET_CLOSED_STATE(); break;
                    case DoorState.OPENNING: SET_OPENNING_STATE(); break;
                    case DoorState.CLOSING: SET_CLOSING_STATE(); break;
                    case DoorState.OPEN: SET_OPEN_STATE(); break;
                }
            }

            //Change behavior according to actual state
            switch (_state)
            {
                case DoorState.CLOSED: CLOSED_STATE(); break;
                case DoorState.OPENNING: OPENNING_STATE(); break;
                case DoorState.CLOSING: CLOSING_STATE(); break;
                case DoorState.OPEN: OPEN_STATE(); break;
            }
        }

        #region DoorBehavior
        void SET_CLOSED_STATE()
        {
            _physicObject.State = PhysicState.NORMAL;
            _closedAcc = 0;
        }

        void SET_OPENNING_STATE()
        {
            _physicObject.State = PhysicState.NORMAL;
            _graphicHandler.PlayAnimation("DoorOpenning", 1);
            _graphicHandler.SetDefaultSprite("DoorOpen");
        }

        void SET_CLOSING_STATE()
        {
            _physicObject.State = PhysicState.NORMAL;
            _graphicHandler.PlayAnimation("DoorClosing", 1);
            _graphicHandler.SetDefaultSprite("DoorClosed");
        }

        void SET_OPEN_STATE()
        {
            _physicObject.State = PhysicState.NOCLIP;
        }

        void CLOSED_STATE()
        {

        }

        void OPENNING_STATE()
        {
            if (_graphicHandler.CurrentGrphObj.GetType() == typeof(GameSprite))
            {
                SwitchState(DoorState.OPEN);
            }
        }

        void CLOSING_STATE()
        {
            if (_graphicHandler.CurrentGrphObj.GetType() == typeof(GameSprite))
            {
                SwitchState(DoorState.CLOSED);
            }
        }

        void OPEN_STATE()
        {
            _closedAcc += GameTime.DeltaTimeU;

            if (_closedAcc >= CLOSEDTIME)
            {
                _closedAcc = 0;

                if(CollisionDetection.AABB_AABB(_physicObject as AABB, _player.PhysicObject as AABB))
                {
                    return;
                }

                SwitchState(DoorState.CLOSING);
            }
        }
        #endregion

        /// <summary>
        /// Switch state
        /// </summary>
        /// <param name="toSwitch"></param>
        public void SwitchState(DoorState toSwitch)
        {
            _nextState = toSwitch;
            _isSwitching = true;
        }

        /// <summary>
        /// Open the door
        /// </summary>
        public void Open()
        {
            if(_state != DoorState.CLOSED)
            {
                return;
            }

            SwitchState(DoorState.OPENNING);
        }

        /// <summary>
        /// Close the door
        /// </summary>
        public void Close()
        {
            if (_state != DoorState.OPEN)
            {
                return;
            }

            SwitchState(DoorState.OPENNING);
        }
    }
}
