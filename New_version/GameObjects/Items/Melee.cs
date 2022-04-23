using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombiesGame.GameObjects.Items
{
    internal class Melee : Weapon
    {
        public Melee(float damage, float cooldown) : base(damage, cooldown)
        {
        }

        public override void Attack()
        {

        }
    }
}
