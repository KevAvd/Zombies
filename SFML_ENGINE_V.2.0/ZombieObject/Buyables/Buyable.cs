using System;
using System.Collections.Generic;
using System.Text;
using SFML_Engine;

namespace ZombiesGame
{
    abstract class Buyable : GameObject
    {
        //Properties
        protected int _price;           //Price of the buyable
        protected Player _player;       //Player who triggered the buyable
        protected int _maxNbrOfBuy;
        protected int _nbrOfBuy;
        public void Buy(Player p)
        {
            _player = p;

            if(_player.Money >= _price && _nbrOfBuy < _maxNbrOfBuy)
            {
                _player.Money -= _price;
                _nbrOfBuy++;
                Buyed();
                return;
            }

            CantAfford();
        }

        protected abstract void Buyed();

        protected abstract void CantAfford();

        public void Reset()
        {
            _nbrOfBuy = 0;
        }
    }
}
