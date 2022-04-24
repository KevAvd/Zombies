using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombiesGame.PhysicObjects;
using ZombiesGame.Enums;

namespace ZombiesGame.GameObjects.Items
{
    abstract class Firearm : Weapon
    {
        protected AmmoType _ammoType;
        protected int _ammoCount;
        protected int _maxAmmo;

        /// <summary>
        /// Get number of ammo left
        /// </summary>
        public int AmmoCount { get => _ammoCount; }

        /// <summary>
        /// Get ammo type
        /// </summary>
        public AmmoType AmmoType { get => _ammoType; }

        /// <summary>
        /// Reload weapon
        /// </summary>
        /// <param name="amount"> Amount of ammo to load </param>
        /// <returns> Excess ammo </returns>
        public int Reload(int amount)
        {
            _ammoCount += amount;

            if(_ammoCount > _maxAmmo)
            {
                int excess = _ammoCount - _maxAmmo;
                _ammoCount = _maxAmmo;
                return excess;
            }

            return 0;
        }
    }
}
