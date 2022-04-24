using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombiesGame.PhysicObjects;

namespace ZombiesGame.GameObjects.Items
{
    abstract class Weapon : Item
    {
        protected float _damage;
        protected float _cooldown;
        protected float _range;

        /// <summary>
        /// Get damage
        /// </summary>
        public float Damage { get => _damage; }

        /// <summary>
        /// Get cooldown
        /// </summary>
        public float Cooldown { get => _cooldown; }

        /// <summary>
        /// Get range
        /// </summary>
        public float Range { get => _range; }
    }
}
