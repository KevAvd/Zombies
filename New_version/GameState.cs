using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombiesGame.GameObjects;
using ZombiesGame.GameObjects.Characters;
using ZombiesGame.Systems;

namespace ZombiesGame
{
    internal class GameState
    {
        //GameObjects
        List<GameObject> _gameObj = new List<GameObject>();
        Player _player;
    }
}
