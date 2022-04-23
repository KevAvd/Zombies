using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombiesGame.GameObjects.Items
{
    abstract class Firearm : Weapon
    {
        public Firearm(float damage, float cooldown) : base(damage, cooldown)
        {
        }

        public override void Attack()
        {

        }

        public abstract void Reload();
    }
}
