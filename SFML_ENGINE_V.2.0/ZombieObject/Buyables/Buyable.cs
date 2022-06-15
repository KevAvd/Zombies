using System;
using System.Collections.Generic;
using System.Text;
using SFML_Engine;

namespace ZombiesGame
{
    abstract class Buyable : GameObject
    {
        //Properties
        int _price;                     //Price of the buyable
        protected Player _player;       //Player who triggered the buyable

        protected void Buy(Player p)
        {
            _player = p;

            if(_player.Money >= _price)
            {
                _player.Money -= _price;
                Buyed();
                return;
            }

            CantAfford();
        }

        protected abstract void Buyed();

        protected abstract void CantAfford();
    }
}
