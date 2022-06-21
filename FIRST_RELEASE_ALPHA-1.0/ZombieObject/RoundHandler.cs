using System;
using System.Collections.Generic;
using System.Text;
using SFML_Engine;
using SFML.System;

namespace ZombiesGame
{
    class RoundHandler : ScriptObject
    {
        //Properties
        int _roundNbr = 0;
        int _nbrOfZombie = 0;
        int _spawnedZombie = 0;
        int _nbrOfZombieToSpawn = 0;
        RoundState _state = RoundState.STARTING;
        RoundState _nextState;
        bool _isSwitching;
        Player _player;

        //Time accumulators
        float _endingRoundAcc = 0;
        float _startingRoundAcc = 0;

        //Constante
        const float ENDING_ROUND_DURATION = 10;
        const float STARTING_ROUND_DURATION = 10;

        public int RoundNbr { get => _roundNbr;}
        public int NbrOfZombie { get => _nbrOfZombie; set => _nbrOfZombie = value; }
        public RoundState State { get => _state; set => _state = value; }

        //Enum
        public enum RoundState
        {
            STARTING,
            PLAYING,
            ENDING,
            GAME_END
        }

        public RoundHandler(Player p)
        {
            _player = p;
        }

        public override void OnStart()
        {
            SET_STARTING_STATE();
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
                    case RoundState.STARTING: SET_STARTING_STATE(); break;
                    case RoundState.PLAYING: SET_PLAYING_STATE(); break;
                    case RoundState.ENDING: SET_ENDING_STATE(); break;
                    case RoundState.GAME_END: SET_GAMEEND_STATE(); break;
                }
            }

            //Change behavior according to the current state
            switch (_state)
            {
                case RoundState.STARTING: STARTING_STATE(); break;
                case RoundState.PLAYING: PLAYING_STATE(); break;
                case RoundState.ENDING: ENDING_STATE(); break;
                case RoundState.GAME_END: GAMEEND_STATE(); break;
            }
        }

        #region RoundBehavior
        void SET_STARTING_STATE()
        {
            _startingRoundAcc = 0;
            _roundNbr++;
        }

        void SET_PLAYING_STATE()
        {
            _spawnedZombie = 0;
            if(_nbrOfZombieToSpawn <= 50)
            {
                _nbrOfZombieToSpawn += 10;
            }
        }

        void SET_ENDING_STATE()
        {
            _endingRoundAcc = 0;
        }

        void SET_GAMEEND_STATE()
        {
            _endingRoundAcc = 0;
        }

        void STARTING_STATE()
        {
            Renderer.RenderText($"MANCHE {_roundNbr}", 100, GameState.Game.Window.GetView().Center - new Vector2f(200,100));

            _startingRoundAcc += GameTime.DeltaTimeU;

            if (_startingRoundAcc >= STARTING_ROUND_DURATION)
            {
                SwitchState(RoundState.PLAYING);
            }
        }

        void PLAYING_STATE()
        {
            if(_nbrOfZombie == 0 && _spawnedZombie < _nbrOfZombieToSpawn)
            {
                for(int i = 0; i < 10; i++)
                {
                    GameState.AddGameObj(new Zombie(new Vector2f(i * 100 + 200, 200), _player, this));
                    _nbrOfZombie++;
                    _spawnedZombie++;
                }
            }

            if(_nbrOfZombie == 0)
            {
                SwitchState(RoundState.ENDING);
            }
        }

        void ENDING_STATE()
        {
            _endingRoundAcc += GameTime.DeltaTimeU;

            if(_endingRoundAcc >= ENDING_ROUND_DURATION)
            {
                SwitchState(RoundState.STARTING);
            }
        }

        void GAMEEND_STATE()
        {

        }
        #endregion

        void SwitchState(RoundState toSwitch)
        {
            _nextState = toSwitch;
            _isSwitching = true;
        }
    }
}
