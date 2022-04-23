using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombiesGame.GameObjects.Items
{
    abstract class Weapon : Item
    {
        protected float _damage;
        protected float _cooldown;

        /// <summary>
        /// Get damage
        /// </summary>
        public float Damage { get => _damage; }

        /// <summary>
        /// Get cooldown
        /// </summary>
        public float Cooldown { get => _cooldown; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="damage"> Weapon's damage </param>
        /// <param name="cooldown"> Weapon's cooldown in seconds </param>
        public Weapon(float damage, float cooldown)
        {
            _damage = damage;
            _cooldown = cooldown;
        }

        public abstract void Attack();
    }
}
