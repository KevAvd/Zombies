using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombiesGame
{
    class PlayerSGLTN
    {
        Player _player;
        static PlayerSGLTN _instance;

        public Player Player { get => _player; set => _player = value; }

        private PlayerSGLTN()
        {
            _player = new Player();
        }

        static public PlayerSGLTN GetInstance()
        {
            if(_instance == null)
            {
                _instance = new PlayerSGLTN();
            }

            return _instance;
        }

    }
}
