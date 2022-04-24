using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombiesGame.Enums;
using ZombiesGame.PhysicObjects;
using SFML.System;
using SFML.Graphics;

namespace ZombiesGame.GameObjects.Items
{
    internal class M1911 : Firearm
    {
        public M1911()
        {
            //Weapon property
            _maxAmmo = 7;
            _ammoCount = _maxAmmo;
            _ammoType = AmmoType.RIFLE;
            _damage = 1;
            _cooldown = 150;
            _range = 500;

            //Object property
            _physicObject = new AABB(new Vector2f(0, 0), 100, 50);
            _vertices = new Vertex[4];
        }
    }
}
