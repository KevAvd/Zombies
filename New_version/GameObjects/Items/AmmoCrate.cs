using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombiesGame.Enums;
using ZombiesGame.PhysicObjects;

namespace ZombiesGame.GameObjects.Items
{
    class AmmoCrate : Item
    {
        //Property
        AmmoType _type;                 //Type of ammunition
        int _count;                     //Amount of ammunition

        /// <summary>
        /// Get/Set number of ammo
        /// </summary>
        public int Count { get => _count; set => _count = value; }

        /// <summary>
        /// Get type of ammo
        /// </summary>
        internal AmmoType Type { get => _type; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="zone"> Zone to reach for interacting with the item </param>
        /// <param name="ammoType"> Type of ammunition </param>
        /// <param name="nbrOfAmmo"> Number of ammunition </param>
        public AmmoCrate(AmmoType ammoType, int nbrOfAmmo, AABB zone)
        {
            _type = ammoType;
            _count = nbrOfAmmo;
            _physicObject = zone;
        }
    }
}
