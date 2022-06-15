using System;
using System.Collections.Generic;
using System.Text;

namespace ZombiesGame
{
    class RifleAuto : Weapon
    {
        public RifleAuto()
        {
            //Set weapon properties
            _maxammo = 30;
            _ammo = 30;
            _fireMode = FireMode.AUTO;
            _fireRate = 0.1f;
            _burstCooldown = 0.0f;
            _dammage = 25;
            _range = 2000;
            _ammoType = AmmoType.RIFLE;
        }
    }
}
