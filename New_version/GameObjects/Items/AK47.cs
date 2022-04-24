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
    internal class AK47 : Firearm
    {
        public AK47()
        {
            //Weapon property
            _maxAmmo = 30;
            _ammoCount = _maxAmmo;
            _ammoType = AmmoType.RIFLE;
            _damage = 1;
            _cooldown = 100;
            _range = 1000;

            //Object property
            _physicObject = new AABB(new Vector2f(0, 0), 100, 50);
            _vertices = new Vertex[4];
            
        }
    }
}
