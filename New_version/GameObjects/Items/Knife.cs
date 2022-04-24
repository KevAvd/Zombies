using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombiesGame.PhysicObjects;
using SFML.System;
using SFML.Graphics;

namespace ZombiesGame.GameObjects.Items
{
    internal class Knife : Melee
    {
        public Knife()
        {
            //Weapon property
            _damage = 100;
            _cooldown = 1000;
            _range = 20;

            //Object property
            _physicObject = new AABB(new Vector2f(0, 0), 100, 50);
            _vertices = new Vertex[4];
        }
    }
}
